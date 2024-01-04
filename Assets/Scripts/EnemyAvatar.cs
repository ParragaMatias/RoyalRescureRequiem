using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAvatar : MonoBehaviour
{
    [SerializeField] private int _dmg; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        var x = other.gameObject.GetComponent<IDamageable>();
        if (x != null)
            x.TakeDamage(_dmg);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var x = other.gameObject.GetComponent<IDamageable>();
        if (x != null)
            x.TakeDamage(_dmg);
    }
}
