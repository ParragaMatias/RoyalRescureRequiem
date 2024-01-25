using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private float _upSpeed;
    [SerializeField] private float _upTime;

    [SerializeField] private float _enemyAmmount;
    [SerializeField] private float _cooldown;

    [SerializeField] private bool _canSpawn = true, _eventStart, _isUp;

    private float _coordinatesX, _coordinatesY;


    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _isUp = true;
    }

    private void Update()
    {
        if(_canSpawn) SpawnEnemy();

        if (_eventStart) 
        {
            _eventStart = false;
            StartCoroutine(FinalBattleEvent());
        } 
    }


    private IEnumerator FinalBattleEvent()
    {
        yield return new WaitForSeconds(2f);
        _anim.SetTrigger(_flyEventAnim);
        Movement(1);
        yield return new WaitForSeconds(1.5f);
        _canSpawn = true;
        _anim.SetBool(_flyingBoolAnim, true);
        yield return new WaitForSeconds(_upTime);
        _anim.SetBool(_flyingBoolAnim, false);
        Movement(-1);
        yield return new WaitForSeconds(5f);
        _eventStart = true;
    }

    private void Movement(int i)
    {
        //while (transform.position.y <= transform.position.y * 5)
        //{
            //transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.up * 5, _upSpeed * Time.deltaTime);
        //}
    }

    private void SpawnEnemy()
    {
        _canSpawn = false;
        _anim.SetTrigger(_spawnEventAnim);
        for (int i = 0; i < _enemyAmmount; i++)
        {
            print(i);
            _coordinatesX = Random.Range(-4, 4);
            _coordinatesY = Random.Range(-4, 4);
            Instantiate(_enemy, new Vector2(_target.transform.position.x + _coordinatesX, _target.transform.position.y + _coordinatesY), Quaternion.identity);
        }

       //StartCoroutine(SpawnCooldown());
    }

    private IEnumerator SpawnCooldown()
    {
        yield return new WaitForSeconds(_cooldown);
        _canSpawn = true;
    }
}
