using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Player mainCharacter;
    
    void Update()
    {
        Vector3 playerPos = mainCharacter.transform.position;

        transform.position = new Vector3(playerPos.x, playerPos.y, transform.position.z);
    }
}
