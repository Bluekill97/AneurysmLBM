using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Terminal;

namespace HemeSimulation.Tasks.HemeSetup {
    public class CMakeConfiguration : ITerminalTask {
        string output = "";
        bool done = false;

        public void AddOutput(string newOutput) {
            output += newOutput + "\r\n";
        }

        public int GetID() {
            return 203;
        }

        public string GetName() {
            return "CMake configuration";
        }

        public int GetNextID() {
            return 204;
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
            //TODO: get settings and add automated (e.g. like CMAKE_INSTALL_PREFIX but not hardcoded)

            // I don't why but the working directory could be changed to the Unity projects path, 
            // so we change it to the Heme folder again
            return "cd;" +
                "cd HemePure/dep/build;" +
                "sudo cmake .. -D CMAKE_INSTALL_PREFIX=\"~/bin\"";
        }

        public void ProcessDone() {
            done = true;
        }
    }
}