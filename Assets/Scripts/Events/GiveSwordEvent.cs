using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveSwordEvent : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 6)
        {
            StaticData._haveSword = true;
        }
    }
}
