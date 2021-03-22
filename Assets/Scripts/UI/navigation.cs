using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;

public class navigation : MonoBehaviour
{
    public GameObject[] Vessels;
    public GameObject ScreenForTwoD;
    public GameObject PlaneForTwoDL;
    public GameObject PlaneForTwoDR;
    public GameObject ModelP1;
    Vector3 scale;
    Vector3 position;
    Vector3 planepositionL;
    Vector3 planepositionR;
    public float speed = 0.05f;
    List<Vector3> originalPosition = new List<Vector3>();
    Quaternion originalRotation;
    List<Vector3> originalScale = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake()
    {
        foreach (GameObject vessel in Vessels)
        {
            originalPosition.Add(vessel.transform.localPosition);
            originalScale.Add(vessel.transform.localScale);
            originalRotation = vessel.transform.localRotation;
        }


    }

    public void show2D()
    {
        if (ScreenForTwoD.activeSelf)
        {
            ScreenForTwoD.SetActive(false);
            PlaneForTwoDL.SetActive(false);
            PlaneForTwoDR.SetActive(false);
            ModelP1.SetActive(false);

}
        else
        {
            planepositionL.x = 0;
            foreach (GameObject vessel in Vessels)
            {
                int Index = Array.IndexOf(Vessels, vessel);
                vessel.transform.localPosition = originalPosition[Index];
                vessel.transform.localScale = originalScale[Index];
                vessel.transform.localRotation = originalRotation;

                if (vessel.activeSelf)
                {
                    if (planepositionL.x == 0)
                    {
                        planepositionL = vessel.transform.localPosition;
                    }
                    else
                    {
                        planepositionR = vessel.transform.localPosition;
                    }
                }
            }

            ScreenForTwoD.SetActive(true);
            PlaneForTwoDL.SetActive(true);
            PlaneForTwoDR.SetActive(true);
            ModelP1.SetActive(true);

            PlaneForTwoDL.transform.localPosition = planepositionL;
        }
    }

    public void reset()
    {
        Debug.Log("RESET");
        foreach (GameObject vessel in Vessels)
        {
            int Index = Array.IndexOf(Vessels, vessel);
            vessel.transform.localPosition = originalPosition[Index];
            vessel.transform.localScale = originalScale[Index];
            vessel.transform.localRotation = originalRotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var scroller = Input.GetAxis("Mouse ScrollWheel");

        //zoom in
        if (scroller > 0f)
        {
            // scroll up
            foreach (GameObject vessel in Vessels)
            {
                scale = vessel.transform.localScale;
                scale.x += speed;
                scale.y += speed;
                scale.z += speed;
                vessel.transform.localScale = scale;
            }
        }
        //zoom out
        else if (scroller < 0f)
        {
            // scroll down
            foreach (GameObject vessel in Vessels)
            {
                // scroll up
                scale = vessel.transform.localScale;
                scale.x -= speed;
                scale.y -= speed;
                scale.z -= speed;
                vessel.transform.localScale = scale;
            }
        }

        // rotate y
        if (Input.GetKey("left"))
        {
            foreach (GameObject vessel in Vessels)
            {
                vessel.transform.Rotate(0, 0, speed * 10);
            }
        }

        // rotate y
        if (Input.GetKey("right"))
        {
            foreach (GameObject vessel in Vessels)
            {
                vessel.transform.Rotate(0, 0, -speed * 10);
            }
        }

        // rotate x
        if (Input.GetKey("up"))
        {
            foreach (GameObject vessel in Vessels)
            {
                vessel.transform.Rotate(speed * 10, 0, 0);
            }
        }

        // rotate x
        if (Input.GetKey("down"))
        {
            foreach (GameObject vessel in Vessels)
            {
                vessel.transform.Rotate(-speed * 10, 0, 0);
            }
        }

        //position right
        if (Input.GetKey("d"))
        {
            foreach (GameObject vessel in Vessels)
            {
                position = vessel.transform.localPosition;
                position.x += speed * 10;
                vessel.transform.localPosition = position;
            }
        }

        //position left
        if (Input.GetKey("a"))
        {
            foreach (GameObject vessel in Vessels)
            {
                position = vessel.transform.localPosition;
                position.x -= speed * 10;
                vessel.transform.localPosition = position;
            }
        }

        //position up
        if (Input.GetKey("w"))
        {
            foreach (GameObject vessel in Vessels)
            {
                position = vessel.transform.localPosition;
                position.y += speed * 10;
                vessel.transform.localPosition = position;
            }
        }

        //position down
        if (Input.GetKey("s"))
        {
            foreach (GameObject vessel in Vessels)
            {
                position = vessel.transform.localPosition;
                position.y -= speed * 10;
                vessel.transform.localPosition = position;
            }
        }
    }
}

