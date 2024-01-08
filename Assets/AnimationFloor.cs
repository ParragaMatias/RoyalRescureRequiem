using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFloor : MonoBehaviour
{
    [SerializeField] private float _animTime = 2.3f;
    private void Start()
    {
        StartCoroutine(Destroy());
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(_animTime);
        Destroy(gameObject);
    }
}
