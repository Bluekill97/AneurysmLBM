using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Terminal;

namespace HemeSimulation.Tasks.HemeBuild {
    public class BuildCMakeConfiguration : ITerminalTask {
        string output = "";
        bool done = false;

        public void AddOutput(string newOutput) {
            output += newOutput + "\r\n";
        }

        public int GetID() {
            return 302;
        }

        public string GetName() {
            return "Build CMake Configuration";
        }

        public int GetNextID() {
            return 303;
        }

        public string GetOutput() {
            return output;
        }

        public float GetProgress() {
            if (!done)
                return 0;

            return 1;
        }

        public string GetTerminalCommand() {
            //TODO: get settings and add automated (e.g. like CMAKE_INSTALL_PREFIX but not hardcoded)

            // I don't why but the working directory could be changed to the Unity projects path, 
            // so we change it to the Heme folder again
            return "cd;" +
                "cd HemePure/src/build;" +
                "sudo cmake .. -D CMAKE_INSTALL_PREFIX=\"~/bin\"";
        }

        public void ProcessDone() {
            done = true;
        }
    }
}