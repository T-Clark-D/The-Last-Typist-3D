using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrittenTextNetwork : MonoBehaviour
{
    // public for testing purposes
    public GameObject m_playerAnchor;
    private Camera m_cam;
    public RectTransform m_RT;

    // Start is called before the first frame update
    void Start()
    {
        m_cam = Camera.main;
        m_RT = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        m_RT.position = m_cam.WorldToScreenPoint(m_playerAnchor.transform.position);
    }
}
