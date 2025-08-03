using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathzone : MonoBehaviour
{


    public GameObject bacon;
    public Transform thePig;


    void LateUpdate()
    {


        transform.position = new Vector3(
                                         thePig.position.x,
                                         transform.position.y,
                                          thePig.position.z
                                        );

    }

    public void OnTriggerExit(Collider col)
    {
        
        if (col.gameObject.name == "Player")
        {
            if (GameManager.gm.badLuckCount < 30) {
               
                
                // this way bacon does not placed on the same position even if the player fails a lot at the same spot
                float randomY = (Random.Range(1, 2) % 2 == 0) ? transform.position.y + Random.Range(0.1f, 0.5f) : transform.position.y - Random.Range(0.1f, 0.5f);

                // Instantiate a new object at the overlap point
                Instantiate(bacon, new Vector3(col.transform.position.x,randomY, col.transform.position.z), Quaternion.identity)
                    .transform.rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
               
            }
           // restart the level (this might happen in game manager insted?)
           GameManager.gm.Ini();
           
        }
    }


}
