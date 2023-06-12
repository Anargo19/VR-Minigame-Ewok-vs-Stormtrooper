using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearSpawner : MonoBehaviourPunCallbacks
{
    public GameObject spear;
    // Start is called before the first frame update
    void Start()
    {
        if(!PhotonNetwork.IsMasterClient) { return; }
        StartCoroutine(SpawnSpear());  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [PunRPC]
    private void StartSpawn(int x, int z)
    {
        Instantiate(spear, new Vector3(x, 0.5f, z), Quaternion.identity);
    }


    IEnumerator SpawnSpear()
    {
        while (true) {
            int x = Random.Range(-24, 24);
            int z = Random.Range(-24, 24);
            photonView.RPC("StartSpawn", RpcTarget.AllViaServer, x, z);

            //photonView .Instantiate(spear, new Vector3(Random.Range(-24, 24), 0.5f, Random.Range(-24, 24)), Quaternion.identity);
            yield return new WaitForSeconds(5); 
        }
    }
}
