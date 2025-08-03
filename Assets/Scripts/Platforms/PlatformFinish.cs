using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFinish : MonoBehaviour
{
    public ParticleSystem winParticles;
 
    public void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.name == "Player")
        {

           if (GameManager.gm.IsWin()) winParticles.Play();

        }

    }
}
