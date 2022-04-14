using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemiesNetwork : TargetableNetwork
{
    public float m_speed;

    private Rigidbody m_RB;

    [SerializeField] GameObject m_enemyAnchor;
    [SerializeField] Text m_anchoredText;

    private Camera m_cam;
    private RectTransform m_RT;


    public void Initialize()
    {
        m_RB = GetComponent<Rigidbody>();

        m_cam = Camera.main;
        m_RT = m_anchoredText.GetComponent<RectTransform>();
    }

    public void InitializeTextBoxWithLength(int length)
    {
        m_anchoredText.text = RandomWordOfLength(length);
    }

    public void AttachText()
    {
        m_RT.position = m_cam.WorldToScreenPoint(m_enemyAnchor.transform.position);
    }

    private string RandomWordOfLength(int length)
    {
        targetWord = "tempword" + length.ToString();
        return targetWord;
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    public void TargetedText(int textLength)
    {

        if (textLength == 0)
        {
            m_anchoredText.text = "<color=white>" + targetWord + "</color>";
        }
        else
        {
            m_anchoredText.text = "<color=red>" + targetWord.Substring(0, textLength) + "</color>" + targetWord.Substring(textLength, targetWord.Length - textLength);
        }
    }
}
