using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    public Slider lifeSlider;

    public TMP_Text lifeText;

    public Player myPlayer;

    string textToShow;

    float max;

    private bool isMinimapActive;

    public GameObject myMinimap;

    void Start()
    {
        max = myPlayer.GetComponent<Player>().SetMaxLife();

        lifeText.text = max + " / " + max;

        isMinimapActive = false;

    }
    
    public void UpdateLife(float perLife, float currentLife)
    {
        lifeSlider.value = perLife;

        textToShow = StaticData.lastHealth + " / " + max;
        
        lifeText.text = textToShow;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            MinimapManager();
        }

    }

    void MinimapManager()
    {
        if(isMinimapActive == false)
        {
            myMinimap.SetActive(true);
            isMinimapActive = true;
            print("Se activo el mapa");
        }

        else
        {
            myMinimap.SetActive(false);
            isMinimapActive = false;
            print("Se desactivo el mapa");
        }
    }
}
