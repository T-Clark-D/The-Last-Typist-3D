using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public Transform playerOnePos;
    public Transform playerTwoPos;
    public Transform enemyPos;


    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate(playerPrefab.name, playerOnePos.position, Quaternion.identity);
            PhotonNetwork.Instantiate(enemyPrefab.name, enemyPos.position, Quaternion.identity);
        }

        else
        {
            PhotonNetwork.Instantiate(playerPrefab.name, playerTwoPos.position, Quaternion.identity);
        }


    }



}
