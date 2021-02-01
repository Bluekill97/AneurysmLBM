using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Terminal;

namespace HemeSimulation.Tasks.LinuxSetup {
    public class PythonDowngrade : ITerminalTask {
        string output = "";
        bool done = false;

        public void AddOutput(string newOutput) {
            output = newOutput + "\r\n";
        }

        public string GetOutput() {
            return output;
        }

        public int GetID() {
            return 112;
        }

        public string GetName() {
            return "Python downgrade to version 2";
        }

        public int GetNextID() {
            return -1;
        }

        public float GetProgress() {
            if (done)
                return 1;

            return 0;
        }

        public string GetTerminalCommand() {

            // keep both, just use python2 when "python" is entered
            return "echo y|sudo apt-get install python2;" +
                "sudo update-alternatives --install /usr/bin/python python /usr/bin/python2 1;" +
                "sudo update-alternatives --install /usr/bin/python python /usr/bin/python3 2;" +
                "echo 1| sudo update-alternatives --config python";     //select update alternative (choose 1 because 0 is the python3 auto-mode)
        }

        public void ProcessDone() {
            done = true;
        }
    }
}