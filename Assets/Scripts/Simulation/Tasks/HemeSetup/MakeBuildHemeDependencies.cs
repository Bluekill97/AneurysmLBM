using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Terminal;

namespace HemeSimulation.Tasks.HemeSetup {
    public class MakeBuildHemeDependencies : ITerminalTask {
        string output = "";
        bool done = false;

        public void AddOutput(string newOutput) {
            output += newOutput + "\r\n";
        }

        public int GetID() {
            return 204;
        }

        public string GetName() {
            return "Make build Heme dependencies";
        }

        public int GetNextID() {
            return 205;
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
            return "cd;" +
                "cd HemePure/dep/build;" + 
                "sudo make";
        }

        public void ProcessDone() {
            done = true;
        }
    }
}