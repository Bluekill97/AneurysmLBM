using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Terminal;

namespace HemeSimulation.Tasks.HemeSimulation {
    public class SimulationFolderSetup : ITerminalTask {
        string output = "";
        bool done = false;

        public void AddOutput(string newOutput) {
            output += newOutput + "\r\n";
        }

        public int GetID() {
            return 400;
        }

        public string GetName() {
            return "Simulation folder setup";
        }

        public int GetNextID() {
            return 401;
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

            // TODO: ask/ evaluate if simulations should be saved here
            // If yes: give each simulation a unique name instead of "simsim"
            // But remember: the highres example had 9.3GB size, huge storage 
            // space might be required
            return "cd;" +
                //"mkdir Simulations;" +
                "rm -rf Simulations;" +
                "mkdir Simulations";
        }

        public void ProcessDone() {
            done = true;
        }
    }
}