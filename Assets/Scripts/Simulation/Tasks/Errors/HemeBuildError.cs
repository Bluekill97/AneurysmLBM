using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Terminal;

namespace HemeSimulation.Tasks.Errors {
    public class HemeBuildError : ITerminalTask {
        public void AddOutput(string newOutput) {
            throw new System.NotImplementedException();
        }

        public int GetID() {
            return 902;
        }

        public string GetName() {
            return "Heme build error";
        }

        public int GetNextID() {
            throw new System.NotImplementedException();
        }

        public string GetOutput() {
            throw new System.NotImplementedException();
        }

        public float GetProgress() {
            throw new System.NotImplementedException();
        }

        public string GetTerminalCommand() {
            throw new System.NotImplementedException();
        }

        public void ProcessDone() {
            throw new System.NotImplementedException();
        }
    }
}