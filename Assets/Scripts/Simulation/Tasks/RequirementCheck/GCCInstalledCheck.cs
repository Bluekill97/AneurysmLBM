using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Terminal;

namespace HemeSimulation.Tasks.LinuxSetup {
    public class GCCInstalledCheck : ITerminalTask {
        string output = "";
        bool gccInstalled = false;

        public void AddOutput(string newOutput) {
            // only first line matters
            if (output != "")
                return;

            output += newOutput + "\r\n";

            if (!output.ToLower().Contains("command not found")) {
                gccInstalled = true;
                return;
            }

            /* //we already updated, so the newest version should be installed
            // version is at the end
            var s = output.Split(' ');
            string version = s[s.Length - 1];

            // check version, 7.4.0 & above               
            var numbers = version.Split('.').Select(int.Parse).ToArray(); //string[] to int[]
            if (numbers[0] > 7)
                gccInstalled = true;
            else if (numbers[0] == 7 && numbers[1] >= 4)
                gccInstalled = true;
            */
        }

        public string GetOutput() {
            return output;
        }

        public int GetID() {
            return 103;
        }

        public string GetName() {
            return "GCC install check";
        }

        public int GetNextID() {
            if (!gccInstalled)
                return 104;

            return 105;
        }

        public float GetProgress() {
            if (output == "")
                return 0;


            return 1;
        }

        public string GetTerminalCommand() {
            return "gcc --version;g++ --version";
        }

        public void ProcessDone() {
            
        }
    }
}