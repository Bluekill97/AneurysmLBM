using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HemeSimulation.Settings;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Reflection;


public class XMLTest : MonoBehaviour {

    //string path = @"D:\Unity\Git Projekte\AneurysmLBM\Assets\Resources\Simulation\";
    //string pathAndName = @"D:\Unity\Git Projekte\AneurysmLBM\testXML.xml";
    string pathAndName2 = @"D:\Unity\Git Projekte\AneurysmLBM\Assets\Resources\Simulation\input-example.xml";
    string pathAndName3 = @"D:\Unity\Git Projekte\AneurysmLBM\Assets\Resources\Simulation\input-example2.xml";

    HemeLBSettings sett;

    // Start is called before the first frame update
    void Start() {

        /*
        var e = AttributeType.String;
        System.Enum en = e;
        System.Type t = en.GetType();
        Debug.Log(t);
        Debug.Log(en.ToString());
        //Debug.Log((t)en);

        FieldInfo[] infos;
        infos = t.GetFields();
        foreach (FieldInfo fi in infos) {
            Debug.Log(fi.Name);
        }  */


        XmlTestRead();
        Debug.Log("-----------------------------");
        XmlTestWrite();
        //XmlTestValidate();

        //Debug.Log(Vector3.zero.ToString());
        //HemeSimulation.Vector3Double vd = new HemeSimulation.Vector3Double(2.23, 0.0001345, -0.707107);
        //Debug.Log(vd.ToString());
    }

    private void XmlTestWrite() {
        //string filename = "testXML.xml";

        /*sett = new HemeLBSettings();
        sett.simulation = new Simulation();
        sett.simulation.step_length = new StepLength();
        sett.simulation.steps = new Steps();
        sett.simulation.stresstype = new StressType();
        sett.simulation.voxel_size = new VoxelSize();
        sett.simulation.origin = new Origin(); */

        var serializer = new XmlSerializer(typeof(HemeLBSettings));
        TextWriter writer = new StreamWriter(pathAndName3);
        serializer.Serialize(writer, sett);
        writer.Close();

        Debug.Log("XML fertig");
    }

    private void XmlTestRead() {
        //string filename = "input-example.xml";

        var serializer = new XmlSerializer(typeof(HemeLBSettings));

        using(Stream reader = new FileStream(pathAndName2, FileMode.Open)) {
            sett = (HemeLBSettings)serializer.Deserialize(reader);
        }

        /*
        Debug.Log(sett.Version);
        Debug.Log(sett.simulation.step_length.Value);
        Debug.Log(sett.simulation.step_length.Units);
        Debug.Log(sett.simulation.steps.Value);
        Debug.Log(sett.simulation.stresstype.Value);
        Debug.Log(sett.simulation.voxel_size.Value);
        Debug.Log(sett.simulation.origin.Units); */
    }

    private void XmlTestValidate() {

        XmlReaderSettings settings = new XmlReaderSettings();
        settings.Schemas.Add("http://www.contoso.com/books", "contosoBooks.xsd");
        settings.ValidationType = ValidationType.Schema;
        XmlReader reader = XmlReader.Create(pathAndName2, settings);
        XmlDocument document = new XmlDocument();
        document.Load(reader);

        ValidationEventHandler eventHandler = new ValidationEventHandler(ValidationEventHandler);

        // the following call to Validate succeeds.
        document.Validate(eventHandler);

    }

    static void ValidationEventHandler(object sender, ValidationEventArgs e) {
        switch (e.Severity) {
            case XmlSeverityType.Error:
                Debug.Log("Error: " + e.Message);
                break;
            case XmlSeverityType.Warning:
                Debug.Log("Warning " + e.Message);
                break;
        }
    }
}
