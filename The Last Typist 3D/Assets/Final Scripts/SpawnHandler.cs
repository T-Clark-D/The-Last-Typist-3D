using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnHandler : MonoBehaviour
{
    #region Variables and Prefabs
    // Prefabs for zombies
    public GameObject basicZombie;
    public GameObject bomberZombie;
    public GameObject witchDoctorZombie;
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

    public void SpawnZombie(int type)
    {
        if (type == 0) // Basic Zombie
        {
            Debug.Log("Basic zombie spawned!");
            int chosenSpawn = Random.Range(0, spawnPointList.Count);
            Instantiate(basicZombie, spawnPointList[chosenSpawn].transform.position, Quaternion.identity);
            GameHandler.spawnedEnemies++;
        }
        if (type == 1) // Bomber Zombie
        {
            Debug.Log("Bomber zombie spawned!");
            int chosenSpawn = Random.Range(0, spawnPointList.Count);
            Instantiate(bomberZombie, spawnPointList[chosenSpawn].transform.position, Quaternion.identity);
            GameHandler.spawnedEnemies++;
        }
        if (type == 2) // Witch Doctor Zombie
        {
            Debug.Log("Witch Doctor zombie spawned!");
            int chosenSpawn = Random.Range(0, spawnPointList.Count);
            Instantiate(witchDoctorZombie, spawnPointList[chosenSpawn].transform.position, Quaternion.identity);
            GameHandler.spawnedEnemies++;
        }
    }
}
