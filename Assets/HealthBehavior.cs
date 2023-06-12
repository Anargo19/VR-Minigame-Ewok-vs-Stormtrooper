using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WS3;

public class HealthBehavior : MonoBehaviourPunCallbacks
{
    public int health;
    public int healthMax;
    [SerializeField] List<AudioClip> hurtSound;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioSource Death;
    public bool isInvulnerable;
    [SerializeField] Image healthBar;
    // Start is called before the first frame update
    void Start()
    {
        health = healthMax;
        if (photonView.IsMine)
            healthBar.transform.parent.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (!photonView.IsMine)
            return;
        if (health <= 0) PhotonNetwork.LeaveRoom();
    }

    public void TakeDamage()
    {
        if (photonView.IsMine)
        {
            photonView.RPC("TakeDamageRPC", RpcTarget.AllViaServer);
        }
    }

    [PunRPC]
    void TakeDamageRPC()
    {
        health -= 1;
        healthBar.fillAmount = (float)health / (float)healthMax;
        GetComponent<AudioSource>().PlayOneShot(hurtSound[Random.Range(0, hurtSound.Count - 1)]);
        isInvulnerable= true;
        StartCoroutine(Invulnerable());
        if (health <= 0)
        {
            AudioSource audioSource = Instantiate(Death, transform.position, Quaternion.identity);
            audioSource.PlayOneShot(deathSound);
        }
    }

    IEnumerator Invulnerable()
    {
        yield return new WaitForSeconds(1);

        isInvulnerable = false;
    }
}
