using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class EnemySpawnEvent : MonoBehaviour
{
    [Header("Animator")]
    private Animator _anim;
    [SerializeField] private string _spawnEventAnim;
    [SerializeField] private string _dmgEventAnim;
    [SerializeField] private string _flyEventAnim;
    [SerializeField] private string _deathEventAnim;
    [SerializeField] private string _flyingBoolAnim;

    [SerializeField] private GameObject _target, _upPosition, _enemy, _startPosition;

    [Header("Values")]
    [SerializeField] private float _lifePoints;
    [SerializeField] private float _upSpeed;
    [SerializeField] private float _upTime;

    [SerializeField] private float _enemyAmmount;
    [SerializeField] private float _cooldown;

    [SerializeField] private bool _canSpawn = true, _canMove = false, _canDMG = false;
    [SerializeField] private Attack _dmg;

    private float _coordinatesX, _coordinatesY;
    private int dir = 1;

    public bool _isLive = true , _eventStart = false;


    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!_isLive) return;

        if (_eventStart) 
        {
            _eventStart = false;
            StartCoroutine(FinalBattleEvent());
        }

        if (dir == 1)
        {
            MoveUp();
        }

        else if(dir == 2)
        {
            MoveDown();
        }

    }


    private IEnumerator FinalBattleEvent()
    {
        yield return new WaitForSeconds(2f);
        _canDMG = false;
        _anim.SetTrigger(_flyEventAnim);
        dir = 1;
        yield return new WaitForSeconds(2f);
        _anim.SetTrigger(_spawnEventAnim);
        _anim.SetBool(_flyingBoolAnim, true);
        yield return new WaitForSeconds(_upTime);
        dir = 2;
        _canDMG = true;
        _canSpawn = false;
        _anim.SetBool(_flyingBoolAnim, false);
        yield return new WaitForSeconds(5f);
        _eventStart = true;
    }

    private void MoveUp()
    {
        if (transform.position.y <= _startPosition.transform.position.y + Vector3.up.y * 0.5f)
        {
            transform.position += (Vector3.up * _upSpeed * Time.deltaTime);
        }
    }

    private void MoveDown()
    {
        if (transform.position.y >= _startPosition.transform.position.y + Vector3.down.y * 0.25f)
        {
            transform.position += (Vector3.down * _upSpeed * Time.deltaTime);
        }
    }

    public void SpawnEnemy()
    {
        float i = 0;
        while(i < _enemyAmmount)
        {
            i++;
            _coordinatesX = Random.Range(-4, 4);
            _coordinatesY = Random.Range(-4, 4);
            Instantiate(_enemy, new Vector2(_target.transform.position.x + _coordinatesX, _target.transform.position.y + _coordinatesY), Quaternion.identity);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            TakeDamage(_dmg.attackDMG);
        }
    }


    public void TakeDamage(float dmg)                                                                            
    {
        if (_canDMG)
        {
            _anim.SetTrigger(_dmgEventAnim);
            _lifePoints -= dmg;
            _canDMG = false;

            if(_lifePoints <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        _isLive = false;
        _anim.SetBool(_deathEventAnim, true);
    }
}
