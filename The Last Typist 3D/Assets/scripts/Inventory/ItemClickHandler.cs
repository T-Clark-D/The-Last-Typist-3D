using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemClickHandler : MonoBehaviour
{
    public HUD hud;
    public InventoryItemBase item;
    public CraftingRecipe recipe;
    public ResourceManager ResourceManager;

    Canvas recipeCanvas;

    // Start is called before the first frame update
    void Start()
    {
        recipeCanvas = gameObject.transform.Find("RecipeCanvas").GetComponent<Canvas>();
        if (recipe != null)
        {
            recipeCanvas.enabled = false;

        }

        recipe.ResourceManager = ResourceManager;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) Debug.Log("Pressed left click.");
        if (Input.GetMouseButtonDown(1))
        {
            //Debug.Log("Pressed right click.");
            CraftItem();
        }
        if (Input.GetMouseButtonDown(2)) Debug.Log("Pressed middle click.");
    }

    public void OnItemClicked()
    {
        //ItemDragHandler dragHandler =
        //gameObject.transform.Find("ItemImage").GetComponent<ItemDragHandler>();
        //IInventoryItem item = dragHandler.Item;
        
        ToggleCanvas();
        ToggleItemSelected();
    }

    

    public void ToggleCanvas()
    {
        if (((Canvas)recipeCanvas).enabled == true)
        {
            recipeCanvas.enabled = false;
        }
        else
        {
            recipeCanvas.enabled = true;
        }
    }

    public void ToggleItemSelected()
    {
        if (item.isSelected)
        {
            hud.Inventory.UseItem(item, false);
        }
        else
        {
            hud.Inventory.UseItem(item, true);
        }
    }

    public void CraftItem()
    {
        recipe.craft(hud.Inventory);
    }
}
