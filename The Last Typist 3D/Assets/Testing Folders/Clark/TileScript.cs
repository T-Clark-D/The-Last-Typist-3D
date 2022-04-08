using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    [SerializeField] public Material baseColor, offsetColor, hoverColor;
    private MeshRenderer MR;
    private bool buildMode = false;
    private MeshCollider MC;
    private bool offsetStatus;

    public void Init(bool isOffSet)
    {
        offsetStatus = isOffSet;
        MC = GetComponent<MeshCollider>();
        MR = GetComponent<MeshRenderer>();
        MR.material = isOffSet ? offsetColor : baseColor;
    }

    private void OnMouseOver()
    {
        if(buildMode)
        MR.material = hoverColor;
    }
    private void OnMouseExit()
    {
        if (buildMode)
        MR.material = offsetStatus ? offsetColor : baseColor;
    }
    private void OnMouseDown()
    {
        if (buildMode)
        {
            //place object
        }
    }
    public void setBuildMode(bool status)
    {
        buildMode = status;
    }
}
