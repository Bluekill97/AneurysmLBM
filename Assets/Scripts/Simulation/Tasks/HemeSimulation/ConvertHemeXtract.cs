using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Terminal;

namespace HemeSimulation.Tasks.HemeSimulation {
    public class ConvertHemeXtract : ITerminalTask {
        string output = "";
        bool done = false;

        public void AddOutput(string newOutput) {
            output += newOutput + "\r\n";
        }

        public int GetID() {
            return 405;
        }

        public string GetName() {
            return "Converting data with HemeXtract";
        }

        public int GetNextID() {
            return 406;
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

            return "cd ~/HemeXtract/;" +
                "sudo ./hemeXtract " +
                "-X " +
                "~/Simulations/finishedSim/Extracted/whole.dat " + //TODO: eventually replace whole.det with filename from settings file
                "> " +
                "~/Simulations/readableOutput.txt"; // Change readableOutputs name if you want, just stay consistent
        }

        public void ProcessDone() {
            done = true;
        }
    }
}