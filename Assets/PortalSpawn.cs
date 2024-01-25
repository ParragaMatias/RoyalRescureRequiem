using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PortalSpawn : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;

    private void Awake()
    {
        _enemy = GameObject.FindGameObjectWithTag("BlorbEnemy");
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }
    private IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(0.7f);
        Instantiate(_enemy, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
