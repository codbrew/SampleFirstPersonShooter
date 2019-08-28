using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private float updateSpeed = 0.5f;
    private Slider healthBar;
    public Transform PlayerCamera;
   
    // Start is called before the first frame update
    private void Awake()
    {
        PlayerCamera = GameObject.FindWithTag("Player Camera").transform;
        
        healthBar = GetComponent<Slider>();
        GetComponentInParent<EnemyHealth>().OnHealthPctChange += HandleHealthChanged;
    }

   
    private IEnumerator ChangeToPct(float pct)
    {
        float preChangePct = healthBar.value;
        float elapsed = 0f;

        while(elapsed < updateSpeed)
        {
            elapsed += Time.deltaTime;
            healthBar.value = Mathf.Lerp(preChangePct, pct, elapsed / updateSpeed);
            yield return null;
                
        }

        healthBar.value = pct;

    }
    private void LateUpdate()
    {
        transform.LookAt(PlayerCamera.transform);
        transform.Rotate(0, 180, 0);
        
    }

    private void HandleHealthChanged(float pct)
    {
        StartCoroutine(ChangeToPct(pct));
    }
}
