using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public float maxLife = 10f;

    private float actualLife;

    public GameObject target;

    public float speed = 5f;

    private float originalSpeed;

    public float rangeAttack = 7f;

    public float stopDistance = 1f;

    public float knockbackDistance = 5f;

    public Rigidbody2D rgBody;

    public float movementForce = 100f;

    private bool damageTake;

    public float timerKonck = 1f;

    private float knockEnd;

    public float damage = 1f;

    private float originalForce;

    public float attackSpeed = 2f;

    public EnemyAttack ghostAttack;

    public float cooldownAttack = 2f;

    private float attackTimer;

    AudioSource myAudioSource;

    public AudioClip deadClip, shootClip;

    void Awake()
    {
        myAudioSource = GetComponent<AudioSource>();

        target = GameObject.FindGameObjectWithTag("PlayerTag");
    }
    
    void Start()
    {
        actualLife = maxLife;

        originalSpeed = speed;

        attackTimer = cooldownAttack;
    }

    void Update()
    {

        #region Movimiento Sexy

        if(damageTake != true)
        {
            if(Vector2.Distance(transform.position, target.transform.position) <= stopDistance)
            {
                if(attackTimer >= cooldownAttack)
                {
                    EnemyAttack newAttack = Instantiate(ghostAttack, transform.position, Quaternion.identity);

                    myAudioSource.PlayOneShot(shootClip);

                    newAttack.transform.up = (target.transform.position - transform.position).normalized;

                    attackTimer = 0;
                }
                
                speed = 0;

                if(attackTimer < cooldownAttack)
                {
                    attackTimer += Time.deltaTime;
                }
            }

            

            if(Vector2.Distance(transform.position, target.transform.position) <= rangeAttack && damageTake != true)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

                speed = originalSpeed;
            }

            
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
        myAudioSource.PlayOneShot(deadClip);
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
