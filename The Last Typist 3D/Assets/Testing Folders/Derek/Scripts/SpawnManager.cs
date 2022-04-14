using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefabs;
    

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length-1)], new Vector3(1, 0, 15), Quaternion.Euler(0f, 180f, 0f));
    }

    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("NPC").Length == 0 && GameHandler.currentEnemyNum != 0)
        {
            Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length-1)], new Vector3(1, 0, 15), Quaternion.Euler(0f, 180f, 0f));
        }
    }


}
