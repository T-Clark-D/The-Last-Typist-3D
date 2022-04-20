using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TileScript : MonoBehaviour
{
    [SerializeField] public Material baseColor, offsetColor, hoverColor;
    private MeshRenderer MR;
    private MeshCollider MC;
    private bool offsetStatus;
    private GameObject instantiatedObject;

    public GameObject fleshBagPrefab;
    public GameObject spikesPrefab;
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
        if (GameHandler.buildMode)
            MR.material = hoverColor;
    }

    private void OnMouseExit()
    {
        if (GameHandler.buildMode)
        {
            MR.material = offsetStatus ? offsetColor : baseColor;
            if (!nmo.isActiveAndEnabled)
            {
                MR.material = offsetStatus ? offsetColor : baseColor;
            }
        }
    }
    private void OnMouseDown()
    {
        if (GameHandler.buildMode)
        {

            if (GameHandler.selectedObject == "FleshBags")
            {
                instantiatedObject = Instantiate(fleshBagPrefab, gameObject.transform.position, new Quaternion(-0.50000006f, -0.49999994f, -0.49999997f, 0.50000006f));
                //carve a whole in the nav mesh
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
            if (GameHandler.selectedObject == "SpikeTraps")
            {
                instantiatedObject = Instantiate(spikesPrefab, gameObject.transform.position, Quaternion.identity);

            }
        }
    }
}
