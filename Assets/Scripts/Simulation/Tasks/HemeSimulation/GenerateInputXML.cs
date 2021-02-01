using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Terminal;

namespace HemeSimulation.Tasks.HemeSimulation {
    public class GenerateInputXML : ITerminalTask {
        string output = "";
        bool done = false;

        public void AddOutput(string newOutput) {
            output += newOutput + "\r\n";
        }

        public int GetID() {
            return 403;
        }

        public string GetName() {
            return "Generating input settings";
        }

        public int GetNextID() {
            return 404;
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

            // TODO: fix this workaround
            // Add generation if settings are available

            // this is actually usable, generate .xml in specific folder
            // -> copy it from there here

            // adjust source path
            string resourcePath = Application.dataPath;
            //if (resourcePath.EndsWith("Assets"))
              //  resourcePath = resourcePath.Replace("Assets", "");

            string drive = resourcePath.Substring(0, 2);        // get drive name
            drive = drive.Remove(1, 1);                         // remove :

            resourcePath = resourcePath.Substring(2);           // remove windows style drive
            resourcePath = "/mnt/" + drive.ToLower() + resourcePath + "/Resources/Simulation/"; // add linux style drive

            return "[ -d \"'~/Simulations/\" ] && echo \"Directory exists\";" +
                "[ ! -d \"'~/Simulations/\" ] && echo \"Directory exists NOT\";" +
                "cd;" +
                "cd Simulations/;" +
                //"sudo cp ~/HemePure/cases/bifurcation/bifurcation_lores/input.xml input.xml";
                //"sudo cp ~/input-example.xml input.xml" +
                "sudo cp -f '" + resourcePath + "input-example.xml' input.xml";
        }

        public void ProcessDone() {
            done = true;
        }
    }
}