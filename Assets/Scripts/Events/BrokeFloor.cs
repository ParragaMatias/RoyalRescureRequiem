using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokeFloor : MonoBehaviour
{
    [SerializeField] private string _animBrokeTriggerName;
    [SerializeField] private int _enemyLayer;

    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (StaticData._canBroke) _anim.SetTrigger(_animBrokeTriggerName);
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.layer == _enemyLayer) Destroy(col.gameObject);
    }
}
