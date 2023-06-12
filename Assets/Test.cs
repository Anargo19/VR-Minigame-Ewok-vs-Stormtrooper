using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Management;

public class Test : MonoBehaviour
{
    public bool isVR;
    // Start is called before the first frame update
    void Start()
    {
        if(isVR)
        {

        }
        foreach(XRLoader xRLoader in XRGeneralSettings.Instance.Manager.activeLoaders)
        {
            Debug.Log(xRLoader);

        }
        
    }

   

    // Update is called once per frame
    void Update()
    {
        
    }
}
