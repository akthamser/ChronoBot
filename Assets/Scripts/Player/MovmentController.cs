using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.SceneManagement;

public class MovmentController : MonoBehaviour
{
    public static MovmentController Instance;

    private float _X_input, _Z_input;
    [HideInInspector]public Rigidbody _rigidbody;
    public float Speed;
    public float JumpHeight = 4;
    public bool MoveRelativeToTheCamera;
    private Camera CurrentCamera;
    private Vector3 FacingDirection;
    public ParticleSystem Deatheffect;
    [HideInInspector]public bool Freeze;
    [HideInInspector] public bool cancontroll;
    private bool dead;
    public bool Canjump;


    private SpiderProcuderalAnimation _procedrualAnimator;

    private void Awake()
    {
        CurrentCamera = Camera.main;
        dead = false;
        if(Instance == null)
            Instance = this;
        Freeze = false;
        cancontroll = true;
        _rigidbody = GetComponent<Rigidbody>();
        _procedrualAnimator = GetComponent<SpiderProcuderalAnimation>();
        Canjump = true;


       // transform.rotation = Quaternion.LookRotation(new Vector3( Camera.main.transform.rotation.x,0,Camera.main.transform.rotation.z));
    }
    void Update()
    {
        _X_input = Input.GetAxisRaw("Horizontal");
        _Z_input = Input.GetAxisRaw("Vertical");

        if(_rigidbody.velocity.x != 0 && _rigidbody.velocity.z != 0)
            FacingDirection = new Vector3(_rigidbody.velocity.x,0, _rigidbody.velocity.z);

       
    }
    private void FixedUpdate()
    {
        Vector3 Direction;
        if (MoveRelativeToTheCamera)
        Direction = (new Vector3(CurrentCamera.transform.forward.x, 0, CurrentCamera.transform.forward.z).normalized * _Z_input + new Vector3(CurrentCamera.transform.right.x, 0, CurrentCamera.transform.right.z).normalized * _X_input).normalized * Speed + _rigidbody.velocity.y * Vector3.up;
        else
        Direction = transform.forward * _Z_input + transform.right * _X_input;



        if (_procedrualAnimator.Grounded() && Input.GetKey(KeyCode.Space)&&Canjump)
            Direction.y = JumpHeight;

        if(cancontroll)
        	_rigidbody.velocity = Direction;

        Vector3 LookAt = new Vector3(Direction.x,0, Direction.z);
        if (LookAt.magnitude > 0.1f&&cancontroll)
        {
           transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.LookRotation(LookAt),0.1f);
        }
    }

    public void LockPlayerMovment()
    {
        Freeze = true;
        cancontroll = false;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.isKinematic = true;
    }
    public void UnLockPlayerMovment()
    {
        Freeze = false;
        cancontroll = true;
        _rigidbody.isKinematic = false;
    }
    public void removecontroll()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.isKinematic = true;
        cancontroll = false;

    }

    public void Die()
    {
        if (!dead)
        {
            AudioManager.Instance.PlaySound("Death");
            dead = true;
        Freeze = true;
        cancontroll = false;
        _rigidbody.velocity = Vector3.zero;
        GetComponent<Animator>().Play("Die");
        ActivateDeathEffect();
            Restart();
        
        }
    }
    public void Restart()
    {
        ScreenFade.instance.FadeIn(SceneManager.GetActiveScene().name);
        
    }
    public void Spoted()
    {
        AudioManager.Instance.PlaySound("Spoted");
        ScreenFade.instance.FadeIn(SceneManager.GetActiveScene().name);
    }



    public void ActivateDeathEffect()
    {
        Deatheffect.Play();
    }

    public void backcontroll()
    {
        _rigidbody.isKinematic = false;
        cancontroll = true;
    }

}
