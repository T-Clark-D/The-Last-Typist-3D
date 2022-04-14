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
    

    void Start()
    {
        waveNum = 1;
        totalEnemyNum = Random.Range((waveNum*10)/2, waveNum * 10);
        currentEnemyNum = totalEnemyNum;
        waveText.text = "Wave " + waveNum;
        enemyText.text = "Enemies Left: " + currentEnemyNum;
    }

    void Update()
    {
        if(currentEnemyNum == 0)
        {
            waveNum++;
            // BUILD PHASE + RESOURCE 



            Debug.Log("Wave complete");
            //RestartWave();
        }
        enemyText.text = "Enemies Left: " + currentEnemyNum;
    }

    void RestartWave()
    {
        totalEnemyNum = Random.Range((waveNum * 10) / 2, waveNum * 10);
        currentEnemyNum = totalEnemyNum;
        waveText.text = "Wave " + waveNum;
        enemyText.text = "Enemies Left: " + currentEnemyNum;
    }

}
