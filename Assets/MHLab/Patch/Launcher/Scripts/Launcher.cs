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
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

namespace MHLab.Patch.Launcher.Scripts
{
    public sealed class Launcher : MonoBehaviour
    {
        public LauncherData Data;
        
        private Repairer _repairer;
        private Updater _updater;
        private UpdatingContext _context;

        public Button gameStart;
        
        private void Initialize(LauncherSettings settings)
        {
            gameStart.gameObject.SetActive(false);
            var progress = new ProgressReporter();
            progress.ProgressChanged.AddListener(Data.UpdateProgressChanged);

            _context = new UpdatingContext(settings, progress);
            _context.LocalizedMessages = new EnglishUpdaterLocalizedMessages();

            _repairer = new Repairer(_context);
            _repairer.Downloader.DownloadComplete += Data.DownloadComplete;
            _repairer.Downloader.ProgressChanged += Data.DownloadProgressChanged;

            _updater = new Updater(_context);
            _updater.Downloader.DownloadComplete += Data.DownloadComplete;
            _updater.Downloader.ProgressChanged += Data.DownloadProgressChanged;
            
            _context.RegisterUpdateStep(_repairer);
            _context.RegisterUpdateStep(_updater);

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
            rootPath = Directory.GetParent(Application.dataPath).FullName;
#elif UNITY_STANDALONE_LINUX
            rootPath = Directory.GetParent(Application.dataPath).FullName;
#elif UNITY_STANDALONE_OSX
            rootPath = Directory.GetParent(Directory.GetParent(Application.dataPath).FullName).FullName;
#endif
            
            settings.RootPath = rootPath;

            return settings;
        }
        
        private void Awake()
        {
            //Initialize(CreateSettings());

            Data.ResetComponents();
        }

        /// <summary>
        /// 进行游戏客户端更新
        /// </summary>
        public void Init(string url)
        {
#if UNITY_STANDALONE_OSX
            Data.GameExecutableName = "Bu.M.app";
            Data.LauncherExecutableName = "StartGame.app";
            Data.RemoteUrl = url + "macpatch";
#elif UNITY_STANDALONE_WIN
            Data.GameExecutableName = "Bu.M.exe";
            Data.LauncherExecutableName = "StartGame.exe";
            Data.RemoteUrl = url + "patch";
#endif
            Initialize(CreateSettings());
            gameStart.onClick.AddListener(() =>
            {
                Invoke(nameof(StartGame), 1.5f);
            });
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
            //try
            //{
            //    _context.Logger.Info("===> Updating process STARTED! <===");
                
            //    if (!NetworkChecker.IsNetworkAvailable())
            //    {
            //        Data.Log(_context.LocalizedMessages.NotAvailableNetwork);
            //        _context.Logger.Error(null, "Updating process FAILED! Network is not available or connectivity is low/weak... Check your connection!");
            //        return;
            //    }

            //    if (!NetworkChecker.IsRemoteServiceAvailable(_context.Settings.GetRemoteBuildsIndexUrl()))
            //    {
            //        Data.Log(_context.LocalizedMessages.NotAvailableServers);
            //        _context.Logger.Error(null, "Updating process FAILED! Our servers are not responding... Wait some minutes and retry!");
            //        return;
            //    }

            //    _context.Initialize();

            //    Task.Run(CheckForUpdates);
            //}
            //catch (Exception ex)
            //{
            //    Data.Log(_context.LocalizedMessages.UpdateProcessFailed);
            //    _context.Logger.Error(ex, "===> Updating process FAILED! <===");
            //}
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
            Data.Log(_context.LocalizedMessages.UpdateProcessCompleted);
            _context.Logger.Info("===> Updating process COMPLETED! <===");
            
            Data.Dispatcher.Invoke(() =>
            {
                Data.ProgressBar.Value = 1;
                Data.ProgressPercentage.text = "100%";
            });
            
            EnsureExecutePrivileges(PathsManager.Combine(_context.Settings.GetGamePath(), Data.GameExecutableName));
            EnsureExecutePrivileges(PathsManager.Combine(_context.Settings.RootPath, Data.LauncherExecutableName));
            
            Data.Dispatcher.Invoke(() =>
            {
                gameStart.gameObject.SetActive(true);
                Data.ElapsedTime.text = string.Empty;
                Data.stepPer.text = string.Empty;
            });
        }

        private void UpdateFailed(Exception e)
        {
            Data.Log(_context.LocalizedMessages.UpdateProcessFailed);
            _context.Logger.Error(e, "===> Updating process FAILED! <===");
        }

        private void UpdateRestartNeeded()
        {
            Data.Log(_context.LocalizedMessages.UpdateRestartNeeded);
            _context.Logger.Info("===> Updating process INCOMPLETE: restart is needed! <===");
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

        private void StartGame()
        {
            var filePath = PathsManager.Combine(_context.Settings.GetGamePath(), Data.GameExecutableName);
            ApplicationStarter.StartApplication(filePath, $"{_context.Settings.LaunchArgumentParameter}={_context.Settings.LaunchArgumentValue}");
            Application.Quit();
        }
    }
}