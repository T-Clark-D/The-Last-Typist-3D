using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicZombieNetwork : EnemiesNetwork
{
    public int m_wordLength = 5;
    // Start is called before the first frame update
    void Start()
    {
        base.Initialize();
        base.InitializeTextBoxWithLength(m_wordLength);
    }

    // Update is called once per frame
    void Update()
    {
        base.AttachText();
    }
}
