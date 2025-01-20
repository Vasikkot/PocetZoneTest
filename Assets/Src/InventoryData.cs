using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using System.IO;
[Serializable]
public class InventoryData : MonoBehaviour
{
    [SerializeField] private int maxSlots = 15;
    [SerializeField]
    List<InventorySlotData> slots;
    public event Action SlotChanged;
    public static InventoryData instance;
    
    public List<InventorySlotData> Slots { get => slots; private set => slots = value; }
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    void Start()
    {
        Slots = new List<InventorySlotData>(maxSlots);
        FillInv();
        LoadData();
    }


    public void AddItem(Item newItem, int count = 1)
    {
        ItemData newItemData = new ItemData
        {
            id = newItem.Id,
            dropChance = newItem.DropChance,
            spriteName = newItem.Sprite.name
        };

        InventorySlotData existingSlot = Slots.Find(slot => slot.ItemInInventoryData != null && slot.ItemInInventoryData.id == newItem.Id);

        if (existingSlot != null)
        {

            Slots[existingSlot.Index].CountOfItems += count;
        }
        else
        {

            InventorySlotData emptySlot = Slots.Find(slot => slot.ItemInInventoryData.id==0);

            if (emptySlot != null)
            {

                Slots[emptySlot.Index].ItemInInventoryData = newItemData;
                Slots[emptySlot.Index].CountOfItems = count;
            }
            else
            {
                Debug.Log("Инвентарь полон! Невозможно добавить новый предмет.");
            }
        }
        UpdateIndexes();
        SlotChanged?.Invoke();
    }
    public bool RemoveItem(int id, int count = 1)
    {
        InventorySlotData slotWithItem = Slots.Find(slot => slot.ItemInInventoryData != null && slot.ItemInInventoryData.id == id);

        if (slotWithItem != null)
        {
            
            if (slotWithItem.CountOfItems > count)
            {
                slotWithItem.CountOfItems -= count;
            }
            else
            {

                Slots[slotWithItem.Index].ItemInInventoryData = null;
                Slots[slotWithItem.Index].CountOfItems = 0;
            }

           
            UpdateIndexes();
            SlotChanged?.Invoke();
            return true;
        }
        else
        {
            return false;


        }
    }

    public void FillInv()
    {
        Slots.Clear();
        for (int i = 0; i < maxSlots; i++)
        {
            Slots.Add(new InventorySlotData(null, 0, i));
        }
    }


    public void UpdateIndexes()
    {
        for (int i = 0; i < Slots.Count; i++)
        {
            Slots[i].Index = i;
        }
    }
    public void SaveData()
    {
        string savePath = Path.Combine(Application.persistentDataPath, "SlotsData.json");
        string json = JsonUtility.ToJson(this); 
        File.WriteAllText(savePath, json);
    }

    public void LoadData()
    {
        
        string savePath = Path.Combine(Application.persistentDataPath, "SlotsData.json");
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            JsonUtility.FromJsonOverwrite(json, this); // Загружаем данные

            
            foreach (var slotData in Slots)
            {
                if (slotData.CountOfItems != 0) { 
                    Item item = Item.CreateItemFromData(slotData.ItemInInventoryData);
                }
                
            }

        }
        else
        {
            Debug.LogWarning("Save file not found");
        }
        Debug.Log(slots);
    }


    private void OnDestroy()
    {
        SaveData();
    }
}