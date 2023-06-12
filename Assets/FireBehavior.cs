using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class FireBehavior : MonoBehaviourPunCallbacks
{
    public Transform firePosition;
    public Transform bullet;
    public Camera camera;
    public AudioClip fireSound;

    // Start is called before the first frame update
    void Start()
    {
        if(!photonView.IsMine)
            camera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (SteamVR_Actions._default.GrabGrip.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            Debug.Log(photonView);
            this.photonView.RPC("Fire", RpcTarget.AllViaServer, firePosition.position, firePosition.rotation);

        }
    }


    [PunRPC]
    void Fire(Vector3 position, Quaternion rotation, PhotonMessageInfo info)
    {

        // Tips for Photon lag compensation. Il faut compenser le temps de lag pour l'envoi du message.
        // donc décaler la position de départ de la balle dans la direction
        float lag = (float)(PhotonNetwork.Time - info.SentServerTime);

        // Instantiate the Snowball from the Snowball Prefab at the position of the Spawner
        //...
        Transform bulletInstance = Instantiate(bullet, position + Vector3.forward * lag, rotation);
        GetComponent<AudioSource>().PlayOneShot(fireSound);
        // Set velocity to the snowballRigidBody direction and speed
        //...
       // bulletInstance.GetComponent<Rigidbody>().AddForce(Vector3.forward * 100);

        // Instantiate the snow ball
        //...

        // Destroy the Snowball after 5 seconds
        //...
        Destroy(bulletInstance.gameObject, 5f);
    }


    
}
