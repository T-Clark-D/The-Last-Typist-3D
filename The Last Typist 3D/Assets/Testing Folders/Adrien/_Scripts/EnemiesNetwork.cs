using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class EnemiesNetwork : TargetableNetwork
{
    public float m_speed;

    private Rigidbody m_RB;
    //public GameObject m_player;
    //public GameObject m_enemyTextPrefab;

    [SerializeField] GameObject m_enemyAnchor;
    [SerializeField] Text m_anchoredText;

    private Camera m_cam;
    private RectTransform m_RT;

    private PhotonView view;

    public void Initialize()
    {
        m_RB = GetComponent<Rigidbody>();

        m_cam = Camera.main;
        m_RT = m_anchoredText.GetComponent<RectTransform>();

        view = GetComponent<PhotonView>();
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

    void TransferOwnership()
    {
        view.RequestOwnership();
        view.RPC(nameof(Kill), RpcTarget.AllBuffered);
    }

    // https://forum.photonengine.com/discussion/18767/ownership-transfer-to-destroy-object
    public void Death()
    {
        if(view.IsMine) // Check if you own it  (view.Owner == PhotonNetwork.LocalPlayer)
        {
            PhotonNetwork.Destroy(gameObject);
        }
        else
        {
            TransferOwnership();
        }
    }

    [PunRPC]
    public void Kill()
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
