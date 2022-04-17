using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameHandler : MonoBehaviour
{
    [SerializeField] Text waveText;
    [SerializeField] Text enemyText;
    public static int waveNum;
    public static int totalEnemyNum;
    public static int currentEnemyNum;
    private float timer = 0;
    private float waitTime = 2f;
    public GameObject basicZombie;
    public WordVomit WV;
    private int spawnedEnemies;
    
    public static bool waveMode;
    public static bool resourceGatheringMode;
    public static bool buildMode;
    public bool resourceGatheringModeInitialised;


    void Start()
    {
        resourceGatheringMode = false;
        buildMode = false;
        waveMode = true;
        resourceGatheringModeInitialised = false;
        waveNum = 1;
        totalEnemyNum = 2;
        currentEnemyNum = totalEnemyNum;
        waveText.text = "Wave " + waveNum;
        enemyText.text = "Enemies Left: " + currentEnemyNum;
        spawnedEnemies = 0;
    }

    void Update()
    {
        if (waveMode)
        {
            if (currentEnemyNum > 0)
            {
                timer += Time.deltaTime;
                if (timer > waitTime)
                {

                    Debug.Log("2sec interval");
                    if (spawnedEnemies < totalEnemyNum)
                    {
                        Instantiate(basicZombie, new Vector3(4, 0, 0), Quaternion.identity);
                        spawnedEnemies++;
                    }
                    timer = 0;
                }

                //InstantiateZombie(Vector3 pos)
            }
            if (currentEnemyNum == 0)
            {
                Debug.Log("Wave complete");
                waveMode = false;
                resourceGatheringMode = true;
                
            }
            enemyText.text = "Enemies Left: " + currentEnemyNum;
        }


        if (resourceGatheringMode)
        {
            //TODO: snap player to meat grinder
            //instantiates the words over courses to start gathering
            if (!resourceGatheringModeInitialised)
            {
                var corpses = GameObject.FindGameObjectsWithTag("corpse");
                foreach (GameObject corpse in corpses)
                {
                    corpse.GetComponent<Enemies>().enabled = true;
                    corpse.GetComponent<Enemies>().InitializeTextBoxWithLength(5);
                }
                resourceGatheringModeInitialised = true;
                
            }
           
            //if build mode button is pressed switch to build mode
        }



        if (buildMode)
        {

        }
    }

    void RestartWave()
    {
        resourceGatheringModeInitialised = false;
        waveNum++;
        spawnedEnemies = 0;
        totalEnemyNum = Random.Range((waveNum * 10) / 2, waveNum * 10);
        currentEnemyNum = totalEnemyNum;
        waveText.text = "Wave " + waveNum;
        enemyText.text = "Enemies Left: " + currentEnemyNum;
    }

}
