using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAvatar : MonoBehaviour
{
    [SerializeField] private int _heal;
    [SerializeField] private int _playerLayer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var x = other.gameObject.GetComponent<IHeleable>();
        if (x != null && other.gameObject.layer == _playerLayer)
        {
            x.TakeHeal(_heal);
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var x = other.gameObject.GetComponent<IHeleable>();
        if (x != null && other.gameObject.layer == _playerLayer)
        {
            x.TakeHeal(_heal);
            Destroy(gameObject);
        }

    }
}