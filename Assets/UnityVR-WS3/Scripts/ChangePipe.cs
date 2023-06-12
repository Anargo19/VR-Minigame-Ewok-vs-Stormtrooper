using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ChangePipe : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject NewPipe;
    public GameObject NewPipe2;
    public SteamVR_Input_Sources inputSource;

    private void Awake()
    {
        inputSource = GetComponent<SteamVR_Behaviour_Pose>().inputSource;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Valve_02 (1)" && SteamVR_Input.GetStateUp("grabpinch", inputSource))
        {
            //Debug.Log("test");
            //NewPipe.SetActive(false);
            //NewPipe.transform.position = new Vector3(-1.70000005f, -1.15999997f, 0.870000005f);
            Destroy(NewPipe);  //  NewPipe.SetActive(false);
            //NewPipe.transform.position = gameObject.transform.position;
            //NewPipe.GetComponent<Rigidbody>().useGravity = false;
            //NewPipe.GetComponent<Rigidbody>().isKinematic = true;
            //other.GetComponent<Rigidbody>().useGravity = false;
            //other.GetComponent<Rigidbody>().isKinematic = true;
            NewPipe2.SetActive(true);
        }
    }

    public void Test()
    {
        transform.position = new Vector3(-1.155f, -0.143f, 1.565f);
    }


}
