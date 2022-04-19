using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //SceneManager.LoadScene("Loading");
    }

    public void PlaySolo()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void PlayOnline()
    {
        SceneManager.LoadScene("Loading");
    }

    public void ExitGame()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
