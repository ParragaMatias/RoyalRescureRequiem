using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalEventTrigger : MonoBehaviour
{
    [SerializeField] private int _playerLayer;

    [SerializeField] private FinalBossFloor _floorTrigger;
    [SerializeField] private EnemySpawnEvent _enemyTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == _playerLayer)
        {
            _floorTrigger._isEventOn = true;
            _enemyTrigger._eventStart = true;
        }
    }
}
