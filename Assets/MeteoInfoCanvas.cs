using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MeteoInfoCanvas : MonoBehaviour
{
    public TextMeshProUGUI tempText;
    public RawImage weatherIcon;
    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(GetWeatherIcon());
    }

    // Update is called once per frame
    void Update()
    {

        tempText.text = $"{Meteo.Inst.main.temp} °C";
    }


    IEnumerator GetWeatherIcon()
    {
        while (true)
        {
            if (Meteo.Inst == null)
            {
                yield return new WaitForSeconds(1);
                continue;
            }
            if (Meteo.Inst.weather == null)
            {
                yield return new WaitForSeconds(1);
                continue;
            }
            Debug.Log($"http://openweathermap.org/img/wn/01d@2x.png");
            UnityWebRequest www = UnityWebRequestTexture.GetTexture($"http://openweathermap.org/img/wn/{Meteo.Inst.weather[0].icon}@2x.png");

            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }

            else
                weatherIcon.texture = ((DownloadHandlerTexture)www.downloadHandler).texture;

            yield return new WaitForSeconds(10);
        }
    }
}
