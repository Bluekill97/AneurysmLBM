using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Terminal;

namespace HemeSimulation.Tasks.HemeSetup {
    public class GitCloneHemeXtract : ITerminalTask {
        string output = "";
        bool done = false;

        public void AddOutput(string newOutput) {
            output += newOutput + "\r\n";
        }

        public int GetID() {
            return 201;
        }

        public string GetName() {
            return "Git clone HemeXtract";
        }

        public int GetNextID() {
            return 202;
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

            // TODO: check if folder already exists
            return "cd ~;pwd;ls;" +
                "sudo git clone \"https://github.com/UCL-CCS/hemeXtract.git\" \"HemeXtract\"";
        }

        public void ProcessDone() {
            done = true;
        }
    }
}