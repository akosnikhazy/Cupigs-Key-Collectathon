using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTeleport : MonoBehaviour
{


    public bool justArrived = false;

    public ParticleSystem teleportBacons;
    
    private float _coolDown = 0;

    public GameObject playerCheck;

    public bool playerIsClose;

    private void Start()
    {
        justArrived = false;
    }

    private void Update()
    {
        
        playerIsClose = playerCheck.gameObject.GetComponent<TeleportPlayerCheck>().playerIsClose;
    }

    public void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name == "Player" && !justArrived)
        {

            GameObject otherTeleport = GameObject.Find((gameObject.name == "TeleportA") ? "TeleportB" : "TeleportA");

            otherTeleport.GetComponent<PlatformTeleport>().justArrived = true;

            other.transform.position = new Vector3(otherTeleport.transform.position.x, other.transform.position.y, otherTeleport.transform.position.z);

            teleportBacons.Play();

           
            otherTeleport.GetComponent<PlatformTeleport>().teleportBacons.Play();
            

        }



    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        { 
            justArrived = false;
        }
    }
}
