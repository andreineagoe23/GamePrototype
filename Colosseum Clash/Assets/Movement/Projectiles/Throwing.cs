using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Throwing : MonoBehaviour
{
    [Header("References")]
    public Transform cam;
    public Transform attackPoint;
    public GameObject objectToThrow;
    public TextMeshProUGUI text_info;

    [Header("Stats")]
    public int totalThrows;
    public float throwCooldown, timeBetweenThrows;

    [Header("Throwing")]
    public float throwForce;
    public float throwUpwardForce;
    public float spread;
    public int throwsPerTap;

    int throwsLeft, throwsToExecute;

    [Header("RayCasting")]
    public bool useRaycasts;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;
    public WeaponSwitching ws;

    [Header("Extra Settings")]
    public bool allowButtonHold;
    bool throwing, readyToThrow, reloading;
    public KeyCode throwKey = KeyCode.E;

    private void Start()
    {
        throwsLeft = totalThrows;
        readyToThrow = true;
    }

    private void Update()
    {
        MyInput();

        ws = Object.FindAnyObjectByType<WeaponSwitching>(); // Updated
    }

    private void MyInput()
    {
        if (allowButtonHold)
            throwing = Input.GetKey(throwKey);
        else
            throwing = Input.GetKeyDown(throwKey);

        if (readyToThrow && throwing && !reloading && throwsLeft > 0)
        {
            throwsToExecute = throwsPerTap;
            Throw();
        }
    }

    private void Throw()
    {
        readyToThrow = false;

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        GameObject projectile = Instantiate(objectToThrow, attackPoint.position, cam.rotation * ws.projectileRotation);

        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        Vector3 forceDirection = cam.transform.forward + new Vector3(x, y, 0);

        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, 500f))
        {
            forceDirection = (hit.point - attackPoint.position).normalized + new Vector3(x, y, 0);
        }

        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;
        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        if (useRaycasts)
        {
            if (Physics.Raycast(cam.transform.position, forceDirection, out rayHit, whatIsEnemy))
            {
                Debug.Log(rayHit.collider.name);

                if (rayHit.collider.CompareTag("Enemy"))
                {
                    var enemy = rayHit.collider.GetComponent<Actor>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(ws.projectileDamage);
                    }
                }
            }
        }

        throwsLeft--;
        throwsToExecute--;

        if (throwsToExecute > 0 && throwsLeft > 0)
            Invoke(nameof(Throw), timeBetweenThrows);
        else if (throwsToExecute <= 0)
            Invoke(nameof(ResetThrow), throwCooldown);
    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }
}
