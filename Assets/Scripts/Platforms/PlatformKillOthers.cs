using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformKillOthers : MonoBehaviour
{


    public void OnTriggerExit(Collider other)
    {

        if (other.gameObject.name == "Player")
        {

            /* 
             * 
             *  based on name we know the coordinates of the platfroms next to it
             *  Destroy doesn't care if it doesn't exist, so it is safe to pass
             *  it like this
             * 
             */

            string[] coordinatesFromName = gameObject.name.Split('_');

            /*
            Destroy(GameObject.Find("p_" + (Int32.Parse(coordinatesFromName[1]) - 1) + "_" + coordinatesFromName[2]));
            Destroy(GameObject.Find("p_" + (Int32.Parse(coordinatesFromName[1]) + 1) + "_" + coordinatesFromName[2]));
            Destroy(gameObject);
            */
            
            
            GameObject.Find("p_" + (Int32.Parse(coordinatesFromName[1]) - 1) + "_" + coordinatesFromName[2]).SetActive(false);
            GameObject.Find("p_" + (Int32.Parse(coordinatesFromName[1]) + 1) + "_" + coordinatesFromName[2]).SetActive(false);
            gameObject.SetActive(false);


        }

        

    }


}
