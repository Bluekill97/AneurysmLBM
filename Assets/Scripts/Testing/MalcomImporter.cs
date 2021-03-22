using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Parabox.Stl;

public class MalcomImporter : MonoBehaviour {
    public Material MeshMaterial = default;

    string assetPath = @"D:\Unity\Git Projekte\AneurysmLBM\Assets\Resources\STL Test Data\fillenium malcon";
    string assetName = "solo.stl";

    // Start is called before the first frame update
    void Start() {
        ImportMalcom();
    } 

    public void ShowMesh(int number) {
        Debug.Log("Selected " + number);
    }

    private void ImportMalcom() {
        Mesh[] meshes = Importer.Import(string.Format("{0}\\{1}", assetPath, assetName));

        int counter = 0;
        foreach(Mesh m in meshes) {
            var go = newMeshObj(counter++);
            (go.GetComponent<MeshFilter>() as MeshFilter).mesh = m;
        }

        transform.Rotate(Vector3.back * -90f);
        transform.localScale = Vector3.one * 0.05f;
    }

    private GameObject newMeshObj(int number) {
        GameObject go = new GameObject();
        go.name = "Mesh " + number;
        go.transform.SetParent(transform);

        MeshFilter mf = go.AddComponent<MeshFilter>() as MeshFilter;
        MeshRenderer mr = go.AddComponent<MeshRenderer>() as MeshRenderer;

        mr.material = MeshMaterial;

        return go;
    }
}
