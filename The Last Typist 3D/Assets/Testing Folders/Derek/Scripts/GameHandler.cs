using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;


public class GameHandler : MonoBehaviour
{
    [SerializeField] Text playerOneText;
    [SerializeField] Text playerTwoText;
    public static Text playerOneType;
    public static Text playerTwoType;


    void Start()
    {
        playerOneType = playerOneText;
        playerTwoType = playerTwoType;
    }

    void Update()
    {
        if (PhotonNetwork.CountOfPlayersOnMaster == 2)
        {
            SetText();
        }
        Debug.Log(PhotonNetwork.CountOfPlayersOnMaster);
    }


    public void SetText()
    {
        playerOneText.text = playerOneType.text;
        playerOneText.transform.position = playerOneType.transform.position;
        playerTwoText.text = playerTwoType.text;
        playerTwoText.transform.position = playerTwoType.transform.position;
    }




}
