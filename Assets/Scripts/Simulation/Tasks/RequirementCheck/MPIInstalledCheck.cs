using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Terminal;

namespace HemeSimulation.Tasks.LinuxSetup {
    public class MPIInstalledCheck : ITerminalTask {
        string output = "";
        bool mpiInstalled = false;

        public void AddOutput(string newOutput) {
            output += newOutput + "\r\n";
        }

        public string GetOutput() {
            return output;
        }

        public int GetID() {
            return 105;
        }

        public string GetName() {
            return "MPI installed check";
        }

        public int GetNextID() {
            if (mpiInstalled)
                return 107;

            return 106;
        }

        public float GetProgress() {
            if (output == "")
                return 0;

            return 1;
        }

        public string GetTerminalCommand() {
            return "mpiexec --version";
        }

        public void ProcessDone() {
            CheckVersion();
        }

        private void CheckVersion() {
            if (!output.ToLower().Contains("not found")) {
                mpiInstalled = true;
                return;
            }


            /* //we already updated, so the newest version should be installed
            // split lines
            string output2 = output.Replace("\r\n", ";");
            var versionOpenMPI = output2.Split(';')[1];

            // get version substring
            int startIndex = versionOpenMPI.IndexOf("now ") + 4;
            versionOpenMPI = versionOpenMPI.Substring(startIndex, 5);

            // check version, 1.10.2 & above
            var numbers = versionOpenMPI.Split('.').Select(int.Parse).ToArray(); //string[] to int[]
            if (numbers[0] > 1)
                mpiInstalled = true;
            else if (numbers[0] == 1 && numbers[1] > 10)
                mpiInstalled = true;
            else if (numbers[0] == 1 && numbers[1] == 10 && numbers[2] >= 2)
                mpiInstalled = true;
            */
        }
    }
}