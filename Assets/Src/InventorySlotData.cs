using System;
using UnityEngine;

[Serializable]
public class InventorySlotData
{
    [SerializeField]
    public ItemData itemInInventoryData; // Данные вместо самого объекта Item
    [SerializeField]
    int countOfItems;
    [SerializeField]
    int index;

    public int CountOfItems { get => countOfItems; set => countOfItems = value; }
    public ItemData ItemInInventoryData { get => itemInInventoryData; set => itemInInventoryData = value; }
    public int Index { get => index; set => index = value; }

    public InventorySlotData(ItemData item, int count, int index)
    {
        ItemInInventoryData = item;
        CountOfItems = count;
        Index = index;
    }
}