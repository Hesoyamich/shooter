using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{

    public int rocketSpeed;
    
    public float exploadRadius;
    public GameObject explodeParticle;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += transform.forward * rocketSpeed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision other) {
        if (!(other.gameObject.tag == "Player"))
        {
            RaycastHit[] allCast = Physics.SphereCastAll(transform.position, exploadRadius, transform.forward, 0.80f);
            foreach (RaycastHit hit in allCast)
            {
                Enemy target = hit.transform.GetComponent<Enemy>();
                if (target != null) target.TakeDamage(100);
            }
            Instantiate(explodeParticle,transform.position - new Vector3(0,1f,0), transform.rotation);
            Destroy(gameObject);
        }
        
    }
}
