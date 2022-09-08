using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{

    ControlPlayer _Control;
    Animator _Animator;
    [SerializeField]
    [Min(0.1f)]
    float _Speed = 1f;
    [SerializeField]
    float _JumpHeight=5f;
    [SerializeField]
    bool _IsGround = true;
    Rigidbody _RigidbodyPlayer;
    Transform _MainCamera;
    [SerializeField]
    int _Helth = 3;

    void Awake()
    {
        _Control = new ControlPlayer();
    }
    private void Start()
    {
        _MainCamera = Camera.main.transform;
        _RigidbodyPlayer = GetComponent<Rigidbody>();
        _Animator = GetComponent<Animator>();
        _Animator.SetFloat("Speed", _Speed);
    }
    void OnEnable()
    {
        _Control.Player.Enable();
        _Control.Player.Jump.performed += Jump_performed;
    }

    private void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (_IsGround)
        {
            _Animator.SetBool("Jump", true);
            _RigidbodyPlayer.velocity = Vector3.up * _JumpHeight;
        }
    }
    private void Update()
    {
        if (transform.position.y <= -1f) SetDamage(int.MaxValue);
        if(_Helth<=0)
        {
            Debug.Log("You Lose");
            UnityEditor.EditorApplication.isPaused = true;
        }
    }

    public void SetDamage(int damage)
    {
        _Speed -= 0.5f;
        _Helth -= damage;
        GameManager_sc.instance.TextHelth.text = _Helth.ToString();
        Debug.Log("Hit");
    }

    private void FixedUpdate()
    {
        Move(); 
        if (_IsGround)
        _Animator.SetBool("Jump", false);
    }

  
    private void Move()
    {
        if (_Speed < 1f)
            _Speed = 1f;
        _Speed += Time.deltaTime/10;
        var input = _Control.Player.Move.ReadValue<float>();
        if (input != 0f)
        {
            if(input<0)
                transform.rotation = Quaternion.Euler(0, -45, 0);
            else if(input>0)
                transform.rotation = Quaternion.Euler(0, 45, 0);

        }else
            transform.rotation = Quaternion.Euler(0, 0, 0);
        
        transform.position += new Vector3(input * Time.deltaTime * _Speed, 0, Time.deltaTime * _Speed);
        _MainCamera.position = new Vector3(0, 5, transform.position.z-5) ;
    }

    private void OnDisable()
    {
        _Control.Player.Disable();
        _Control.Player.Jump.performed -= Jump_performed;
    }

    private void OnCollisionStay(Collision collision)
    {
        _IsGround = true;
        _Animator.SetBool("IsGrounded", _IsGround);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Lava")
        {
            SetDamage(1);

        }
        else if
               (other.gameObject.tag == "Rock")
        {
            SetDamage(1);

        }
        else if (other.gameObject.tag == "Boost")
        {
            _Speed += 1f;
        }
        else if (other.gameObject.tag == "Stop")
        {
            _Speed -= 1f;
        }
        else if (other.gameObject.tag == "Axe")
        {
            SetDamage(1);
        }
    }    

    private void OnCollisionExit(Collision collision)
    {
        _IsGround = false;
        _Animator.SetBool("IsGrounded", _IsGround);
    }
}
