using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnHandler : MonoBehaviour
{
    #region Variables and Prefabs
    // Prefabs for zombies
    public GameObject basicZombie;
    // List of spawn points for zombies
    public List<GameObject> spawnPointList;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        var a = GameObject.FindGameObjectsWithTag("SpawnPoint");
        spawnPointList = a.ToList();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnZombie(string type)
    {
        if (type.Equals("Basic"))
        {
            int chosenSpawn = Random.Range(0, spawnPointList.Count);
            Instantiate(basicZombie, spawnPointList[chosenSpawn].transform.position, Quaternion.identity);
            GameHandler.spawnedEnemies++;
        }
    }
}
