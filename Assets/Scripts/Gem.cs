using UnityEngine;

public class Gem : MonoBehaviour
{
   
    public void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name == "Player")
        {
            GameManager.gm.keysCollected++;
            SteamManager.UnlockAchievement("FIRST_KEY");
            gameObject.SetActive(false);
        }


    }
   
}

