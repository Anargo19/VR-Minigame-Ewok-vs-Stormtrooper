using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableIfNotLocal : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Camera>().enabled = photonView.IsMine;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
