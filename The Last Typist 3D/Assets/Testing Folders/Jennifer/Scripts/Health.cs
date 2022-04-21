using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public static int health; 
    public int numOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private void Start()
    {
        // Start the game with some health
        health = numOfHearts;
    }

    void Update()
    {
        // Make sure player doesn't have more health than the number of heart containers
        if(health > numOfHearts)
        {
            health = numOfHearts;
        }

        for(int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            // Show numOfHearts defined 
            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
                hearts[i].enabled = false;
        }

        if(health == 0)
        {
            SceneManager.LoadScene("GameOverScene");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "NPC")
        {
            health -= 1; // dmg
        }
    }
}
