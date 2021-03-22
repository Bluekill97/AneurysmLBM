using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace HemeSimulation.Settings {

    [XmlRoot("hemelbsettings")]
    public class HemeLBSettings : ISimulationSetting{

        /*
         * Note: Classes with complex types like Vector3 as members variables have to implement IXmlSerializable (e.g. Origin class)
         * Useful websites regarding XML:
         * http://wiki.unity3d.com/index.php?title=Saving_and_Loading_Data:_XmlSerializer
         * https://stackoverflow.com/questions/279534/proper-way-to-implement-ixmlserializable
         * https://www.codeproject.com/Articles/43237/How-to-Implement-IXmlSerializable-Correctly
         */

        [XmlAttribute("version")]
        public int Version = 3;

        public Simulation simulation;
        public Geometry geometry;
        public InitialConditions initialconditions;
        public Monitoring monitoring;

        [XmlArray("inlets")]
        [XmlArrayItem("inlet")]
        public List<Inlet> Inlets = new List<Inlet>();

        [XmlArray("outlets")]
        [XmlArrayItem("outlet")]
        public List<Outlet> Outlets = new List<Outlet>();
        
        public Particles particles;
        public BodyForces bodyForces;
        public BoundaryConditions boundaryConditions;
        public Properties properties;

        public void ApplySimulationSetting(SimulationSetting applySetting) {
            Version = (int)((SimulationAttributeNumber)applySetting.Attributes[0]).GetNumber();

            simulation.ApplySimulationSetting(applySetting.SubSettings[0]);
            geometry.ApplySimulationSetting(applySetting.SubSettings[1]);
            initialconditions.ApplySimulationSetting(applySetting.SubSettings[2]);
            monitoring.ApplySimulationSetting(applySetting.SubSettings[3]);
            particles.ApplySimulationSetting(applySetting.SubSettings[4]);
            bodyForces.ApplySimulationSetting(applySetting.SubSettings[5]);
            boundaryConditions.ApplySimulationSetting(applySetting.SubSettings[6]);
            properties.ApplySimulationSetting(applySetting.SubSettings[7]);

            // Unfortunately we don't know how many In and Outlets we expect and 
            List<SimulationSetting> inTemp = new List<SimulationSetting>();
            List<SimulationSetting> outTemp = new List<SimulationSetting>();
            Inlet inl = new Inlet();
            Outlet outl = new Outlet();
            for (int i = 8; i < applySetting.SubSettings.Count; i++) {

                if (applySetting.SubSettings[i].IsEqual(inl)) {
                    inTemp.Add(applySetting.SubSettings[i]);
                    Debug.Log("<color=blue SHEEEESH1 </color>");
                }

                else if (applySetting.SubSettings[i].IsEqual(outl)) {
                    outTemp.Add(applySetting.SubSettings[i]);
                    Debug.Log("<color=blue SHEEEESH1 </color>");
                }

                else {
                    Debug.Log("<color=red>Couldn't assign SimulationSetting in HemeLBSettings</color>");
                }
            }

            for(int i = 0; i < inTemp.Count; i++) { 
                // Add fields if necessary
                if (Inlets.Count <= i)
                    Inlets.Add(new Inlet());

                Inlets[i].ApplySimulationSetting(inTemp[i]);
            }

            for (int i = 0; i < outTemp.Count; i++) {
                // Add fields if necessary
                if (Outlets.Count <= i)
                    Outlets.Add(new Outlet());

                Outlets[i].ApplySimulationSetting(outTemp[i]);
            }

            // Remove fields in case they got deleted
            int countDiff = Inlets.Count - inTemp.Count;
            if (countDiff > 0)
                Inlets.RemoveRange(inTemp.Count, countDiff);

            // Remove fields in case they got deleted
            countDiff = Outlets.Count - outTemp.Count;
            if (countDiff > 0)
                Outlets.RemoveRange(outTemp.Count, countDiff);
        }

        public SimulationSetting GetSimulationSetting() {
            var att = new SimulationAttributeNumber("Version", Version);

            List<SimulationSetting> subSetts = new List<SimulationSetting>();
            subSetts.Add(simulation.GetSimulationSetting());
            subSetts.Add(geometry.GetSimulationSetting());
            subSetts.Add(initialconditions.GetSimulationSetting());
            subSetts.Add(monitoring.GetSimulationSetting());
            subSetts.Add(particles.GetSimulationSetting());
            subSetts.Add(bodyForces.GetSimulationSetting());
            subSetts.Add(boundaryConditions.GetSimulationSetting());
            subSetts.Add(properties.GetSimulationSetting());

            // Add all In- and Outlets to the subsettings
            foreach (var inlet in Inlets)
                subSetts.Add(inlet.GetSimulationSetting());

            foreach (var outlet in Outlets)
                subSetts.Add(outlet.GetSimulationSetting());

            return new SimulationSetting(this, "HemeLBSettings",
                new List<ISimulationAttribute>() { att },
                subSetts);
        }
    }
}