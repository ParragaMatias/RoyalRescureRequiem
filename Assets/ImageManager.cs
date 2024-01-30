using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageManager : MonoBehaviour
{
    [SerializeField] public GameObject[] _images;
    [SerializeField] public int _imageIndex = 0, _playerLayer;

    public bool _showImage;

    private void Update()
    {
        if (_showImage)
        {
            _images[_imageIndex].SetActive(true);
        }

        else if(!_showImage)
        {
            _images[_imageIndex].SetActive(false);
        }
    }
}
