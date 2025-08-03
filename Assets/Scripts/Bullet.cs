using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public bool on = false;

    private float _speed = 2f;

    public Vector3 home;
   
    void Update()
    {
        
        if (on) transform.position -= new Vector3( Time.deltaTime * _speed, 0,0) ;
    }

    public void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name == "Player")
        {

            gameObject.transform.position = home;
            GameManager.gm.Ini();


        }



    }
}
