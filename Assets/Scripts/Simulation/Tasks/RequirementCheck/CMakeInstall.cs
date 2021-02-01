using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Terminal;

namespace HemeSimulation.Tasks.LinuxSetup {
    public class CMakeInstall : ITerminalTask {
        string output = "";
        bool done = false;

        public void AddOutput(string newOutput) {
            output += newOutput + "\r\n";
        }

        public string GetOutput() {
            return output;
        }

        public int GetID() {
            return 110;
        }

        public string GetName() {
            return "CMake install";
        }

        public int GetNextID() {
            return 111;
        }

        public float GetProgress() {
            if (done)
                return 1;

            return 0;
        }

        public string GetTerminalCommand() {
            return "echo y| sudo apt-get install cmake; " +
                "echo y| sudo apt-get install cmake-curses-gui";
        }

        public void ProcessDone() {
            done = true;
        }
    }
}