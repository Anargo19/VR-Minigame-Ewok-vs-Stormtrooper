using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Terrain" || other.tag == "Stormtrooper")
        {

        if (other.tag == "Stormtrooper")
        {
                HealthBehavior health = other.GetComponentInParent<HealthBehavior>();
                if (health.isInvulnerable)
                {
                    Destroy(gameObject);
                    return;
                }
                health.TakeDamage();
            Debug.Log("TOUCHE");
        }
        Destroy(gameObject);
        }
    }
}
