using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HouseManager : MonoBehaviour
{
    private bool canExit;
    
    void OnTriggerEnter2D(Collider2D col)
    {
        
        if(col.gameObject.layer == 6)
        {
            canExit = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.layer == 6)
        {
            canExit = false;
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && canExit == true)
        {
            SceneManager.LoadScene(1);
        }
    }
}
