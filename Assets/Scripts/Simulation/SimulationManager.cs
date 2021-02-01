using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace HemeSimulation {
    public class SimulationManager : MonoBehaviour {
        private UnixManager UnixMgr = default;
        private bool RequirementCheckSuccessfull = false;
        private bool HemeSetupSuccessfull = false;
        private bool SimulationSuccessfull = false;

        void Start() {
            UnixMgr = new UnixManager();
        }

        public async void SimStartAsync() {
            Debug.Log("Simulation Start");

            try {
                SimulationSuccessfull = await UnixMgr.HemeSimStartAsync();
            }
            catch (System.Exception e) {
                Debug.Log(e);
                Utilities.ErrorLog.LogError(e);
            }


            if (!SimulationSuccessfull)
                Debug.Log("<color=red>Simulation was not successfull</color>");
            else
                Debug.Log("<color=yellow>Simulation successfull</color>");
        }

        public void SimSettings() {

        }

        public async void SimSetupAsync() {
            Debug.Log("Linux Check");

            try {
                RequirementCheckSuccessfull = await UnixMgr.UnixRequirementCheckAsync();
            }
            catch (System.Exception e) {
                Debug.Log(e);
                Utilities.ErrorLog.LogError(e);
            }


            if (!RequirementCheckSuccessfull)
                Debug.Log("<color=red>Requirement check was not successfull, can't start any Simulations</color>");
            else
                Debug.Log("<color=yellow>Requirement check was not successfull</color>");



            Debug.Log("Heme setup");
            try {
                HemeSetupSuccessfull = await UnixMgr.HemeSimSetupAsync();
            }
            catch (System.Exception e) {
                Debug.Log(e);
                Utilities.ErrorLog.LogError(e);
            }

            if (!HemeSetupSuccessfull)
                Debug.Log("<color=red>Heme setup was not successfull, can't start any Simulations</color>");
            else
                Debug.Log("<color=yellow>Heme setup was not successfull</color>");
        }
    }
}