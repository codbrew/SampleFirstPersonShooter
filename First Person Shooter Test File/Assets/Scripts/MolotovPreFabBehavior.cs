using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MolotovPreFabBehavior : MonoBehaviour
{
    [SerializeField] private float fireForce;
    [SerializeField] private Rigidbody rb;
    public float rbAwakeTimer;
    public int rbStartTime;

    private void Awake()
    {
        rb.useGravity = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(transform.forward * fireForce * Time.deltaTime);
        rbAwakeTimer += 1 * Time.deltaTime;

        if (rbAwakeTimer >= rbStartTime)
        {
            rb.useGravity = true;
        }

        
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        Destroy(gameObject);
    }


}
