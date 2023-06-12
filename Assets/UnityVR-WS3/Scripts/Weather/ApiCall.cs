using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ApiCall : MonoBehaviour
{


    private string WebAPILink = "api.openweathermap.org/data/2.5/weather?id=6454707&appid=c4ace5f5a8eb3499237402f8396d59a9&units=metric";
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetWeather());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator GetWeather()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(WebAPILink))
        {
            yield return www.SendWebRequest();

            Meteo.Inst.UpdateValuesFromJSON(www.downloadHandler.text);
        }
    }

}
