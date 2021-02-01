using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Terminal;

namespace HemeSimulation.Tasks.HemeSetup {
    public class MakeHemeXtract : ITerminalTask {
        string output = "";
        bool done = false;

        public void AddOutput(string newOutput) {
            output += newOutput + "\r\n";
        }

        public int GetID() {
            return 205;
        }

        public string GetName() {
            return "Make building HemeXtract";
        }

        public int GetNextID() {
            return -1;
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
                "cd HemeXtract;" +
                "sudo make";
        }

        public void ProcessDone() {
            done = true;
        }
    }
}