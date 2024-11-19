using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMonster : Monsterbase, IDamagable
{
    private void Awake()
    {
        base.Init();
    }

    public void TakeHit(int damage)
    {
        CurrentHp -= damage;

        if (IsDead)
        {
            Debug.Log("die");
            Die();
        }
    }
}
