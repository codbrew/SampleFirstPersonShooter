using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRayCast : MonoBehaviour
{
    [SerializeField] private KeyCode fire;
    [SerializeField] private int damage;
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(fire))
        {
            RayCast();
        }
    }
    private void RayCast()
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        Ray ray = new Ray(origin, direction);


        bool result = Physics.Raycast(ray, out RaycastHit raycastHit);

        if (result)
        {
            raycastHit.collider.GetComponent<EnemyHealth>().ModifyHealth(damage);
            Debug.Log("Enemy Hit!");
            

        }

           
        

       
    }
}
