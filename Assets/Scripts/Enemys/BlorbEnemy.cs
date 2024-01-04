using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using UnityEngine;

[SelectionBase]
public class BlorbEnemy : MonoBehaviour, IDamageable
{
    [Header("Values")]
    [SerializeField] private float _maxHP;
    [SerializeField] private float _speed;
    [SerializeField] private float _dmg;
    [SerializeField] private float _viewDistance;
    [SerializeField] private float _stopDistance;
    [SerializeField] private float _knockForce;
    [SerializeField] private float _rollForce;
    [SerializeField] private float _rollTime;
    [SerializeField] private float _rollStunTime;

    [SerializeField] private GameObject _target;
    [SerializeField] private GameObject _rollWP;

    private float _actualHP;
    private bool _targetDetected;
    private bool _canRoll;
    private bool _rollEnd;
    private bool _canMove;

    private Vector3 _knockDir;

    private Rigidbody2D _rb;
    private Animator _animator;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _actualHP = _maxHP;
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        IsEnemyDetected();

        if (Vector3.Distance(transform.position, _target.transform.position) <= _stopDistance && !_canRoll) StartCoroutine(RollAttack());
    }

    private void FixedUpdate()
    {
        if (!_canMove) return;
        if(_targetDetected && _rollEnd) Movement();
    }

    private void IsEnemyDetected()
    {
        float dis = (Vector3.Distance(transform.position, _target.transform.position));
        if (dis <= _viewDistance && dis > _stopDistance) { _targetDetected = true; }
        else { _targetDetected = false; }
    }

    private IEnumerator RollAttack()
    {
        _canMove = false;
        if (!_canRoll)
        {
            _canRoll = true;
            _rollEnd = false;
        }
        _rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(0.5f);

        Vector3 attackDir = (_target.transform.position - transform.position).normalized;
        _rb.AddForce(attackDir * _rollForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(_rollTime);
        _canRoll = false;
        _rollEnd = true;
        yield return new WaitForSeconds(_rollStunTime);
        _canMove = true;
    }

    private void Movement()
    {
        _knockDir = (_target.transform.position - transform.position);

        Vector3 dir = (_target.transform.position - transform.position).normalized;

        _rb.MovePosition(transform.position + dir * _speed * Time.deltaTime);
    }

    private void KnockBackAplly(Vector2 vec)
    {
        _rb.velocity = Vector2.zero;
        _rb.AddForce(vec * _knockForce, ForceMode2D.Impulse);
    }

    public void TakeDamage(int dmg)
    {
        Vector2 instPos = _target.transform.position;
        Vector2 knockbackDirection = ((Vector2)transform.position - instPos).normalized;
        KnockBackAplly(knockbackDirection);
        _actualHP -= dmg;

        if( _actualHP <= 0 )
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        Object.Destroy(gameObject);
        yield return null;
    }
}
