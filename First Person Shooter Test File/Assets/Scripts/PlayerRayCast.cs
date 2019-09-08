using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRayCast : MonoBehaviour
{
    [SerializeField] private KeyCode fire;
    [SerializeField] private KeyCode projectileFire;
    [SerializeField] private KeyCode reload;
    [SerializeField] private KeyCode projectileReload;
    [SerializeField] private int damage;
    [SerializeField] GameObject molotov;
    [SerializeField] GameObject projectileSpawner;

    //Hit Scan Ammo Variables

    [SerializeField] private int maxAmmo;
    [SerializeField] private int currentAmmo;
    [SerializeField] private int projectileAmmo;

   


    [SerializeField] private Animator anim;

    private void Start()
    {
        GetComponent<Animator>();
        maxAmmo = 30;
        Debug.Log("Ammo Max" + maxAmmo);
        currentAmmo = maxAmmo;
        projectileAmmo = 1;
        
    }
    private void FixedUpdate()
    {
        
        if (Input.GetKeyDown(fire) && currentAmmo > 0)
        {
            RayCast();
            
            Debug.Log(currentAmmo);
        }
        else
        {
            anim.SetBool("isShooting", false);
           
        }

        if (Input.GetKeyDown(projectileFire) && projectileAmmo == 1)
        {
            Instantiate(molotov, projectileSpawner.transform.position, transform.rotation);
            projectileAmmo--;

        }
      

        if (Input.GetKeyDown(reload))
        {
            currentAmmo = maxAmmo;
            anim.SetBool("isReloading", true);


        }
        else
        {
            anim.SetBool("isReloading", false);
        }

        if(projectileAmmo == 0 && Input.GetKeyDown(projectileReload))
        {
            anim.SetBool("isProjectileReloading", true);
            projectileAmmo = 1;
        }
        else
        {
            anim.SetBool("isProjectileReloading", false);
        }
        
        if(projectileAmmo == 0)
        {
            Debug.Log("Launcher Empty");
        }

        

        

        
    }
    private void RayCast()
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        Ray ray = new Ray(origin, direction);


        bool result = Physics.Raycast(ray, out RaycastHit raycastHit);
        anim.SetBool("isShooting", true);


        if (result)
        {
            currentAmmo--;
            raycastHit.collider.GetComponent<EnemyHealth>().ModifyHealth(damage);
            Debug.Log("Enemy Hit!");
            

        }

           
        

       
    }
}
