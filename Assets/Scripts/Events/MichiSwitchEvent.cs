using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MichiSwitchEvent : MonoBehaviour
{
    [SerializeField] private GameObject _michi1, _michi2;
    private bool _isSwitch = false;
    private void Update()
    {
        SwitchMichi();
    }

    private void SwitchMichi()
    {
        if (StaticData._michiTaim == true && _isSwitch == false)
        {
            _michi1.SetActive(false);
            _michi2.SetActive(true);
        }

    }
}
