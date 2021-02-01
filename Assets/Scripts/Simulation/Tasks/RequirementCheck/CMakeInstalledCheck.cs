using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Terminal;

namespace HemeSimulation.Tasks.LinuxSetup {
    public class CMakeInstalledCheck : ITerminalTask {
        string output = "";
        bool cmakeInstalled = false;

        public void AddOutput(string newOutput) {
            output += newOutput + "\r\n";
        }

        public string GetOutput() {
            return output;
        }

        public int GetID() {
            return 109;
        }

        public string GetName() {
            return "CMake installed check";
        }

        public int GetNextID() {
            if (cmakeInstalled)
                return 111;

            return 110;
        }

        public float GetProgress() {
            if (cmakeInstalled)
                return 1;

            return 0;
        }

        public string GetTerminalCommand() {
            //install ccmake just in case someone has to manually change something
            return "cmake --version; apt-cache policy cmake-curses-gui";
        }

        public void ProcessDone() {
            Check();
        }

        private void Check() {
            Debug.Log("-----command not found:" + output.ToLower().Contains("command not found"));
            Debug.Log("-----Installed: (none):" + output.ToLower().Contains("Installed: (none)"));

            if (!(output.ToLower().Contains("command not found") ||  // cmake command known
                output.ToLower().Contains("Installed: (none)")))    // package cmake-curses-gui installed
                cmakeInstalled = true;
        }
    }
}