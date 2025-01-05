using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ProjectileStick : MonoBehaviour
{

    private void Start()
    {
        // Ensure Rigidbody is assigned
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("[ProjectileStick] Rigidbody not found on the projectile!");
        }
        if (ws == null)
        {
            ws = Object.FindAnyObjectByType<WeaponSwitching>();
            if (ws == null)
            {
                Debug.LogError("[ProjectileStick] WeaponSwitching reference not found in the scene!");
            }
        }

    }

    private Rigidbody rb;

    private bool targetHit;
    public WeaponSwitching ws;


    private void OnCollisionEnter(Collision collision)
    {
        if (targetHit)
            return;
        else
            targetHit = true;

        if(collision.gameObject.GetComponent<Actor>() != null)
        {
            Actor enemy = collision.gameObject.GetComponent<Actor>();

            enemy.TakeDamage(ws.projectileDamage);
        }

        rb.isKinematic = true;

        transform.SetParent(collision.transform);

    }


}
