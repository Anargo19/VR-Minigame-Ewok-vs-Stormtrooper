using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Meteo
{


    public List<Weather> weather;
    public Main main;
    // Start is called before the first frame update
    private static Meteo inst;
    public static Meteo Inst
    {
        get
        {
            if (inst == null)
            {
                inst = new Meteo();
            }
            return inst;
        }
    }

    public void UpdateValuesFromJSON(string jsonString)
    {
        JsonUtility.FromJsonOverwrite(jsonString, Inst);
    }
}
