using UnityEngine;

public class PlatformNormal : MonoBehaviour
{


    public void OnTriggerExit(Collider other)
    {

        
        if (other.gameObject.name == "Player")
        {
           
            gameObject.SetActive(false);
        }

        /* seriously 
        gameObject.SetActive(!(other.gameObject.name == "Player"));
        */
    }
}
