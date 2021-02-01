using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Terminal;

namespace HemeSimulation.Tasks.LinuxSetup {
    public class LinuxAptUpdateUpgrade : ITerminalTask {
        string output = "";
        bool done = false;

        public void AddOutput(string newOutput) {
            output += newOutput + "\r\n";
        }

        public string GetOutput() {
            return output;
        }

        public int GetID() {
            return 100;
        }

        public string GetName() {
            return "Check in Linux for package updates and upgrades";
        }

        public int GetNextID() {
            return 101;
        }

        public float GetProgress() {
            return done ? 0 : 1;
        }

        public string GetTerminalCommand() {
            return "sudo apt-get update; echo y|sudo apt-get upgrade";
        }

        public void ProcessDone() {
            done = true;
        }
    }
}
