using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FinalBossFloor : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private float _trapProb;
    [SerializeField] private float _trapCoolDown;

    [SerializeField] private GameObject[,] _trapColliders;
    [SerializeField] private GameObject _cube;

    [SerializeField]    private int x, y;
    [SerializeField]    private Transform _startPoint;

    private bool _canTrap = true;
    private bool _isAttacking;
    int _trapIndex = 0;

    private bool _isEventOn;

    private void Start()
    {
        _trapColliders = new GameObject[x, y];


        for (int i = 0; i < _trapColliders.GetLength(0); i++)
        {
            for (int j = 0; j < _trapColliders.GetLength(1); j++)
            {
                _trapIndex++;
                Vector3 coordinates = new Vector3(i, j);
                var x = Instantiate(_cube, _startPoint.position + coordinates, Quaternion.identity);
                _trapColliders[i, j] = x;
            }
        }
    }

    private void Update()
    {
        if (!_isEventOn) return;
        
        if (_isAttacking)
        {
            if (_canTrap) TrapSetter();
        }
    }

    private void TrapSetter()
    {        
        _canTrap = false;
        StartCoroutine(TrapCoolDown(_trapCoolDown));

        for (int i = 0; i < _trapColliders.GetLength(0); i++)
        {
            for (int j = 0; j < _trapColliders.GetLength(1); j++)
            {                
                var sprite = _trapColliders[i, j].GetComponent<SpriteRenderer>();
                sprite.color = Color.red;                
            }
        }
        for (int i = 0; i < _trapColliders.GetLength(0); i++)
        {
            for (int j = 0; j < _trapColliders.GetLength(1); j++)
            {
                var x = IsTrapSet(_trapProb);
                if (x)
                {
                    var sprite = _trapColliders[i, j].GetComponent<SpriteRenderer>();
                    sprite.color = Color.white;
                }
            }
        }
       
    }

    private bool IsTrapSet(float prob)
    {
        float num = Random.Range(0, 101);

        if(prob >= num) return true;

        else  return false; 
    }

    private IEnumerator TrapCoolDown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown + Random.Range(1, 3));
        _canTrap = true;
    }
}
