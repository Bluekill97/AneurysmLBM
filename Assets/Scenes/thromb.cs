using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thromb : MonoBehaviour
{
    public GameObject[] thrombs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void showThromb()
    {
        foreach (GameObject thromb in thrombs)
        {
            if (thromb.activeSelf)
            {
                thromb.SetActive(false);
                Debug.Log("Thrombenauswahl");
            }
            else
            {
                thromb.SetActive(true);
                Debug.Log("Thrombenauswahl");
            }
        }
    }
}
