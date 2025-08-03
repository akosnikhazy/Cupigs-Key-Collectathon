using TMPro;
using UnityEngine;

public class PlatformCounter : MonoBehaviour
{
    public int touchCount = 3;
    public TMP_Text counterNumber;

    public void OnTriggerExit(Collider other)
    {

        touchCount--;
        counterNumber.SetText(touchCount.ToString());
        
        if (touchCount == 0)
        {
            gameObject.SetActive(false);
           
        }
        
    }
}
