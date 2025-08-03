using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkBrain : MonoBehaviour
{

    public GameObject bacon;
    public float launchSpeed = 10f;


    private float time = 0;
    private Vector3 direction;
    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {

            direction = (player.transform.position - gameObject.transform.position).normalized;
          
        }
    }
    private void Update()
    {
     
        transform.position += Time.deltaTime * launchSpeed * direction;
        time += Time.deltaTime;

        if (time > 10)
        {

            Destroy(gameObject);
        
        }
    }

    public void OnTriggerExit(Collider col)
    {

        if (col.gameObject.name == "Player")
        {
            if (GameManager.gm.badLuckCount < 30)
            {


                // this way bacon does not placed on the same position even if the player fails a lot at the same spot
                float randomY = (Random.Range(1, 2) % 2 == 0) ? transform.position.y + Random.Range(0.1f, 0.5f) : transform.position.y - Random.Range(0.1f, 0.5f);

                // Instantiate a new object at the overlap point
                Instantiate(bacon, new Vector3(col.transform.position.x, randomY, col.transform.position.z), Quaternion.identity)
                    .transform.rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

            }
            // restart the level
            GameManager.gm.Ini();

        }

    }
    
}
