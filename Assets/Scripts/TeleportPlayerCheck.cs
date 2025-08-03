using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayerCheck : MonoBehaviour
{
    public bool playerIsClose = false;


    public void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name == "Player")
        {

            playerIsClose = true;
        }
    
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {

            playerIsClose = false;
        }

    }
     
}
