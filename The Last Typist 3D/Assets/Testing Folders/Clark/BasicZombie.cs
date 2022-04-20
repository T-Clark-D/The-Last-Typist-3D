using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicZombie : Enemies
{

    public int m_wordLength = 5;
    Zombie_Controller zombieController;

    // Start is called before the first frame update
    public void Start()
    {
        base.Initialize();
        base.InitializeTextBoxWithLength(m_wordLength);
        zombieController = GetComponent<Zombie_Controller>();
        if (m_speed != 0)
        {
            zombieController.StartOrStopWalkAnimation1();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //otherwise its corpse that doesnt move
        if (gameObject.tag == "NPC")
        {
            Vector3 direction = (m_player.transform.position - transform.position).normalized;
            Vector3 velocity = direction * m_speed;
            transform.LookAt(m_player.transform.position);
            transform.position = (transform.position + velocity * Time.deltaTime);
        }
        if (killed)
        {
            zombieController.StartOrStopWalkAnimation1();
            zombieController.DeathAnimation();
            gameObject.GetComponent<Enemies>().enabled = false;

        }
    }
}
