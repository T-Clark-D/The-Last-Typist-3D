using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TileScript : MonoBehaviour
{
    [SerializeField] public Material baseColor, offsetColor, hoverColor;
    private MeshRenderer MR;
    private bool buildMode = true;
    private MeshCollider MC;
    private bool offsetStatus;
    private NavMeshObstacle nmo;

    public void Init(bool isOffSet)
    {
        offsetStatus = isOffSet;
        MC = GetComponent<MeshCollider>();
        MR = GetComponent<MeshRenderer>();
        MR.material = isOffSet ? offsetColor : baseColor;
        nmo = GetComponent<NavMeshObstacle>();
    }

    private void OnMouseOver()
    {
        if(buildMode)
        MR.material = hoverColor;
    }
    private void OnMouseExit()
    {
        if (buildMode)
        {
            if (!nmo.isActiveAndEnabled)
            {
                MR.material = offsetStatus ? offsetColor : baseColor;
            }
        }
    }
    private void OnMouseDown()
    {
        if (buildMode)
        {
            if (!nmo.isActiveAndEnabled)
            {
                nmo.enabled = true;
                MR.material = hoverColor;
            }
            else
            {
                nmo.enabled = false;
                MR.material = offsetStatus ? offsetColor : baseColor;
            }
        }
    }
    public void setBuildMode(bool status)
    {
        buildMode = status;
    }
}
