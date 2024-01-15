using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnEvent : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private GameObject _enemy;

    [SerializeField] private float _enemyAmmount;
    [SerializeField] private float _cooldown;

    private bool _canSpawn = true;

    private float _coordinatesX, _coordinatesY;

    private void Update()
    {
        if(_canSpawn) SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        _canSpawn = false;
        for (int i = 0; i < _enemyAmmount; i++)
        {
            _coordinatesX = Random.Range(-3, 4);
            _coordinatesY = Random.Range(-3, 4);
            Instantiate(_enemy, new Vector2(_target.transform.position.x + _coordinatesX, _target.transform.position.y + _coordinatesY), Quaternion.identity);
        }

       StartCoroutine(SpawnCooldown());
    }

    private IEnumerator SpawnCooldown()
    {
        yield return new WaitForSeconds(_cooldown);
        _canSpawn = true;
    }
}
