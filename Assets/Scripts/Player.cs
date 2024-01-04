using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IDamageable
{
    public float maxLife = 20f;

    private float currentLife;
    
    public float speed = 5f;

    public Attack myAttack;

    public Transform attackArriba;

    public Transform attackIzquierda;

    public Transform attackDerecha;

    public Transform attackAbajo;

    public float attackDamage = 2f;

    private int attackDirection;

    public float fireRate = 2f;

    private float fireTimer;

    private bool isAttacking;

    public float stopTime = 5f;
    
    private float stopTimer;

    private bool isAlive = true;

    private Rigidbody2D rgb2D;

    private Vector2 direccion;

    AudioSource myAudioSource;

    public AudioClip hurtClip, attackClip, deadClip;

    public AudioSource villageMusic, woodsMusic;

    public CanvasManager canvasManager;

    public Castillo myCastle;

    public Transform spawnPointCastle;

    float percentil;

    public float cameraShakeIntencity = 5f;

    public float cameraShakeFrecuency = 5f;

    public float cameraShakeTime = 0.5f;

    

    void Awake()
    {
        myAudioSource = GetComponent<AudioSource>();
        
        currentLife = maxLife;

        if(StaticData.firstStart != false)
        {
            StaticData.lastHealth = maxLife;
        }
    }
    
    void Start()
    {

        fireTimer = fireRate;

        stopTimer = stopTime;

        rgb2D = GetComponent<Rigidbody2D>();
        
        if(StaticData.firstStart != false)
        {
            currentLife = StaticData.lastHealth;
            StaticData.firstStart = false;
        }

    }

    //float lastMovementDesireX;
    
    //float lastMovementDesireY;

    //Vector3 lastMovement;

    void Update()
    {      
        if(!isAlive)
        {
            return;
        }
        
        #region MovimientoRestringido a 4 direcciones (no funca)

        //if(Input.GetAxisRaw("Horizontal") != 0 && Input.GetAxisRaw("Vertical") != 0)
        //{
            //transform.position += lastMovement * speed * Time.deltaTime;
            //return;
        //}
        
        //if(Input.GetAxisRaw("Horizontal") != 0)
        //{
            //lastMovementDesireX = Input.GetAxisRaw("Horizontal");
            //transform.position += new Vector3(lastMovementDesireX,0,0) * speed * Time.deltaTime;
            //lastMovement = new Vector3(lastMovementDesireX, 0, 0);
            //return;
        //}

        //if(Input.GetAxisRaw("Vertical") != 0)
        //{
            //lastMovementDesireY = Input.GetAxisRaw("Vertical");
            //transform.position += new Vector3(0,lastMovementDesireY,0) * speed * Time.deltaTime;
            //lastMovement = new Vector3(0,lastMovementDesireY, 0);
            //return;
        //}


        #endregion

        //transform.position += direction * speed * Time.deltaTime;


        #region Attack Direction

        if(Input.GetKeyDown(KeyCode.W))
        {
            attackDirection = 0;
        }
        
        if(Input.GetKeyDown(KeyCode.A))
        {
            attackDirection = 1;
        }

        if(Input.GetKeyDown(KeyCode.D))
        {
            attackDirection = 2;
        }

        if(Input.GetKeyDown(KeyCode.S))
        {
            attackDirection = 3;
        }        

        #endregion
        
        #region Ataque en 4 direcciones

        if(Input.GetKeyDown(KeyCode.Mouse0) && attackDirection == 0 && fireTimer >= fireRate)
        {
            
            Attack newAttack = Instantiate(myAttack, attackArriba.position, attackArriba.rotation);

            isAttacking = true;

            myAudioSource.PlayOneShot(attackClip);
        
            newAttack.SetDamage(attackDamage);

            fireTimer = 0;

            stopTimer = 0;
        }

        if(Input.GetKeyDown(KeyCode.Mouse0) && attackDirection == 1 && fireTimer >= fireRate)
        {
            Attack newAttack = Instantiate(myAttack, attackIzquierda.position, attackArriba.rotation);

            isAttacking = true;

            myAudioSource.PlayOneShot(attackClip);
        
            newAttack.SetDamage(attackDamage);

            fireTimer = 0;

            stopTimer = 0;
        }
        
        if(Input.GetKeyDown(KeyCode.Mouse0) && attackDirection == 2 && fireTimer >= fireRate)
        {
            Attack newAttack = Instantiate(myAttack, attackDerecha.position, attackArriba.rotation);

            isAttacking = true;

            myAudioSource.PlayOneShot(attackClip);
        
            newAttack.SetDamage(attackDamage);

            fireTimer = 0;

            stopTimer = 0;
        }

        if(Input.GetKeyDown(KeyCode.Mouse0) && attackDirection == 3 && fireTimer >= fireRate)
        {
            Attack newAttack = Instantiate(myAttack, attackAbajo.position, attackArriba.rotation);

            isAttacking = true;

            myAudioSource.PlayOneShot(attackClip);
        
            newAttack.SetDamage(attackDamage);

            fireTimer = 0;

            stopTimer = 0;
        }

        if(fireTimer < fireRate)
        {
            fireTimer += Time.deltaTime;
        }
    
        if(stopTimer < stopTime)
        {
            stopTimer += Time.deltaTime;
        }

        if(stopTimer >= stopTime)
        {
            isAttacking = false;
        }

            if(StaticData.castleChecker == true)
        {  
        
            currentLife = StaticData.lastHealth;
            
            percentil = StaticData.lastHealth / maxLife;

            canvasManager.UpdateLife(percentil, StaticData.lastHealth);

            transform.position = spawnPointCastle.position;
            
            StaticData.castleChecker = false;
        }

    }

    
    
    void FixedUpdate()
    {
        if(isAttacking == true)
        {
            return;
        }
        
        float AxisV = Input.GetAxisRaw("Vertical");
                
        float AxisH = Input.GetAxisRaw("Horizontal");

        Vector2 newPosition = transform.position + new Vector3(AxisH, AxisV, 0).normalized * speed * Time.fixedDeltaTime;

        rgb2D.MovePosition(newPosition);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.layer == 7)
        {
            TakeDamage(col.gameObject.GetComponent<Enemy>().GetEnemyDamage());
        }

        if(col.gameObject.layer == 10)
        {
            TakeDamage(col.gameObject.GetComponent<Ghost>().GetEnemyDamage());
        }


    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.layer == 9)
        {
            TakeDamage(col.gameObject.GetComponent<EnemyAttack>().SetDamage());
        }

        if(col.gameObject.layer == 11 && woodsMusic.isPlaying == false)
        {
            villageMusic.Stop();
            woodsMusic.Play();
        }

        if(col.gameObject.layer == 12 && villageMusic.isPlaying == false)
        {
            woodsMusic.Stop();
            villageMusic.Play();
        }
    }

    public void TakeDamage(float amount)
    {
        currentLife -= amount;
        
        StaticData.lastHealth = currentLife;

        CinemachineManager.instance.MoverCamara(cameraShakeIntencity, cameraShakeFrecuency, cameraShakeTime);

        myAudioSource.PlayOneShot(hurtClip);

        if(currentLife <= 0)
        {
            currentLife = 0;
            PlayerDie();
            return;
        }

        percentil = StaticData.lastHealth/maxLife;
        canvasManager.UpdateLife(percentil, StaticData.lastHealth);

        print("Vida Actual = " + currentLife);

    }

    void PlayerDie()
    {
        myAudioSource.PlayOneShot(deadClip);
        print("Moriste.");
        isAlive = false;

        SceneManager.LoadScene(4);
    }

    #endregion

    public float SetMaxLife()
    {
        return maxLife;
    }

    public void TakeDamage(int dmg)
    {
        currentLife -= dmg;

        StaticData.lastHealth = currentLife;

        CinemachineManager.instance.MoverCamara(cameraShakeIntencity, cameraShakeFrecuency, cameraShakeTime);

        myAudioSource.PlayOneShot(hurtClip);

        if (currentLife <= 0)
        {
            currentLife = 0;
            PlayerDie();
            return;
        }

        percentil = StaticData.lastHealth / maxLife;
        canvasManager.UpdateLife(percentil, StaticData.lastHealth);

        print("Vida Actual = " + currentLife);

    }
}