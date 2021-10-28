using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Camera_Controller : MonoBehaviour
{

    public Component freeLookCam;
    public CinemachineFreeLook cinefree;



    bool v_Override = true;
    
    float y;

    public void Start()
    {
        CinemachineCore.GetInputAxis = GetAxisCustom;

    }

    public void setOverride(bool ourVariable)
    {
        v_Override = ourVariable;

    }

    public float getRigVal(int x)
    {
        y = cinefree.m_Orbits[x].m_Radius;
        return y;

    }

    public void setRigVal(float v, int f)
    {
        cinefree.m_Orbits[f].m_Radius = v;
    }

    public float GetAxisCustom(string axisName)
    {
        if (axisName == "Mouse X" && v_Override == false)
        {
            return Input.GetAxisRaw("Mouse X");
        }
        else if (axisName == "Mouse Y" && v_Override == false)
        {
            return Input.GetAxisRaw("Mouse Y");
        }

        if (axisName == "Mouse X" && v_Override == true)
        {
            return 0;
        }
        else if (axisName == "Mouse Y" && v_Override == true)
        {
            return 0;
        }

        return 0;
    }
    // Update is called once per frame
    void Update()
    {
        
            //Mouse camera movement mechanic
           

            if (Input.GetMouseButton(2))
            {
                setOverride(false);
                GetAxisCustom("Mouse X");
                GetAxisCustom("Mouse Y");
                Cursor.lockState = CursorLockMode.Locked;

            }

            if ((Input.GetMouseButtonUp(2)) && (Cursor.lockState == CursorLockMode.Locked))
            {
                setOverride(true);
                Cursor.lockState = CursorLockMode.None;
            }


        }
}
