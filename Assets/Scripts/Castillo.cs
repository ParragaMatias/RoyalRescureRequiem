using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Castillo : MonoBehaviour
{
    bool canEntry;

    bool canExit;

    public Player myPlayer;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.layer == 6)
        {
            canEntry = true;
            print("Podes entrar");
        }

        if(gameObject.CompareTag("SalidaCastillo") && col.gameObject.layer == 6)
        {
            canExit = true;
            print("podes salir");
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.layer == 6)
        {
            canEntry = false;
            print("Ya no podes entrar");
        }

        if(gameObject.CompareTag("SalidaCastillo") && col.gameObject.layer == 6)
        {
            canExit = false;
            print("no podes salir");
        }
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && canEntry == true)
        {
            CastleEntry();
        }
        
        if(Input.GetKeyDown(KeyCode.F) && canExit == true)
        {
            CastleExit();
        }
    }

    void CastleEntry()
    {
        SceneManager.LoadScene(3);
    }

    void CastleExit()
    {  
        StaticData.castleChecker = true;
        SceneManager.LoadScene(1);
    }
}
