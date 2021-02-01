using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Terminal;

namespace HemeSimulation.Tasks.LinuxSetup {

    public class GCCInstall : ITerminalTask {
        string output = "";
        bool gccInstalled = false;

        public void AddOutput(string newOutput) {
            output += newOutput + "\r\n";
        }

        public string GetOutput() {
            return output;
        }

        public int GetID() {
            return 104;
        }

        public string GetName() {
            return "GCC install";
        }

        public int GetNextID() {
            return 105;
        }

        public float GetProgress() {
            if (gccInstalled)
                return 1;

            return 0;
        }

        public string GetTerminalCommand() {
            return "echo y| sudo apt-get install gcc; echo y| sudo apt-get install g++";
        }

        public void ProcessDone() {
            gccInstalled = true;
        }
    }
}