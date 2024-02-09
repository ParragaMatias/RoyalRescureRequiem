using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    public float lifeTime = 1f;

    public float attackDMG = 1f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }
    
    void Update()
    {
    
    }

    public void SetDamage(float damageAmount)
    {
        attackDMG = damageAmount;
    }

    public float GetDamage()
    {
        return attackDMG;
    }

}
