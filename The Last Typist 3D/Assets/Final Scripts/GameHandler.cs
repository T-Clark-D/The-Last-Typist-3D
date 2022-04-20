using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameHandler : MonoBehaviour
{
    [SerializeField] Text waveText;
    [SerializeField] Text enemyText;
    public static int waveNum;
    public static int totalEnemyNum = 0;
    public static int currentEnemyNum = 0;
    private float timer = 0;
    private float waitTime = 5f;
    public GameObject basicZombie;
    public WordVomit WV;
    // The CameraSwitcher script is attacked to the stateDrivenCamera gameobject
    public GameObject stateDrivenCamera;
    public CameraSwitcher CS;
    public static int spawnedEnemies;
    public SpawnHandler SH;
    public static bool waveMode;
    public static bool resourceGatheringMode;
    public static bool buildMode;
    public bool resourceGatheringModeInitialised;

    public Canvas hud;

    public static string selectedObject = "FleshBags";

    private void Awake()
    {
        CS = stateDrivenCamera.GetComponent<CameraSwitcher>();
        SH = GetComponent<SpawnHandler>();
    }

    void Start()
    {
        //selectedObject = null;
        resourceGatheringMode = false;
        buildMode = false;
        waveMode = true;
        resourceGatheringModeInitialised = false;
        waveNum = 1;
        totalEnemyNum = 1; //Random.Range((waveNum * 10) / 2, waveNum * 10);
        currentEnemyNum = totalEnemyNum;
        waveText.text = "Wave " + waveNum;
        enemyText.text = "Enemies Left: " + currentEnemyNum;
        spawnedEnemies = 0;
        hud.gameObject.SetActive(false);
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
                    //Debug.Log("2sec interval");
                    if (spawnedEnemies < totalEnemyNum)
                    {
                        int chosenZombie = Random.Range(0, 1);
                        //Instantiate(basicZombie, new Vector3(4, 0, 0), Quaternion.identity);
                        //spawnedEnemies++;
                        SH.SpawnZombie(chosenZombie);
                    }
                    timer = 0;
                }
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
                CS.SwitchState();
                var corpses = GameObject.FindGameObjectsWithTag("corpse");
                foreach (GameObject corpse in corpses)
                {
                    corpse.GetComponent<Enemies>().enabled = true;
                    corpse.GetComponent<Enemies>().InitializeTextBoxWithLength(5);
                }
                resourceGatheringModeInitialised = true;

            }

            //if build mode button is pressed switch to build mode
            if (GameObject.FindGameObjectsWithTag("corpse").Length == 0)
            {
                buildMode = true;
                resourceGatheringMode = false;
                Debug.Log("BUILD MODE ENTERED");
            }
        }

        if (buildMode)
        {
            hud.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.R))
            {
                buildMode = false;
                waveMode = true;
                CS.SwitchState();
                RestartWave();
            }

        }
    }

    void RestartWave()
    {
        Debug.Log("NEW WAVE");
        resourceGatheringModeInitialised = false;
        waveNum++;
        spawnedEnemies = 0;
        totalEnemyNum = Random.Range((waveNum * 10) / 2, waveNum * 10);
        currentEnemyNum = totalEnemyNum;
        waveText.text = "Wave " + waveNum;
        enemyText.text = "Enemies Left: " + currentEnemyNum;
        hud.gameObject.SetActive(false);


    }

}
