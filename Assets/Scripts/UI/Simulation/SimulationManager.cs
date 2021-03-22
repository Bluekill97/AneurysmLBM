using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using HemeSimulation.UI.Simulation.Settings;

namespace HemeSimulation {
    public class SimulationManager : MonoBehaviour {
        [SerializeField]
        private SimulationsSettingsManager SimulationSettingsMgr = default;

        private string SimulationSettingFileName = "input-example.xml";

        private UnixManager UnixMgr = default;
        private bool RequirementCheckSuccessfull = false;
        private bool HemeSetupSuccessfull = false;
        private bool SimulationSuccessfull = false;

        void Start() {
            UnixMgr = new UnixManager();

            // Start simulation settings UI
            string path = GetSimulationSettingsPath();
            SimulationSettingsMgr.Initialize(path);
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

            // TODO: Eventually tell user
            if (!SimulationSuccessfull)
                Debug.Log("<color=red>Simulation was not successfull</color>");
            else
                Debug.Log("<color=yellow>Simulation successfull</color>");
        }

        /// <summary>
        /// Toggles the visibility of the Simulation Settings window
        /// </summary>
        public void SimSettingsToggle() {
            //Debug.Log("Toggled SimulationSettings window");

            SimulationSettingsMgr.gameObject.SetActive(!SimulationSettingsMgr.gameObject.activeSelf);
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

            // TODO: Eventually tell user about failure
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

            // TODO: Eventually tell user
            if (!HemeSetupSuccessfull)
                Debug.Log("<color=red>Heme setup was not successfull, can't start any Simulations</color>");
            else
                Debug.Log("<color=yellow>Heme setup was not successfull</color>");
        }

        private string GetSimulationSettingsPath() {
            string path = Application.dataPath + "/Resources/Simulation/" + SimulationSettingFileName;
            //Debug.Log(path);
            return path;
        }
    }
}