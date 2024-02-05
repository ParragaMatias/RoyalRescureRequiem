using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnEvent : MonoBehaviour
{
    [SerializeField] private float _despawnTime;
    private SpriteRenderer _sprite;
    private Color _color;
    private bool _canDesactivate;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _color = new Color(1, 1, 1, 1);
    }

    public void FadeStartEvent()
    {
        StartCoroutine(Fade());
    }

    private void Update()
    {
        if (_canDesactivate) StartCoroutine(Desactivate());
    }

    private IEnumerator Desactivate()
    {
        yield return new WaitForSeconds(0.3f);
        gameObject.SetActive(false);
    }
    private IEnumerator Fade()
    {
        float alpha = _sprite.color.a;
        for (float i = 0; i < _despawnTime; i += Time.deltaTime)
        {
            _color = new Color(1, 1, 1, Mathf.Lerp(_sprite.color.a, 0, i));
            _sprite.color = _color;
            _canDesactivate = true;
            yield return null;
        }
    }
}
