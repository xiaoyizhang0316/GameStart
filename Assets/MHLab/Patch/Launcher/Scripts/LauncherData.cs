using System;
using System.IO;
using System.Threading;
using MHLab.Patch.Core.Client;
using MHLab.Patch.Core.Client.IO;
using MHLab.Patch.Core.Client.Progresses;
using MHLab.Patch.Core.Utilities;
using MHLab.Patch.Launcher.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

namespace MHLab.Patch.Launcher.Scripts
{
    public sealed class LauncherData : MonoBehaviour
    {
        public string RemoteUrl;
        public string LauncherExecutableName;
        public string GameExecutableName;

        public Dispatcher Dispatcher;
        public ProgressBar ProgressBar;
        public Text DownloadSpeed;
        public Text ProgressPercentage;
        public Text Logs;
        public Text ElapsedTime; 
        public Text stepPer;
         
        public const string WorkspaceFolderName = "PATCHWorkspace";

        private Timer _timer;
        private int _elapsed;

        private DateTime _lastTime = DateTime.UtcNow;
        private long _lastSize = 0;
        private int _downloadSpeed = 0;

        public void DownloadProgressChanged(object sender, DownloadEventArgs e)
        {
            if (_lastTime.AddSeconds(1) <= DateTime.UtcNow)
            { 
                _downloadSpeed = (int) ((e.CurrentFileSize - _lastSize) / (DateTime.UtcNow - _lastTime).TotalSeconds);
                if (_downloadSpeed < 0)
                {
                    _downloadSpeed = 0;

                    DownloadSpeed.gameObject.SetActive(false);
                }

                else
                {
                    DownloadSpeed.gameObject.SetActive(true);
                    
                }
 
                //Debug.Log("Last Size:" + _lastSize.ToString() + "-----currentSize" + e.CurrentFileSize.ToString() + "-------Total size:" + e.TotalFileSize.ToString());
            
                _lastSize = e.CurrentFileSize;
                _lastTime = DateTime.UtcNow;
               
            }

            Dispatcher.Invoke(() =>
            {
                DownloadSpeed.text = FormatUtility.FormatSizeBinary(_downloadSpeed, 2) + "/s";
                ;
            });
        }

        public void DownloadComplete(object sender, EventArgs e)
        {

            Dispatcher.Invoke(() =>
            {
                DownloadSpeed.text = string.Empty;

            });
        }

        public void UpdateProgressChanged(UpdateProgress e)
        {
            Dispatcher.Invoke(() =>
            {
                var totalSteps = Math.Max(e.TotalSteps, 1);
                ProgressBar.Value = (float) e.CurrentSteps / totalSteps;
                stepPer.text = Mathf.Min( e.CurrentSteps, totalSteps).ToString() + "/" + totalSteps.ToString();
                ProgressPercentage.text = (e.CurrentSteps * 100 / totalSteps) + "%";
                per = Math.Min( (e.CurrentSteps * 100 / totalSteps),100);
            });

            Log(e.StepMessage);
        }

        public void Log(string message)
        {
            Dispatcher.Invoke(() => { Logs.text = message; });
        }

        public void ResetComponents()
        {
            ProgressPercentage.text = string.Empty;
            DownloadSpeed.text = string.Empty;
            ElapsedTime.text = string.Empty;
            Logs.text = string.Empty;

            ProgressBar.Value = 0;
        }

        private int per;

        public void StartTimer()
        {
            //_timer = new Timer((state) =>
            //{
            //    _elapsed++;
            //    Dispatcher.Invoke(() =>
            //    {
            //        var minutes = _elapsed / 60;
            //        var seconds = _elapsed % 60;

            //        ElapsedTime.text = string.Format("{0}:{1}", (minutes < 10) ? "0" + minutes : minutes.ToString(), (seconds < 10) ? "0" + seconds : seconds.ToString());
            //    });
            //}, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
            _timer = new Timer((state) =>
            {
                _elapsed++;
                int actualNum = (100 - per) * _elapsed / per;
                Dispatcher.Invoke(() =>
                {
                    var minutes = actualNum / 60;
                    var seconds = actualNum % 60;


                    ElapsedTime.text = string.Format("???????????? {0}:{1}", (minutes < 10) ? "0" + minutes : minutes.ToString(), (seconds < 10) ? "0" + seconds : seconds.ToString());

                });
            }, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
        }

        public void StopTimer()
        {
            _timer.Dispose();
        }
    }
}