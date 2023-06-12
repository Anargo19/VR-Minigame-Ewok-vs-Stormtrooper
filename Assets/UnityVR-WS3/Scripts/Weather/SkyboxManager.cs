using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SkyboxManager : MonoBehaviour
{

    public Material CloudySkyBoxFilePath;
    public Material SunnySkyBoxFilePath;
    public Material MistySkyBoxFilePath;
    public Material DefaultSkyBoxFilePath;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    { 
        switch (Meteo.Inst.weather[0].main)
        {
            case "Clear":
                RenderSettings.skybox = SunnySkyBoxFilePath;
                break;
            case "Rain":
                RenderSettings.skybox = CloudySkyBoxFilePath;
                break;
            case "Mist":
                RenderSettings.skybox = MistySkyBoxFilePath;
                break;
            default:
                RenderSettings.skybox = DefaultSkyBoxFilePath;
                break;
        }

    }




    
}
