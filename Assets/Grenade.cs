using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{

    public float delay = 3f;
    public float radius = 5f;
    public float force = 700;

    public GameObject explosionEffect;

    bool hasExploded = false;

    float countdown;

    // Start is called before the first frame update
    void Start()
    {
        //ketika granat nya memulai countdown, spawnnya selama 3 detik
        countdown = delay;

    }

    // Update is called once per frame
    void Update()
    {
        
        countdown -= Time.deltaTime;   //mengurangi waktu countdown 
        if (countdown <= 0f && hasExploded == false ) //hanya memanggil explode bila belum meledak bomnya 
        {
            Explode();
            hasExploded = true;  
        }

    }

    void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation); 
        
        //memberi list collider yang bersentuhan atau di dalam sphere, dan lokasi spherenya yang sudah kita tentukan
        Collider[] collidersToDestroy    = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObject in collidersToDestroy)
        {

            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }

           
        }
        

        Destroy(gameObject);
    }
}
