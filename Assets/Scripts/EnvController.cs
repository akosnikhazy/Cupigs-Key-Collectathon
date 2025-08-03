using UnityEngine;

public class EnvController : MonoBehaviour
{// this is the same as the CameraController tweaked so it perfectly follows the pigs movement with the camera

    public Transform thePig; 
    public float followSpeed = 1.5f; 

    void LateUpdate()
    {


        transform.position = Vector3.Lerp(
                                            transform.position,
                                            new Vector3(
                                                        thePig.position.x, 
                                                        transform.position.y, 
                                                        thePig.position.z
                                                        ),
                                            followSpeed * Time.deltaTime
                                           );

    }
}
