using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformKillOthersUp : MonoBehaviour
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
            Destroy(GameObject.Find("p_" + coordinatesFromName[1] + "_" + (Int32.Parse(coordinatesFromName[2]) - 1)));
            Destroy(GameObject.Find("p_" + coordinatesFromName[1] + "_" + (Int32.Parse(coordinatesFromName[2]) + 1)));
            Destroy(gameObject);
            */

            GameObject.Find("p_" + coordinatesFromName[1] + "_" + (Int32.Parse(coordinatesFromName[2]) - 1)).SetActive(false);
            GameObject.Find("p_" + coordinatesFromName[1] + "_" + (Int32.Parse(coordinatesFromName[2]) + 1)).SetActive(false);
            gameObject.SetActive(false);

        }



    }


}
