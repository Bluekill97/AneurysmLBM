using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using UnityEngine;
using HemeSimulation.Settings;

namespace HemeSimulation.UI.Simulation.Settings {
    public class SimulationsSettingsManager : MonoBehaviour {
        [SerializeField]
        private Transform SettingEntryContainer = default;

        /// <summary>
        /// Always have to give this extra as reference, because i can't store the prefab reference in the SettingElementController itself
        /// This would else result in a self reference -> instead of instantiating the prefab, the instantiation would 
        /// happen for the instance itself -> the same object just gets copied
        /// </summary>
        [SerializeField]
        private GameObject SettingEntryPrefab = default;

        private string SimulationSettingsPath;
        private HemeLBSettings settings;


        public void Initialize(string settingsPath) {
            SimulationSettingsPath = settingsPath;
            LoadSettingsFromXML();
        }

        public void SaveSettingsToXML() {
            var serializer = new XmlSerializer(typeof(HemeLBSettings));

            // for testing 
            //string savePath = SimulationSettingsPath.Split('.')[0];
            //savePath += "-saveTest.xml";

            TextWriter writer = new StreamWriter(SimulationSettingsPath);
            serializer.Serialize(writer, settings);
            writer.Close();

            Debug.Log("Settings saved");
        }

        /// <summary>
        /// Returns Prefab to create line in settings GUI
        /// </summary>
        /// <returns> Prefab for a setting entry (one line in the GUI) </returns>
        public GameObject GetSettingEntryPrefab() {
            // This is important to recursively create the settings
            return SettingEntryPrefab;
        }

        private void LoadSettingsFromXML() {
            var serializer = new XmlSerializer(typeof(HemeLBSettings));

            using (Stream reader = new FileStream(SimulationSettingsPath, FileMode.Open)) {
                settings = (HemeLBSettings)serializer.Deserialize(reader);
            }

            ApplySettingsToUI();
        }

        private void ApplySettingsToUI() {
            var go = Instantiate(SettingEntryPrefab, SettingEntryContainer);
            go.GetComponent<SettingElementController>().Initialize(0, settings.GetSimulationSetting(), this);
        }
    }
}