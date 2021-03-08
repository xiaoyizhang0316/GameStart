using System;
using System.IO;
using System.Threading.Tasks;
using MHLab.Patch.Core.Client;
using MHLab.Patch.Core.Client.IO;
using MHLab.Patch.Core.IO;
using MHLab.Patch.Launcher.Scripts.Localization;
using MHLab.Patch.Launcher.Scripts.Utilities;
using MHLab.Patch.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MHLab.Patch.Launcher.Scripts
{
    public sealed class LauncherUpdater : MonoBehaviour
    {
        public LauncherData Data;
        public int SceneToLoad;
        
        private PatcherUpdater _patcherUpdater;
        private UpdatingContext _context;
        
        private void Initialize(LauncherSettings settings)
        {
            var progress = new ProgressReporter();
            progress.ProgressChanged.AddListener(Data.UpdateProgressChanged);

            _context = new UpdatingContext(settings, progress);
            _context.LocalizedMessages = new EnglishUpdaterLocalizedMessages();

            _patcherUpdater = new PatcherUpdater(_context);
            _patcherUpdater.Downloader.DownloadComplete += Data.DownloadComplete;
            _patcherUpdater.Downloader.ProgressChanged += Data.DownloadProgressChanged;
            
            _context.RegisterUpdateStep(_patcherUpdater);

            _context.Runner.PerformedStep += (sender, updater) =>
            {
                if (_context.IsDirty) UpdateRestartNeeded();
            };
        }

        private LauncherSettings CreateSettings()
        {
            var settings = new LauncherSettings();
            settings.RemoteUrl = Data.RemoteUrl;
            settings.PatchDownloadAttempts = 3;
            settings.AppDataPath = Application.persistentDataPath;

            string rootPath = string.Empty;
            
#if UNITY_EDITOR
            rootPath = Path.Combine(Path.GetDirectoryName(Application.dataPath), LauncherData.WorkspaceFolderName, "TestLauncher");
            Directory.CreateDirectory(rootPath);
#elif UNITY_STANDALONE_WIN
            rootPath = Directory.GetParent(Directory.GetParent(Application.dataPath).FullName).FullName;
#elif UNITY_STANDALONE_LINUX
            rootPath = Directory.GetParent(Directory.GetParent(Application.dataPath).FullName).FullName;
#elif UNITY_STANDALONE_OSX
            rootPath = Directory.GetParent(Directory.GetParent(Directory.GetParent(Application.dataPath).FullName).FullName).FullName;
#endif
            
            settings.RootPath = rootPath;

            return settings;
        }
        
        private void Awake()
        {
             Initialize(CreateSettings());

            Data.ResetComponents();
        }

        /// <summary>
        /// 进行游戏客户端更新
        /// </summary>
        public void Init()
        {
            Initialize(CreateSettings());
            try
            {
                _context.Logger.Info("===> Launcher updating process STARTED! <===");

                if (!NetworkChecker.IsNetworkAvailable())
                {
                    Data.Log(_context.LocalizedMessages.NotAvailableNetwork);
                    _context.Logger.Error(null, "Updating process FAILED! Network is not available or connectivity is low/weak... Check your connection!");
                    return;
                }

                if (!NetworkChecker.IsRemoteServiceAvailable(_context.Settings.GetRemoteBuildsIndexUrl()))
                {
                    Data.Log(_context.LocalizedMessages.NotAvailableServers);
                    _context.Logger.Error(null, "Updating process FAILED! Our servers are not responding... Wait some minutes and retry!");
                    return;
                }

                _context.Initialize();

                Task.Run(CheckForUpdates);
            }
            catch (Exception ex)
            {
                Data.Log(_context.LocalizedMessages.UpdateProcessFailed);
                _context.Logger.Error(ex, "===> Launcher updating process FAILED! <===");
            }
        }

        private void Start()
        {
             try
             {
                 _context.Logger.Info("===> Launcher updating process STARTED! <===");
               
                 if (!NetworkChecker.IsNetworkAvailable())
                 {
                     Data.Log(_context.LocalizedMessages.NotAvailableNetwork);
                     _context.Logger.Error(null, "Updating process FAILED! Network is not available or connectivity is low/weak... Check your connection!");
                     return;
                 }

                 if (!NetworkChecker.IsRemoteServiceAvailable(_context.Settings.GetRemoteBuildsIndexUrl()))
                 {
                     Data.Log(_context.LocalizedMessages.NotAvailableServers);
                     _context.Logger.Error(null, "Updating process FAILED! Our servers are not responding... Wait some minutes and retry!");
                     return;
                 }

                 _context.Initialize();

                 Task.Run(CheckForUpdates);
             }
             catch (Exception ex)
             {
                 Data.Log(_context.LocalizedMessages.UpdateProcessFailed);
                 _context.Logger.Error(ex, "===> Launcher updating process FAILED! <===");
             }
        }
        
        private void CheckForUpdates()
        {
            UpdateStarted();

            try
            {
                _context.Update();

                UpdateCompleted();
            }
            catch (Exception ex)
            {
                Debug.Log(ex);
                UpdateFailed(ex);
            }
            finally
            {
                Data.StopTimer();
            }
        }
        
        private void UpdateStarted()
        {
            Data.StartTimer();
        }

        private void UpdateCompleted()
        {
            Data.Dispatcher.Invoke(() =>
            {
                Data.ProgressBar.Value = 1;
                Data.ProgressPercentage.text = "100%";
            });
            
            var repairer = new Repairer(_context);
            var updater = new Updater(_context);

            if (repairer.IsRepairNeeded() || updater.IsUpdateAvailable())
            {
                UpdateRestartNeeded();
                return;
            }
            
            Data.Log(_context.LocalizedMessages.UpdateProcessCompleted);
            _context.Logger.Info("===> Launcher updating process COMPLETED! <===");
            StartGameScene();
        }

        private void StartGameScene()
        {
            Data.Dispatcher.Invoke(() => 
            {
                SceneManager.LoadScene(SceneToLoad);
            });
        }

        private void UpdateFailed(Exception e)
        {
            Data.Log(_context.LocalizedMessages.UpdateProcessFailed);
            _context.Logger.Error(e, "===> Launcher updating process FAILED! <===");
        }

        private void UpdateRestartNeeded()
        {
            Data.Log(_context.LocalizedMessages.UpdateRestartNeeded);
            _context.Logger.Info("===> Launcher updating process INCOMPLETE: restart is needed! <===");
            
            EnsureExecutePrivileges(PathsManager.Combine(_context.Settings.RootPath, Data.LauncherExecutableName));
            
            ApplicationStarter.StartApplication(Path.Combine(_context.Settings.RootPath, Data.LauncherExecutableName), "");
            
            Data.Dispatcher.Invoke(Application.Quit);
        }
        
        private void EnsureExecutePrivileges(string filePath)
        {
            try
            {
                PrivilegesSetter.EnsureExecutePrivileges(filePath);
            }
            catch (Exception ex)
            {
                _context.Logger.Error(ex, "Unable to set executing privileges on {FilePath}.", filePath);
            }
        }
    }
}