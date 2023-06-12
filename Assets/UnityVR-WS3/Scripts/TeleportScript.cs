using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class TeleportScript : MonoBehaviour
{
    // Start is called before the first frame update

    public SteamVR_Input_Sources inputSource;
    public bool isGrabbingPinch;
    private SteamVR_Input_Sources teleport;
    public GameObject player;

    void Awake()
    {
        inputSource = GetComponent<SteamVR_Behaviour_Pose>().inputSource;

        //angularV = GetComponent<SteamVR_Behaviour_Pose>().GetAngularVelocity();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (SteamVR_Actions._default.Teleport.GetStateDown(inputSource))
        {
            Debug.Log("Teloprt Start");
            TeleportPressed();
        }
        else if (SteamVR_Actions._default.Teleport.GetStateUp(inputSource))
        {
            TeleportReleased();
        }

    }


    public void TeleportPressed()
    {
        gameObject.AddComponent<ControllerPointer>();
    }

    public void TeleportReleased()
    {
        var controllerPointer = GetComponent<ControllerPointer>();

        if (controllerPointer.CanTeleport)
        {
            //GameObject cameraRig = GameObject.Find("[CameraRig]");
            player.transform.position = controllerPointer.TargetPosition;
            controllerPointer.DesactivatePointer();
            Destroy(gameObject.GetComponent<ControllerPointer>());
        }
    }

    public delegate void OnGrabPressed(GameObject controller);
    public static event OnGrabPressed onGrabPressed;
    public delegate void OnGrabReleased(GameObject controller);
    public static event OnGrabReleased onGrabReleased;
}
