using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemyPF;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(enemyPF, transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
