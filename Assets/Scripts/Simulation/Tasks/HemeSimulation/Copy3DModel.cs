using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Terminal;

namespace HemeSimulation.Tasks.HemeSimulation {
    public class Copy3DModel : ITerminalTask {
        string output = "";
        bool done = false;
        bool success = true;

        public void AddOutput(string newOutput) {
            output += newOutput + "\r\n";

            if (newOutput.Contains("NO"))
                success = false;
        }

        public int GetID() {
            return 401;
        }

        public string GetName() {
            return "Copying 3D model to simulation folder";
        }

        public int GetNextID() {
            if (!success)
                return 903;

            return 402;
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

            // TODO: fix this workaround
            // Right now we don't have a tool to to convert our files to .gmy
            // Thats why we just use these example cases for now
            // Remember to also adjust the next task when fixing the workaround
            return "cd;" +
                "cd Simulations/;" +
                "sudo cp ~/HemePure/cases/bifurcation/bifurcation_lores/bifurcation.gmy bifurcation.gmy;" +
                "[ -f bifurcation.gmy ] && echo \"GMY copy success\";" +
                "[ ! -f bifurcation.gmy ] && echo   \"GMY copy NO success.\"";
        }

        public void ProcessDone() {
            done = true;
        }
    }
}