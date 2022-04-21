using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Witch : Enemies
{
    public int m_wordLength = 5;
    WitchDoctor_Controller witchController;

    // Start is called before the first frame update
    void Start()
    {
        base.Initialize();
        base.InitializeTextBoxWithLength(m_wordLength);
        witchController = GetComponent<WitchDoctor_Controller>();
        if (m_speed != 0)
        {
            witchController.StartOrStopWalkAnimation1();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (killed)
        {
            witchController.StartOrStopWalkAnimation1();
            witchController.DeathAnimation();
            gameObject.GetComponent<Enemies>().enabled = false;
        }
    }

    public bool getKilled()
    {
        return killed;
    }
}
