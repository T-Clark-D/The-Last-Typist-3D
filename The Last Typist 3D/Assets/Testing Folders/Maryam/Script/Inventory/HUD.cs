
using System;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour 
{
    public Inventory Inventory;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("adding item");
        Inventory.ItemAdded += InventoryScript_ItemAdded;
        Inventory.ItemRemoved += InventoryScript_ItemRemoved;
    }

    private void InventoryScript_ItemAdded(object sender, IInventoryEventArgs e)
    {
        Transform inventoryPanel = transform.Find("InventoryPanel");
        int index = -1;
        foreach (Transform slot in inventoryPanel)
        {
            index++;
            //Border...image
            Transform imageTransform = slot.GetChild(0).GetChild(0);
            Transform textTransform = slot.GetChild(0).GetChild(1);

            UnityEngine.UI.Image image = imageTransform.GetComponent<UnityEngine.UI.Image>();
            Text txtCount = textTransform.GetComponent<Text>(); 
           
            //we found empty slot
            if (index == e.Item.Slot.Id)
            {
                image.enabled = true;
                image.sprite = e.Item.Image;

                int itemCount = e.Item.Slot.Count;
                if (itemCount > 0)
                {
                    txtCount.text = itemCount.ToString();
                }
                else
                {
                    txtCount.text = "0";
                }
                
                //store a reference to the item
                //itemDragHandler.Item = e.Item;
                //TODO Store a reference to the item
                break;
            }
        }
    }

    private void InventoryScript_ItemRemoved(object sender, IInventoryEventArgs e)
    {
        Transform inventoryPanel = transform.Find("InventoryPanel");
        int index = -1;
        foreach (Transform slot in inventoryPanel)
        {
            index++;

            //Border...image
            Transform imageTransform = slot.GetChild(0).GetChild(0);
            Transform textTransform = slot.GetChild(0).GetChild(1);

            UnityEngine.UI.Image image = imageTransform.GetComponent<UnityEngine.UI.Image>();
            Text txtCount = textTransform.GetComponent<Text>();

                       
            //found the slot to remove from
            if (index == e.Item.Slot.Id)
            {
                int itemCount = e.Item.Slot.Count;

                if(itemCount < 2)
                {
                    txtCount.text = "";
                }
                else
                {
                    txtCount.text = itemCount.ToString();
                }

                if(itemCount == 0)
                {
                    image.enabled = false;
                    image.sprite = null;
                }
                break;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
