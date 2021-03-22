using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using UnityEngine;

namespace HemeSimulation {
    public class Vector3Double : IXmlSerializable{
        public double X;
        public double Y;
        public double Z;

        public Vector3Double() { }

        public Vector3Double(double x, double y, double z) {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3Double(Vector3 vec) {
            X = vec.x;
            Y = vec.y;
            Z = vec.z;
        }

        public Vector3 ToVector3() {
            Vector3 newVec = new Vector3(
                (float)X,
                (float)Y,
                (float)Z);

            return newVec;
        }

        public XmlSchema GetSchema() {
            return (null);
        }

        public void ReadXml(XmlReader reader) {
            throw new System.NotImplementedException();
        }

        public void WriteXml(XmlWriter writer) {
            throw new System.NotImplementedException();
        }

        public override string ToString() {
            string s = "(" +
                DoubleToString(X) + "," +
                DoubleToString(Y) + "," +
                DoubleToString(Z) + ")";

            return s;
        }

        public void ParseString(string rString) {
            string[] temp = rString.Substring(1, rString.Length - 2).Split(',');

            X = double.Parse(temp[0].Replace(".", ","), NumberStyles.Float);
            Y = double.Parse(temp[1].Replace(".", ","), NumberStyles.Float);
            Z = double.Parse(temp[2].Replace(".", ","), NumberStyles.Float);
        }

        public static string DoubleToString(double d) {
            //return d.ToString();
            return (d).ToString("0.00000e0", CultureInfo.InvariantCulture);
        }
    }
}