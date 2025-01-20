using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject slotPrefab; 
    private InventoryData inventoryData; 
    [SerializeField] private Transform slotsParent; 

    private List<GameObject> slotInstances = new List<GameObject>();

    void Start()
    {
        inventoryData = InventoryData.instance;
        InitializeInventoryUI();
        inventoryData.SlotChanged += UpdateInventoryUI;
    }

    public void InitializeInventoryUI()
    {
        foreach (var slot in slotInstances)
        {
            Destroy(slot);
        }
        slotInstances.Clear();

        foreach (var slotData in inventoryData.Slots)
        {
            GameObject newSlot = Instantiate(slotPrefab, slotsParent);

            InventorySlotUI slotUI = newSlot.GetComponent<InventorySlotUI>();
            if (slotUI != null)
            {
                slotUI.SetSlot(slotData);
            }
            slotInstances.Add(newSlot);
        }
    }

    public void UpdateInventoryUI()
    {
        for (int i = 0; i < inventoryData.Slots.Count; i++)
        {
            InventorySlotUI slotUI = slotInstances[i].GetComponent<InventorySlotUI>();
            if (slotUI != null)
            {
                slotUI.UpdateSlot(inventoryData.Slots[i]);
            }
        }
    }
}