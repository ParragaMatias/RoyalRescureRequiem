using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxLife = 10f;

    private float actualLife;

    public Attack myAttack;

    public GameObject target;

    public Rigidbody2D rgBody;

    public float speed = 5f;

    public float rangeAttack = 7f;

    public float stopDistance = 1f;

    public float knockbackDistance = 5f;

    public float movementForce = 5f;

    private bool damageTake;

    public float damage = 1f;

    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("PlayerTag");
    }
    void Start()
    {
        actualLife = maxLife;
    }

    void Update()
    {

        #region Movimiento Sexy

        if(damageTake == true)
        {
            return;
        }

        if(Vector2.Distance(transform.position, target.transform.position) <= stopDistance)
        {
            return;
        }

            

        if(Vector2.Distance(transform.position, target.transform.position) <= rangeAttack)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
        
        #endregion
    }

    void TakeDamage(float amount)
    {
        
        actualLife -= amount;

        print("el enemigo recibio " + amount + " de daño");

        if(actualLife <= 0)
        {
            print("El enemigo murio");
            Die();
            return;
        }

    }

    void FixedUpdate()
    {

        if(damageTake == true)
        {
            Vector2 instPos = target.transform.position;

            Vector2 knockbackDirection = ((Vector2)transform.position - instPos).normalized;

            KnockBackAplly(knockbackDirection);

            return;
        }

    }

    void KnockBackAplly(Vector2 vec)
    {
        rgBody.velocity = Vector2.zero;

        rgBody.AddForce(vec * movementForce, ForceMode2D.Impulse);

        damageTake = false;
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.layer == 8)
        {
            TakeDamage(col.gameObject.GetComponent<Attack>().GetDamage());
            
            damageTake = true;
        }
    }

    public float GetEnemyDamage()
    {
        return damage;
    }
}