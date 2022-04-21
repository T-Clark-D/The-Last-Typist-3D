using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

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
            if (GameHandler.selectedObject == "" || IsPointerOverUIElement())
            {
                return;
            }

            if (GameHandler.selectedObject == "FleshBags")
            {
                if (GameHandler.fleshCount >= 1)
                {
                    instantiatedObject = Instantiate(fleshBagPrefab, gameObject.transform.position, new Quaternion(-0.50000006f, -0.49999994f, -0.49999997f, 0.50000006f));
                    GameHandler.fleshCount -= 1;
                }
                else
                {
                    Debug.Log("Not enough Flesh");
                }

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
                if (GameHandler.fleshCount >= 2)
                {
                    instantiatedObject = Instantiate(spikesPrefab, gameObject.transform.position, new Quaternion(-0.50000006f, -0.49999994f, -0.49999997f, 0.50000006f));
                    GameHandler.fleshCount -= 2;
                }
                else
                {
                    Debug.Log("Not enough Flesh");
                }
            }
        }
    }

    #region Actual miracle code https://answers.unity.com/questions/1095047/detect-mouse-events-for-ui-canvas.html

    // Returns 'true' if we touched or hovering on Unity UI element.
    public static bool IsPointerOverUIElement()
    {
        return IsPointerOverUIElement(GetEventSystemRaycastResults());
    }

    // Returns 'true' if we touched or hovering on Unity UI element.
    public static bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if (curRaysastResult.gameObject.layer == LayerMask.NameToLayer("UI"))
                return true;
        }
        return false;
    }

    // Gets all event systen raycast results of current mouse or touch position.
    static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
    }

    #endregion
}
