using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Terminal;

namespace HemeSimulation.Tasks.HemeBuild {
    public class DeleteBuildFolder : ITerminalTask {
        string output = "";

        public void AddOutput(string newOutput) {
            output += newOutput + "\r\n";
        }

        public int GetID() {
            return 300;
        }

        public string GetName() {
            return "Delete Heme build folder";
        }

        public int GetNextID() {
            return 301;
        }

        public string GetOutput() {
            return output;
        }

        public float GetProgress() {
            if (output == "")
                return 0;

            return 1;
        }

        public string GetTerminalCommand() {
            return "cd;" +
                "cd HemePure/src/;" +
                "sudo rm -rf build";
        }

        public void ProcessDone() {

        }
    }
}