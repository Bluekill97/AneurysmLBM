using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Terminal;

namespace HemeSimulation.Tasks.LinuxSetup {
    public class GNUMakeInstall : ITerminalTask {
        string output = "";
        bool gnuInstalled = false;

        public void AddOutput(string newOutput) {
            output += newOutput + "\r\n";
        }

        public string GetOutput() {
            return output;
        }

        public int GetID() {
            return 108;
        }

        public string GetName() {
            return "GNU Make installed check";
        }

        public int GetNextID() {
            return 109;
        }

        public float GetProgress() {
            if (gnuInstalled)
                return 1;

            return 0;
        }

        public string GetTerminalCommand() {
            return "echo y| sudo apt-get install make";
        }

        public void ProcessDone() {
            gnuInstalled = true;
        }
    }
}