using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject inv;
    public static UIManager instance;

    private event Action _shootBinPressed;

    public event Action ShootBtnPressed
    {
        add
        {
            _shootBinPressed += value;

        }
        remove
        {
            _shootBinPressed -= value;
        }
    }
    PShoot shooter;
    private void Awake()
    {
            if (instance == null)
            instance = this;
            else
            Destroy(gameObject);
    }

    void Start()
    {
        
        shooter =  FindObjectOfType<PShoot>();
        
    }

    public void ShootBtn()
    {
       
         _shootBinPressed?.Invoke();
    }

    public void InventorytBtn()
    {
        if (inv.activeSelf)
        {
            inv.SetActive(false);
        }
        else
        {
            inv.SetActive(true);
        }
    }



}
