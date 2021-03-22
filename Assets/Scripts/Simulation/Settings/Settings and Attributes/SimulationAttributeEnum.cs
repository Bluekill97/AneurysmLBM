using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace HemeSimulation.Settings {
    public class SimulationAttributeEnum : ISimulationAttribute {
        private string Name;
        private Type EnumType;
        private int EnumValue;
        private SimulationSetting ContainingSetting;

        public SimulationAttributeEnum(string name, Type enumType, int enumValue) {
            Name = name;
            EnumType = enumType;
            EnumValue = enumValue;
        }

        public SimulationAttributeEnum(string name, Enum num) {
            Name = name;
            EnumType = num.GetType();
            EnumValue = GetCurrentValue(num);
        }

        public void RegisterContainingSetting(SimulationSetting containingSetting) {
            ContainingSetting = containingSetting;
        }

        public AttributeType GetAttributeType() {
            return AttributeType.Enum;
        }

        public string GetName() {
            return Name;
        }

        /// <summary>
        /// Returns all Enum instances as strings in the correct order
        /// </summary>
        /// <returns></returns>
        public List<string> GetEnumChoices() {
            List<string> choices = new List<string>();

            FieldInfo[] infos;
            infos = EnumType.GetFields();
            for (int i = 1; i < infos.Length; i++) // first entry is "value__"
                choices.Add(infos[i].Name);

            return choices;
        }

        public int GetEnumValue() {
            return EnumValue;
        }

        public void SetEnumValue(int newValue) {
            EnumValue = newValue;
            ContainingSetting.RegisterAttributeChange(this);
        }

        private int GetCurrentValue(Enum num) {
            List<string> options = GetEnumChoices();

            for (int i = 0; i < options.Count; i++) {
                if (options[i] == num.ToString())
                    return i;
            }

            return -1;
        }
    }
}