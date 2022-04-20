using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CraftingSlotManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
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
        recipe.Item = item;
        recipe.craft(hud.Inventory);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ToggleCanvas();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        ToggleCanvas();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            CraftItem();
            Debug.Log("crafting item" + item.name);
        }
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            ToggleItemSelected();
            Debug.Log("selecting item" + item.name);
        }
    }
}
