using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float fallForce;
    [SerializeField] private float fallMultiplyer;
    [SerializeField] private KeyCode enemyFall;
    private float healthReference;
    

    [SerializeField] private float moveSpeed;
   

    public Transform Player;
    
    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        GetComponent<EnemyHealth>();

        Player = GameObject.FindWithTag("Player").transform;


    }

    private void Awake()
    {
        transform.LookAt(Player);

        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, Player.position, moveSpeed * Time.deltaTime);
        EnemyIsDead();

    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

    private void EnemyIsDead()
    {
        if (EnemyHealth.currentHealth <= 0)
        {
            rb.useGravity = true;
            AddFallForce();
        }
        else
        {
            rb.useGravity = false;
        }
    }

    private void AddFallForce()
    {
        rb.AddForce(Vector3.down * fallForce * Time.deltaTime * fallMultiplyer);
    }
}
