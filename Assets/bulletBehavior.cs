using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WS3;

public class bulletBehavior : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        RaycastHit hit;
        if(Physics.Raycast(transform.position,transform.forward, out hit, 2f))
        {
            if(hit.collider.tag == "TPS")
            {
                HealthBehavior health = hit.collider.GetComponent<HealthBehavior>();
                if (health.isInvulnerable)
                {
                    Destroy(gameObject);
                    return;
                }
                health.TakeDamage();

            }
            Destroy(gameObject);
        }
    }
}
