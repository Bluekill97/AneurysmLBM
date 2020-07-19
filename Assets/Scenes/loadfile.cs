using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadfile : MonoBehaviour
{
    public GameObject GO1;
    public GameObject GO2;
    public GameObject GO3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void showPatient()
    {
        GO3.SetActive(false);
        GO2.SetActive(false);
        GO1.SetActive(true);
        Debug.Log("Patientenauswahl");
    }
}
