using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDrop : MonoBehaviour
{
    [SerializeField] HP monsterHp;
    void Start()
    {
        monsterHp.onDie += MonsterHp_onDie;
    }
    private void OnDestroy()
    {
        monsterHp.onDie -= MonsterHp_onDie;
    }

    private void MonsterHp_onDie()
    {
        Dropper.instanse.Drop(gameObject.transform);
    }
}
