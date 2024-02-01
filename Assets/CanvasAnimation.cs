using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasAnimation : MonoBehaviour
{
    [SerializeField] private Image m_Image;

    [SerializeField] private Sprite[] m_SpriteArray;
    [SerializeField] private float m_Speed;

    private int m_IndexSprite;
    private bool _isDone;
    GameObject _image;
    Coroutine m_CorotineAnim;

    public void Start()
    {
        //Func_PlayUIAnim(_image);
    }

    public void Func_PlayUIAnim(GameObject image)
    {
        _image = image;
        _isDone = false;
        _image.SetActive(true);
        StartCoroutine(Func_PlayAnimUI());
    }

    public void Func_StopUIAnim()
    {
        _isDone = true;
        StopCoroutine(Func_PlayAnimUI());
    }

    private IEnumerator Func_PlayAnimUI()
    {
        yield return new WaitForSecondsRealtime(m_Speed);
        if(m_IndexSprite >= m_SpriteArray.Length)
        {
            m_IndexSprite = 0;
        }
        m_Image.sprite = m_SpriteArray[m_IndexSprite];
        m_IndexSprite += 1;
        if(_isDone == false && m_Image != null)       
            m_CorotineAnim = StartCoroutine(Func_PlayAnimUI());
        
    }
}
