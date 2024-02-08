using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokeFloor : MonoBehaviour
{
    [SerializeField] private string _animBrokeTriggerName;

    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (StaticData._canBroke) _anim.SetTrigger(_animBrokeTriggerName);
    }
}
