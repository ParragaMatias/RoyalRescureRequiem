using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollWP : MonoBehaviour
{
    [SerializeField] private float _lifeTime;
    private void Start()
    {
        StartCoroutine(DestroyCoolDown());
    }

    private IEnumerator DestroyCoolDown()
    {
        yield return new WaitForSeconds(_lifeTime);
        Object.Destroy(gameObject);
    }
}
