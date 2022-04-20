using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{

    public void FleshBagSelect()
    {
        Debug.Log("Fleshbag Selected");
        GameHandler.selectedObject = "FleshBags";
    }

    public void SpikeTrapSelect()
    {
        Debug.Log("SpikeTrap Selected");
        GameHandler.selectedObject = "SpikeTraps";
    }
}
