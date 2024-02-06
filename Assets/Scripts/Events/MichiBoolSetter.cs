using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MichiBoolSetter : MonoBehaviour
{
    [SerializeField] private int _playerLayer;
    [SerializeField] private KeyCode _interactKey;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == _playerLayer) ChangeBool(); 
    }
    
    private void ChangeBool()
    {
        StaticData._michiTaim = true;
    }
}
