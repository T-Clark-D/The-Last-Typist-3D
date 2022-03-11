using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform playerOnePos;
    public Transform playerTwoPos;
    

    // Start is called before the first frame update
    void Start()
    {
        if(PhotonNetwork.IsMasterClient)
        {
           PhotonNetwork.Instantiate(playerPrefab.name, playerOnePos.position, Quaternion.identity);
        }

        else
        {
            PhotonNetwork.Instantiate(playerPrefab.name, playerTwoPos.position, Quaternion.identity);
        }
        

    }


}
