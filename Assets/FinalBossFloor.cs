using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class FinalBossFloor : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private float _trapProb;
    [SerializeField] private float _trapCoolDown;

    [SerializeField] private GameObject[,] _trapSlabs;
    [SerializeField] private GameObject _cube;

    [SerializeField] private int x, y;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private GameObject _animatedSlab;

    private Animation _animation;

    private bool _canTrap = true;
    private bool _isAttacking;
    int _trapIndex = 0;

    private bool _isEventOn;

    private void Start()
    {
        _trapSlabs = new GameObject[x, y];


        for (int i = 0; i < _trapSlabs.GetLength(0); i++)
        {
            for (int j = 0; j < _trapSlabs.GetLength(1); j++)
            {
                _trapIndex++;
                Vector3 coordinates = new Vector3(i, j);
                var x = Instantiate(_cube, _startPoint.position + coordinates, Quaternion.identity);

                _trapSlabs[i, j] = x;
                x.SetActive(false);
            }
        }

        TrapSetter();
    }

    private void Update()
    {
        //if (!_isEventOn) return;
        
        if (_canTrap) StartCoroutine(TrapSetter());
        
    }

    private IEnumerator TrapSetter()
    {        
        _canTrap = false;
        StartCoroutine(TrapCoolDown(_trapCoolDown));

        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < _trapSlabs.GetLength(0); i++)
        {
            for (int j = 0; j < _trapSlabs.GetLength(1); j++)
            {
                var x = IsTrapSet(_trapProb);
                if (x)
                { 
                    Instantiate(_animatedSlab, _trapSlabs[i, j].transform.position, Quaternion.identity);
                    StartCoroutine(Damage(i, j));
                }
            }
        }
       
    }

    private IEnumerator Damage(int i, int j)
    {
        yield return new WaitForSeconds(1.5f);
        _trapSlabs[i, j].SetActive(true);
        yield return new WaitForSeconds(.5f);
        _trapSlabs[i, j].SetActive(false);
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
        
        for (int i = 0; i < _trapSlabs.GetLength(0); i++)
        {
            for (int j = 0; j < _trapSlabs.GetLength(1); j++)
            {
                if (_trapSlabs[i, j].activeSelf == true) _trapSlabs[i, j].SetActive(false);
            }
        }
        _canTrap = true;
    }
}
