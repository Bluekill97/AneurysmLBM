using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HemeSimulation.Settings {
    public class SimulationAttributeString : ISimulationAttribute {
        string Name;
        string Value;
        private SimulationSetting ContainingSetting;

        public SimulationAttributeString(string name, string value) {
            Name = name;
            Value = value;
        }
        public void RegisterContainingSetting(SimulationSetting containingSetting) {
            ContainingSetting = containingSetting;
        }

        public AttributeType GetAttributeType() {
            return AttributeType.String;
        }

        public string GetName() {
            return Name;
        }

        public string GetStringValue() {
            return Value;
        }

        public void SetString(string newStr) {
            Value = newStr;
            ContainingSetting.RegisterAttributeChange(this);
        }
    }
}