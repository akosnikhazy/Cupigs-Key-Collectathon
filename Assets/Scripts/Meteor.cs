using UnityEngine;

public class Meteor : MonoBehaviour
{

    public GameObject bacon;
    
    private float _xRot;
    private float _yRot;
    private float _zRot;

  
    void Start()
    {
        _xRot = Random.Range(0.1f, 1f);
        _yRot = Random.Range(0.1f, 1f);
        _zRot = Random.Range(0.1f, 1f);
    }


    void Update()
    {
        transform.Rotate(_xRot, _yRot, _zRot);
        Destroy(gameObject, 3f);
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
