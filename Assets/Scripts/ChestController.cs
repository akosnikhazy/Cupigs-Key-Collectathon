using TMPro;
using UnityEngine;

public class ChestController : MonoBehaviour
{

    public GameObject[] lid;
    public TMP_Text counterNumber;
   
    void Awake()
    {
        lid[1].SetActive(false);
        counterNumber.SetText((GameManager.gm.numberOfKeys-1).ToString());
    }

  
    void Update()
    {
        counterNumber.SetText((GameManager.gm.numberOfKeys-1-GameManager.gm.keysCollected).ToString());
    }

    public void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.name == "Player")
        {
            if (GameManager.gm.numberOfKeys - 1 - GameManager.gm.keysCollected == 0)
            {
                lid[0].SetActive(false);
                lid[1].SetActive(true);
            }

        }


    }
}
