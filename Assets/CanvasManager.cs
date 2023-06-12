using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using WS3;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager inst;
    public TextMeshProUGUI _amountText;
    private void Awake()
    {
        if(inst != null)  return; 
        inst = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        bool isPC = UserDeviceManager.GetDeviceUsed() == UserDeviceType.PC;
        gameObject.SetActive(isPC);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAmount(int amount)
    {
        _amountText.text = amount.ToString();
    }
}
