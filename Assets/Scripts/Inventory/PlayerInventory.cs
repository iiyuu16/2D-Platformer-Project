using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private List<Item> items;
    [SerializeField] InventoryScreen inventoryScreen;
    private bool inventoryOpen;
    private InputAction inventoryAction;
    private float inventoryICD = 0.5f;
    private bool canOpen = true;

    private void Start()
    {
        inventoryAction = InputSystem.actions.FindAction("OpenInventory");
    }

    private void Update()
    {
        if (inventoryAction.IsPressed() && canOpen)
        {
            if (!inventoryOpen)
            {
                inventoryOpen = true;
                if (TutorialManager.Instance != null)
                {
                    TutorialManager.Instance.OpenedInventory();
                }
                inventoryScreen.OpenInventoryMenu(items);
            }
            else
            {
                inventoryOpen = false;
                inventoryScreen.CloseInventoryMenu();
            }
            canOpen = false;
        }
        if (!canOpen)
        {
            inventoryICD -= Time.deltaTime;
            if (inventoryICD < 0)
            {
                canOpen = true;
                inventoryICD = 0.5f;
            }
        }
    }

    public void AddItem(Item item)
    {
        items.Add(item);
    }

    public string CheckItem()
    {
        string itemID = "";
        foreach(Item item in items)
        {
            if (item.itemID == "key")
            {
                itemID = item.itemName;
            }
        }
        return itemID;
    }
}
