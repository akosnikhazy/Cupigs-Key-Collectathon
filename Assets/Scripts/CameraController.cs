using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("INI")]
    public Transform thePig; 
    public float camSpeed = 1.5f;


    private Color[] cameraColors = null;
    private float _percentage = 0;

    private void Update()
    {
       
        if(cameraColors == null || cameraColors != GameManager.gm.backgroundColorsNow) cameraColors = GameManager.gm.backgroundColorsNow;
        
        if (GameManager.gm.numberOfKeys > 0)
        { 
            _percentage = (GameManager.gm.keysCollected / (float)GameManager.gm.numberOfKeys);
        }

        _percentage     = Mathf.Clamp01(_percentage);

        int startIndex  = Mathf.FloorToInt((_percentage) * (cameraColors.Length - 1));
        int endIndex    = Mathf.CeilToInt((_percentage) * (cameraColors.Length - 1));

        float t = (_percentage - (startIndex / (float)(cameraColors.Length - 1))) * (cameraColors.Length - 1);

        Camera.main.backgroundColor = Color.Lerp(cameraColors[startIndex], cameraColors[endIndex], t);

       
    }

    void LateUpdate()
    {
        if (GameState.Reset == GameManager.gm.State)
        {
            transform.position = new Vector3(
                                             thePig.position.x,
                                             transform.position.y,
                                              thePig.position.z
                                            );
            

        }
     
            transform.position = Vector3.Lerp(
                                             transform.position,
                                             thePig.position,
                                             camSpeed * Time.deltaTime
                                            );

    }
}
