using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    [SerializeField]  private List<Item> itemsList = new List<Item>(); // Список предметов
    [SerializeField]  GameObject StandartItenPref;
    [SerializeField] Transform parrent;
    public static  Dropper instanse;
    private void Awake()
    {
        if (instanse == null) 
            instanse = this;
        else
           Destroy(gameObject);
    }
    public void Drop(Transform parentTransform)
    {

        float totalChance = 0f;
        foreach (var loot in itemsList)
        {
            totalChance += loot.DropChance;
        }


        float randomValue = Random.Range(0f, totalChance);


        float currentChance = 0f;
        foreach (var loot in itemsList)
        {
            currentChance += loot.DropChance;
            if (randomValue <= currentChance)
            {
                var tmp = Instantiate(StandartItenPref, parentTransform.position, Quaternion.identity);
                tmp.GetComponent<Item>().SetNewItem(loot);
                
                return;
            }
        }
    }
}