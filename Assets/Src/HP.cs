using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    private float currentHp;
    public event System.Action onDie;
    [SerializeField] private float maxHp;
    [SerializeField] private Image hpImg;


    public void TakeDmg(float damage = 1)
    {
        currentHp-= damage;
        if (currentHp <= 0)
        {
            onDie?.Invoke();
            Destroy(gameObject);
        }
        UpdateUi();

    }
    private void UpdateUi()
    {
        hpImg.fillAmount = currentHp / maxHp;
    }
    void Start()
    {
        currentHp = maxHp;
        UpdateUi();
    }

 
    
}
