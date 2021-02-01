using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Terminal;

namespace HemeSimulation.Tasks.LinuxSetup {
    public class GitInstalledCheck : ITerminalTask {
        string output = "";
        bool isInstalled = false;

        public void AddOutput(string newOutput) {

            // should only be one line
            if (output != "")
                return;

            output += newOutput + "\r\n";

            if (!output.ToLower().Contains("command not found"))
                isInstalled = true;

            // version shouldn't matter
        }

        public string GetOutput() {
            return output;
        }

        public int GetID() {
            return 101;
        }

        public string GetName() {
            return "Git installed check";
        }

        public int GetNextID() {
            if (isInstalled)
                return 103;

            return 102;
        }

        public float GetProgress() {
            if (output == "")
                return 0;

            return 1;
        }

        public string GetTerminalCommand() {
            return "git --version";
        }

        public void ProcessDone() {

        }
    }
}