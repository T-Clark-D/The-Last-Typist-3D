using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombie : Enemies
{
    public int m_wordLength = 5;
    BombZombie_Controller bombZombieController;

    // Start is called before the first frame update
    void Start()
    {
        base.Initialize();
        base.InitializeTextBoxWithLength(m_wordLength);
        bombZombieController = GetComponent<BombZombie_Controller>();
        if (m_speed != 0)
        {
            bombZombieController.StartOrStopSlowWalkAnimation();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag == "NPC")
        {
            Vector3 direction = (m_player.transform.position - transform.position).normalized;
            Vector3 velocity = direction * m_speed;
            transform.LookAt(m_player.transform.position);
            transform.position = (transform.position + velocity * Time.deltaTime);
        }
        if (killed)
        {
            bombZombieController.StartOrStopSlowWalkAnimation();
            bombZombieController.DeathAnimation();
            gameObject.GetComponent<Enemies>().enabled = false;

        }
    }
}
