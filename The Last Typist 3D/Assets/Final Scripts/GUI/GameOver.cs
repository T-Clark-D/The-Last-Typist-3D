using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] Text waveText;

    public static int wave;
    
    // Start is called before the first frame update
    void Start()
    {
        wave = GameHandler.waveNum;
        waveText.text = "" + wave;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
