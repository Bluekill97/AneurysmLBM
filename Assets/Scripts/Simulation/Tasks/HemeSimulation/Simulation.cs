using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Terminal;

namespace HemeSimulation.Tasks.HemeSimulation {
    public class Simulation : ITerminalTask {
        string output = "";
        string latestOutput = "";
        bool done = false;

        public void AddOutput(string newOutput) {
            output += newOutput + "\r\n";
            latestOutput = newOutput;
        }

        public int GetID() {
            return 404;
        }

        public string GetName() {
            return "Simulating flow..";
        }

        public int GetNextID() {
            return 405;
        }

        public string GetOutput() {
            return output;
        }

        public float GetProgress() {
            //TODO: Update progress according to latest output

            if (done)
                return 1;

            return 0;
        }

        public string GetTerminalCommand() {

            // TODO: Get parameters from settings
            return "cd ~/HemePure/src/build; " +
                "mpirun " +
                "-np 6 " + //replace 6 with value from settings
                "./hemepure " +
                "-in ~/Simulations/input.xml " + //replace input.xml with name of generated settings file
                "-out ~/Simulations/finishedSim"; //replace models name
        }

        public void ProcessDone() {
            done = true;
        }
    }
}