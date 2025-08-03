using TMPro;
using UnityEngine;

public class SpawnLevelBtns : MonoBehaviour
{
    public GameObject lv1btn;         
    public int totalButtons = 100;    
    public float yOffset = -116.80001f;   
    
    void Start()
    {
        /*
        for (int i = 2; i <= totalButtons; i++)
        {
            GameObject newBtn = Instantiate(lv1btn, lv1btn.transform.parent);
            newBtn.name = $"lv{i}btn";

            // Set new position with offset
            Vector3 newPosition = lv1btn.transform.localPosition;
            newPosition.y += yOffset * (i - 1); // Offset based on the button index
            newBtn.transform.localPosition = newPosition;

            TextMeshProUGUI tmpText = newBtn.GetComponentInChildren<TextMeshProUGUI>();
            if (tmpText != null)
            {

                tmpText.GetComponentInChildren<TextMeshProUGUI>().SetText(TranslationManager.GetStringById("level"), i);
            }
            else
            {
                Debug.LogWarning($"TextMeshProUGUI not found in {newBtn.name}");
            }
        }
        */
    }

    
    void Update()
    {
        
    }
}
