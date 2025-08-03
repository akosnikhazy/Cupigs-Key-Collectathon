using System.Collections;
using TMPro;
using UnityEngine;

public class Platform : MonoBehaviour
{


    private string _myNameIs = "";
    public int touchCount = 3;
    public TMP_Text counterNumber;
    private bool _endTrigger = false;
    private void Awake()
    {
        _myNameIs = gameObject.tag;

        if (_myNameIs == "Counter")
        {

        }
    }
 
    public void OnTriggerExit(Collider other)
    {

        if (other.gameObject.name == "Player" && _myNameIs != "Counter")
        {
            gameObject.SetActive(false);
        }

        if (_myNameIs == "Counter")
        {

            touchCount--;

            if (touchCount == 0)
            {
                gameObject.SetActive(false);

            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {

        if (_myNameIs == "Finish")
        {
            _endTrigger = true;

        }
    }

    void LateUpdate()
    {
        if (_endTrigger)
        {
            GameManager.gm.IsWin();
        }
    }
}
