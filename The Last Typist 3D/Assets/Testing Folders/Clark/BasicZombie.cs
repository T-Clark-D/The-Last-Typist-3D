using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicZombie : Enemies
{
    
    public int m_wordLength = 5;
    // Start is called before the first frame update
     public void Start()
    {
        base.Initialize();
        base.InitializeTextBoxWithLength(m_wordLength);
        m_speed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //otherwise its corpse that doesnt move
        if(gameObject.tag == "NPC")
        {
            Vector3 direction = (m_player.transform.position - transform.position).normalized;
            Vector3 velocity = direction * m_speed;
            transform.position = (transform.position + velocity * Time.deltaTime);
        }
    }
}
