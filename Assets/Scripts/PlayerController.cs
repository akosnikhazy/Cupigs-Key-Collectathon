using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

   

    [Header("Movement Settings")]
    public float movementSpeed = 5f;
    public float rotationSpeed = 5f;
    public float dashSpeed = 10f;
    public float dashDuration = 0.4f;

    [Header("Audio Clips")]
    public AudioClip collectSFX;
    public AudioClip winSFX;
    public AudioClip loseSFX;
    public AudioClip dashSFX;

    [Header("Effects")]
    public ParticleSystem dashParticles;
    public float dashZoomFactor = 0.05f;

    [Header("Attachments")]
    public GameObject pipe;
    public GameObject backpack;
    public GameObject spaceBubble;

    private float _x;
    private float _z;

    private bool _isDashing = false;
    private float _dashTimer = 0f;

    private Animator _anim;
    private static AudioSource _levelSounds;

    private float _cameraSize;

    private Rigidbody _rb;

    private void Awake()
    {
        
        _levelSounds = GetComponent<AudioSource>();
    }
    private void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _cameraSize = Camera.main.orthographicSize;
    }

    private void Update()
    {
        var gamepad = Gamepad.current;

        
        _z = Input.GetAxisRaw("Horizontal");
        _x = Input.GetAxisRaw("Vertical");

        var gamepadDash = false;

        if (gamepad != null)
        {
            if (gamepad.xButton.isPressed || gamepad.aButton.isPressed) gamepadDash = true;
        }

        if ((Input.GetKeyDown(KeyCode.Space) || gamepadDash) && !_isDashing)
        {
            _isDashing = !_isDashing;
            _dashTimer = dashDuration;

            // effects
            AudioManager.PlaySFX(_levelSounds, dashSFX);
            dashParticles.Play();
        }

       

    }

    private void FixedUpdate()
    {
        if (GameManager.gm.State != GameState.GamePlay) return;
        
        if (_isDashing)
        {
            _dashTimer -= Time.deltaTime;
            
            _rb.constraints = RigidbodyConstraints.FreezePositionY;
            
            if (_dashTimer <= 0)
            {
                // set the camera back to normal to be sure.
                Camera.main.orthographicSize = _cameraSize;
               _isDashing = !_isDashing;
                _rb.constraints = RigidbodyConstraints.None;


            } 
            else
            {
                // we zoom in half the dash and zoom back out the other half
                if (_dashTimer > dashDuration / 2)
                {
                    Camera.main.orthographicSize = Camera.main.orthographicSize - dashZoomFactor;
                }
                else
                {
                    Camera.main.orthographicSize = Camera.main.orthographicSize + dashZoomFactor;
                }
                
            }

        }

        if (_x != 0 || _z != 0)
        {
            
            Vector3 moveDirection = new Vector3(_x*-1, 0, _z).normalized; 
            Quaternion targetRotation;
          
            if (Mathf.Abs(moveDirection.x) > Mathf.Abs(moveDirection.z))
            {
                // Horizontal movement (left or right)
                targetRotation = Quaternion.LookRotation(new Vector3(moveDirection.x , 0, 0));
            }
            else
            {
                // Vertical movement (up or down)
                targetRotation = Quaternion.LookRotation(new Vector3(0, 0, moveDirection.z));
            }

            GameManager.gm.ResetPooTimer();
            
            gameObject.transform.rotation = targetRotation;

            _anim.SetFloat("Speed", 2);

            // are we dashing?
            float currentSpeed = _isDashing ? dashSpeed : movementSpeed;

           
            // move accordingly
            Vector3 movement = moveDirection * currentSpeed * Time.deltaTime;
            gameObject.transform.Translate(movement, Space.World);

        }
        else
        {
            _anim.SetFloat("Speed", 0);
        }
       


    }

    public void OnTriggerEnter(Collider other)
    {
        // the player's only collision concern are sounds
        // as anything that gives out sound is destroyed
        // before it can give out sound, it is safer to
        // make the player do the job.
        AudioClip toPlay = null;

        switch (other.gameObject.name)
        {
            case "Gem":
                toPlay = collectSFX;
                break;
            case "Treasure Chest":
                if (GameManager.gm.IsWin())
                {
                    toPlay = winSFX;
                    GameManager.gm.SaveTime();
                    break;
                }
                toPlay = loseSFX;
                break;
          
        }

        if (toPlay) AudioManager.PlaySFX(_levelSounds, toPlay);

    }

}
