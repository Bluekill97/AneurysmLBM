using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HemeSimulation.Settings {
    public class SimulationAttributeVector : ISimulationAttribute {
        string Name;
        Vector3Double vector;
        private SimulationSetting ContainingSetting;

        public SimulationAttributeVector(string name, Vector3Double vec) {
            Name = name;
            vector = vec;
        }

        public SimulationAttributeVector(string name, Vector3 vec) {
            Name = name;
            vector = new Vector3Double(vec);
        }

        public void RegisterContainingSetting(SimulationSetting containingSetting) {
            ContainingSetting = containingSetting;
        }

        public AttributeType GetAttributeType() {
            return AttributeType.Vector;
        }

        public string GetName() {
            return Name;
        }

        public Vector3Double GetVectorDouble() {
            return vector;
        }

        public Vector3 GetVectorFloat() {
            return vector.ToVector3();
        }

        public void SetVector(Vector3Double newVec) {
            vector = newVec;
            ContainingSetting.RegisterAttributeChange(this);
        }
    }
}