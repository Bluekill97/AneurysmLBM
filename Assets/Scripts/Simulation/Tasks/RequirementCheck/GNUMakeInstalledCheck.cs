using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Terminal;

namespace HemeSimulation.Tasks.LinuxSetup {
    public class GNUMakeInstalledCheck : ITerminalTask {
        string output = "";
        bool gnuInstalled = false;

        public void AddOutput(string newOutput) {
            // should only be one line
            if (output != "")
                return;

            output += newOutput + "\r\n";

            if (!output.ToLower().Contains("not found"))
                gnuInstalled = true;
        }

        public string GetOutput() {
            return output;
        }

        public int GetID() {
            return 107;
        }

        public string GetName() {
            return "GNU Make installed check";
        }

        public int GetNextID() {
            if (gnuInstalled)
                return 109;

            return 108;
        }

        public float GetProgress() {
            if (gnuInstalled)
                return 1;

            return 0;
        }

        public string GetTerminalCommand() {
            return "make --version";
        }

        public void ProcessDone() {
            gnuInstalled = true;
        }
    }
}