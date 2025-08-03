using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformShark : MonoBehaviour
{
   
    public Transform spawnerA;
    public Transform spawnerB;

   
    public GameObject sharkPrefab;
    private void Awake()
    {
        sharkPrefab.SetActive(false);
    }

    private void SpawnShark()
    {
     
        string chosenSpawner = Random.value < 0.5f ? "A" : "B";
        GameObject thisShark;
        if (chosenSpawner == "A")
        {

            thisShark = Instantiate(sharkPrefab, spawnerA.position, spawnerA.rotation);
        } else
        {
            thisShark = Instantiate(sharkPrefab, spawnerB.position, spawnerB.rotation);
        }

       
        thisShark.transform.localScale = Vector3.one * 0.3f; 
        thisShark.SetActive(true);
    }

    // Detect collision with the player
    public void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.name == "Player")
        {
           
            SpawnShark();
        }
    }
}
