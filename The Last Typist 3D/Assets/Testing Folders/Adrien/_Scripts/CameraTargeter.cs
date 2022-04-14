using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraTargeter : MonoBehaviour
{
    public GameObject player;
    public PlayerController pc;
    [SerializeField] private CinemachineVirtualCamera cam;

    private void Awake()
    {
        pc = player.GetComponent<PlayerController>();
        cam = player.GetComponent<CinemachineVirtualCamera>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (true)
        {

        }
    }
}
