using UnityEngine;

public class LevitateObject : MonoBehaviour
{// simple as that
    public float floatSpeed = 1.0f; 
    public float floatDistance = 1.0f;
    public float rotationSpeed = 1.0f;

    private Vector3 _startPos;

    void Start()
    {
        _startPos = transform.position;
 
    }

    void Update()
    {
     

        float newY = _startPos.y + Mathf.PingPong(Time.time * floatSpeed, floatDistance);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        
    }
}
