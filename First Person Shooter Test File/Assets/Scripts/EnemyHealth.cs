using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private KeyCode enemyHurt = KeyCode.Q;
    [SerializeField] private int maxHealth;
    public static int currentHealth;
    [SerializeField] private int damage;
    

    public event Action<float> OnHealthPctChange = delegate { };
    // Start is called before the first frame update
    private void OnEnable()
    {
        
        currentHealth = maxHealth;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(enemyHurt))
        {
            ModifyHealth(damage);
            
        }

        
    }

    public void ModifyHealth(int amount)
    {
        currentHealth += amount;
        float currentHealthPct = (float)currentHealth / (float)maxHealth;
        OnHealthPctChange(currentHealthPct);

    }
}
