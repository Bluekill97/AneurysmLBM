using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Terminal;

namespace HemeSimulation.Tasks.HemeSimulation {
    public class Convert3DModel : ITerminalTask {
        string output = "";
        bool done = false;

        public void AddOutput(string newOutput) {
            output += newOutput + "\r\n";
        }

        public int GetID() {
            return 402;
        }

        public string GetName() {
            return "Converting 3D model to .gmy";
        }

        public int GetNextID() {
            return 403;
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

            // TODO: fix previous workaround
            // Because of our workaround from teh previous step we don't need this for now
            // If we have a conversion tool change this
            return "cd;" +
                "pwd";
        }

        public void ProcessDone() {
            done = true;
        }
    }
}