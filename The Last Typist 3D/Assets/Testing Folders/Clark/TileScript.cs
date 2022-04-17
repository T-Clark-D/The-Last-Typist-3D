using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    [SerializeField] public Material baseColor, offsetColor, hoverColor;
    private MeshRenderer MR;
    private MeshCollider MC;
    private bool offsetStatus;
    private GameObject instantiatedObject;

    public GameObject fleshBagPrefab;
    public GameObject spikesPrefab;

    public void Init(bool isOffSet)
    {
        offsetStatus = isOffSet;
        MC = GetComponent<MeshCollider>();
        MR = GetComponent<MeshRenderer>();
        MR.material = isOffSet ? offsetColor : baseColor;
    }

    private void OnMouseOver()
    {
        if(GameHandler.buildMode)
        MR.material = hoverColor;
    }

    private void OnMouseExit()
    {
        if (GameHandler.buildMode)
        MR.material = offsetStatus ? offsetColor : baseColor;
    }
    private void OnMouseDown()
    {
        if (GameHandler.buildMode)
        {
            
            if(GameHandler.selectedObject == "FleshBags")
            {
                instantiatedObject = Instantiate(fleshBagPrefab, gameObject.transform.position , new Quaternion(-0.50000006f, -0.49999994f, -0.49999997f, 0.50000006f));
                //carve a whole in the nav mesh
            }
            if (GameHandler.selectedObject == "SpikeTraps")
            {
                instantiatedObject = Instantiate(spikesPrefab, gameObject.transform.position, Quaternion.identity);
                //carve a whole in the nav mesh
            }
        }
    }
}
