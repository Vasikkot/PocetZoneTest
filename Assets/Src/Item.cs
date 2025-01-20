using System;
using UnityEngine;

[Serializable]
public class Item : MonoBehaviour
{
    [SerializeField] string ImagePath;
    private Sprite sprite;
    [SerializeField] private float dropChance = 0.1f;
    [SerializeField] private int id;

    public float DropChance
    {
        get => dropChance;
        set => dropChance = value;
    }

    public int Id
    {
        get => id;
        private set => id = value;
    }
    public Sprite Sprite
    {
        get => sprite; private set
        {
            sprite = value;
            ImagePath = sprite.name;
        }
    }

    void Awake()
    {
        if (Sprite == null)
            Sprite = GetComponent<SpriteRenderer>().sprite;
    }

    public void SetNewItem(Item newItem)
    {
        GetComponent<SpriteRenderer>().sprite = newItem.Sprite;
        sprite = newItem.Sprite;
        Id = newItem.Id;
        DropChance = newItem.DropChance;
    }
    public Item Clone()
    {
        return (Item)this.MemberwiseClone();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Проверяем тег игрока
        {
            InventoryData.instance?.AddItem(this);
            Destroy(gameObject);
        }
    }
    public static Item CreateItemFromData(ItemData itemData)
    {
        if (itemData.spriteName != "")
        {
            Item newItem = new Item();
            newItem.Id = itemData.id;
            newItem.DropChance = itemData.dropChance;

            newItem.Sprite = Resources.Load<Sprite>(itemData.spriteName);

            return newItem;
        }
        else {
            Item newItem = new Item();
            return newItem;
        }
    }



}