using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemCountText;

    public void SetSlot(InventorySlotData slotData)
    {
        if (slotData.itemInInventoryData.id!=0)
        {
            itemIcon.sprite = Resources.Load<Sprite>(slotData.itemInInventoryData.spriteName);
            itemCountText.text = slotData.CountOfItems.ToString();
            itemIcon.enabled = true;
            itemCountText.enabled = true;
        }
        else
        {
            itemIcon.enabled = false;
            itemCountText.enabled = false;
        }
    }


    public void UpdateSlot(InventorySlotData slotData)
    {
        if (slotData.itemInInventoryData.id != 0)
        {
            itemIcon.sprite = Resources.Load<Sprite>(slotData.itemInInventoryData.spriteName);
            itemCountText.text = slotData.CountOfItems.ToString();
            itemIcon.enabled = true;
            itemCountText.enabled = true;
        }
        else
        {
            itemIcon.enabled = false;
            itemCountText.enabled = false;
        }
    }
}