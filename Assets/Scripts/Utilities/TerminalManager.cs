using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using UnityEngine;

namespace Utilities.Terminal {

    // Helpful article:
    // https://weblog.west-wind.com/posts/2020/May/19/Using-WSL-to-Launch-Several-Bash-Command-from-an-Application

    public class TerminalManager {
        public string DataOutput { get; private set; } = "";
        public string ErrorOutput { get; private set; } = "";

        /// <summary>
        /// Indicates if ProcessErrorReceived was called. Keep in mind that updated terminal output (for the UI, 
        /// e.g. an updating progress bar) gets returned as an error although everything could go correctly.
        /// </summary>
        private bool EndedWithError = false;

        private Process process = default;
        private TaskCompletionSource<bool> taskHandled;

        private ITerminalTask currentTask;

        bool debugMode = true;


        public async Task<bool> RunAsync(ITerminalTask ttask) {
            currentTask = ttask;
            EndedWithError = false;

            taskHandled = new TaskCompletionSource<bool>();

            // don't put into using(), else we get no output because the process object got destructed
            process = new Process();

            // remember: after starting wsl like this the working folder is the path to this unity project
            process.StartInfo.FileName = "wsl.exe";//"cmd.exe";
            process.StartInfo.Arguments = currentTask.GetTerminalCommand();

            // redirect standard outout -> required setting set UseShellExecute false
            process.StartInfo.UseShellExecute = false;

            // redirect all inputs from the window to us
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardError = true;
            process.OutputDataReceived += ProcessDataReceived;
            process.ErrorDataReceived += ProcessErrorReceived;

            // set EnableRaisingEvents true to use process.Exited
            process.EnableRaisingEvents = true;
            process.Exited += CommandExit;

            // since everything is redirected we dont need to see the window anymore
            //process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.CreateNoWindow = true;

            try {
                process.Start();

                //impotant to receive redirected input
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
            }
            catch (Exception e) {

                if (debugMode)
                    UnityEngine.Debug.Log($"An error occurred trying to execute \"{currentTask.GetTerminalCommand()}\":\n{e.Message}");

                ErrorOutput += currentTask.GetTerminalCommand() + "\n" + e.Message;
                currentTask.AddOutput(ErrorOutput);

                Utilities.ErrorLog.LogError(e);
                return false;
            }

            // wait for command finish
            await Task.WhenAny(taskHandled.Task);


            if (!EndedWithError) 
                currentTask.AddOutput(DataOutput);          
            else 
                currentTask.AddOutput(ErrorOutput);


            return true;
        }

        private void ProcessDataReceived(object sender, DataReceivedEventArgs e) {

            // null data means we've received everything
            if (e.Data == null) {
                process.CancelOutputRead();
                process.CancelErrorRead();
                //process.Kill();

                taskHandled.TrySetResult(true);
                return;
            }
            else {
                if (debugMode)
                    UnityEngine.Debug.Log("Message received ---" + e.Data + "---");

                DataOutput += e.Data;
            }
        }

        private void ProcessErrorReceived(object sender, DataReceivedEventArgs e) {
            // null data means we've received everything
            if (e.Data == null) {
                process.CancelOutputRead();
                process.CancelErrorRead();

                taskHandled.TrySetResult(true);
                return;
            }

            EndedWithError = true;

            if (debugMode)
                UnityEngine.Debug.Log("Error received: " + e.Data);

            ErrorOutput += e.Data;
        }


        private void CommandExit(object sender, System.EventArgs e) {
            if (debugMode)
                UnityEngine.Debug.Log("CommandExit");

            currentTask.ProcessDone();
        }

    }
}
