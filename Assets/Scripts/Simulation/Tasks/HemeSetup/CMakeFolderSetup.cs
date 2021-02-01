using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Terminal;

namespace HemeSimulation.Tasks.HemeSetup {
    public class CMakeFolderSetup : ITerminalTask {
        string output = "";
        bool done = false;

        public void AddOutput(string newOutput) {
            output += newOutput + "\r\n";
        }

        public int GetID() {
            return 202;
        }

        public string GetName() {
            return "Heme dependency setup";
        }

        public int GetNextID() {
            return 203;
        }

        public string GetOutput() {
            return output;
        }

        public float GetProgress() {
            if (done)
                return 1;

            return 0;
        }

        public string GetTerminalCommand() {
            return "cd ~/HemePure/dep/;" +
                "sudo mkdir build;" +
                "cd build;" +
                "pwd";
        }

        public void ProcessDone() {
            done = true;
        }
    }
}

/*
 * CMake settings:
 * 
 * 
 * 
 * 
 * 
 * 
 */