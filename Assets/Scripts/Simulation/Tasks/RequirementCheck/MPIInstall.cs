using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Terminal;

namespace HemeSimulation.Tasks.LinuxSetup {
    public class MPIInstall : ITerminalTask {
        string output = "";
        bool mpiInstalled = false;

        public void AddOutput(string newOutput) {
            output += newOutput + "\r\n";
        }

        public string GetOutput() {
            return output;
        }

        public int GetID() {
            return 106;
        }

        public string GetName() {
            return "MPI install";
        }

        public int GetNextID() {
            return 107;
        }

        public float GetProgress() {
            if (mpiInstalled)
                return 1;

            return 0;
        }

        public string GetTerminalCommand() {
            return "echo y| sudo apt-get install libopenmpi-dev";// openmpi-bin ";
        }

        public void ProcessDone() {
            mpiInstalled = true;
        }
    }
}