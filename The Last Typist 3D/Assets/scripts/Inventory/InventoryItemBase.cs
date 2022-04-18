using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemBase : MonoBehaviour, IInventoryItem
{
    public virtual string Name
    {
        get
        {
            return "_base_item";
        }
    }

    public Sprite _Image;

    public Sprite Image
    {
        get
        {
            return _Image;
        }
    }

    public InventorySlot Slot { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public virtual void OnPickup()
    {
        //TODO add logic what happens when metal is picked up
        gameObject.SetActive(false);
    }
    public virtual void OnDrop()
    {
        //TODO ... Move a logic like this to a base class or helper
        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000))
        {
            gameObject.SetActive(true);
            gameObject.transform.position = hit.point;
        }
    }

    public virtual void OnUse()
    {
        //TODO ... Move a logic like this to a base class or helper
        
    }
}
