using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Terminal;

namespace HemeSimulation.Tasks.LinuxSetup {
    public class GitInstall : ITerminalTask {
        string output = "";
        bool done = false;

        public void AddOutput(string newOutput) {
            output += newOutput + "\r\n";
        }

        public string GetOutput() {
            return output;
        }

        public int GetID() {
            return 102;
        }

        public string GetName() {
            return "Git Install";
        }

        public int GetNextID() {
            return 103;
        }

        public float GetProgress() {
            return done ? 1 : 0;
        }

        public string GetTerminalCommand() {
            return "echo y|sudo apt-get install git";
        }

        public void ProcessDone() {
            done = true;
        }
    }
}