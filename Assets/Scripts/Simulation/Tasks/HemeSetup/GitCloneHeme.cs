using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Terminal;

namespace HemeSimulation.Tasks.HemeSetup {
    public class GitCloneHeme : ITerminalTask {
        string output = "";
        bool done = false;

        public void AddOutput(string newOutput) {
            output += newOutput + "\r\n";
        }

        public int GetID() {
            return 200;
        }

        public string GetName() {
            return "Git clone Heme";
        }

        public int GetNextID() {
            return 201;
        }

        public string GetOutput() {
            return output;
        }

        public float GetProgress() {
            // since git clone updates the terminal content we only receive an error message
            // luckily no "real" error, so we just have to wait
            // thats why we give any progress information

            if (done)
                return 1;

            return 0;
        }

        public string GetTerminalCommand() {
            return "cd ~;pwd;ls;" +
                "sudo git clone \"https://github.com/UCL-CCS/HemePure.git\" \"HemePure\"";
        }

        public void ProcessDone() {
            done = true;
        }
    }
}