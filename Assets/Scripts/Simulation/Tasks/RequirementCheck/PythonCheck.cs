using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Terminal;

namespace HemeSimulation.Tasks.LinuxSetup {
    public class PythonCheck : ITerminalTask {
        string output = "";
        bool pythonVersionCorrect = false;

        public void AddOutput(string newOutput) {
            Debug.Log("Python check 1");

            // should only be one line
            if (output != "")
                return;

            output += newOutput + "\r\n";

            Debug.Log("Python check 2");

            if (output.Contains("not found")) 
                return;

            try {
                // version has to be 2, NOT 3
                string wholeVersion = output.Split(' ')[1];
                Debug.Log("python whole version: " + wholeVersion);
                int majorVersion = int.Parse(wholeVersion.Split('.')[0]);
                if (majorVersion == 2)
                    pythonVersionCorrect = true;
            } catch {
                pythonVersionCorrect = false;
            }
        }

        public string GetOutput() {
            return output;
        }

        public int GetID() {
            return 111;
        }

        public string GetName() {
            return "Python version check";
        }

        public int GetNextID() {
            if (!pythonVersionCorrect)
                return 112;

            return -1;
        }

        public float GetProgress() {
            if (output != "")
                return 1;

            return 0;
        }

        public string GetTerminalCommand() {
            return "python --version";
        }

        public void ProcessDone() {
          
        }
    }

}