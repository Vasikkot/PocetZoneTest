using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EAttack : MonoBehaviour
{
     HP playerHP;
    [SerializeField] EMove mover;
    [SerializeField] float reloadTime;
    private bool isAttack = false;
    
    
    private void AttackOut()
    {
        if (!isAttack) {

            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        
        
            isAttack = true;

            playerHP.TakeDmg();



            yield return new WaitForSeconds(reloadTime);

            isAttack = false;
        
        yield return null;
    }
    void Start()
    {
        mover.CloseToPlayer += AttackOut;
        playerHP = FindAnyObjectByType(typeof(PMove)).GetComponent<HP>();
    }

    private void OnDestroy()
    {
        mover.CloseToPlayer -= AttackOut;
    }
}
