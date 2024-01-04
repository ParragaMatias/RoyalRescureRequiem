using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float damage = 2f;

    public float attackSpeed = 2f;

    public float lifeTime = 1f;
    void Start()
    {
        Destroy(gameObject,lifeTime);
    }


    void Update()
    {

        transform.position += transform.up * attackSpeed * Time.deltaTime;
    }

    public float SetDamage()
    {
        Destroy(gameObject);
        return damage;
    }
}
