using UnityEngine;
using UnityEngine.EventSystems;

public class MainButton : MonoBehaviour
{

    [Header("SFX")]
    public AudioClip buttonPress;
    public AudioClip buttonHover;

    private AudioSource _buttonSounds;

    // Start is called before the first frame update
    private void Awake()
    {
        _buttonSounds = GetComponent<AudioSource>();
    }
    void Start()
    {
        EventTrigger eventTrigger = GetComponent<EventTrigger>();

        if (eventTrigger == null) eventTrigger = gameObject.AddComponent<EventTrigger>();
        
        EventTrigger.Entry pointerDownEvent = new EventTrigger.Entry();
        EventTrigger.Entry pointerEnter = new EventTrigger.Entry();
        
        pointerDownEvent.eventID = EventTriggerType.PointerDown;
        pointerEnter.eventID = EventTriggerType.PointerEnter;

        pointerDownEvent.callback.AddListener((data) => { AudioManager.PlaySFX(_buttonSounds,buttonPress); });
        eventTrigger.triggers.Add(pointerDownEvent);

        pointerEnter.callback.AddListener((data) => { AudioManager.PlaySFX(_buttonSounds,buttonHover); });
        eventTrigger.triggers.Add(pointerEnter);
    }


}
