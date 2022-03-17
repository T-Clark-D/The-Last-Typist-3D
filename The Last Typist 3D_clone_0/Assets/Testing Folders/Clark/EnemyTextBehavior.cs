using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTextBehavior : MonoBehaviour
{
    public GameObject m_enemyAnchor;
    private Camera m_cam;
    public RectTransform m_RT;

    void Start()
    {
        m_cam = Camera.main;
        m_RT = GetComponent<RectTransform>();
    }

    void Update()
    {
        //anchor on player transformed into point on canvas
        if (m_enemyAnchor != null)
        {
            m_RT.position = m_cam.WorldToScreenPoint(m_enemyAnchor.transform.position);
        }
    }

    public void SetAnchor(GameObject anchor)
    {
        m_enemyAnchor = anchor;
    }
}
