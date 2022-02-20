using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Serialized fields
    [SerializeField] private CharacterAnimationsEvents events = null;
    #endregion
    
    #region Attributes
    public Transform[] corridorPosition;
    public Animator _animator;
    private int jumpHash = Animator.StringToHash("jump");
    int slideHash = Animator.StringToHash("slide");
    int deathHash = Animator.StringToHash("death");
    public int currentCorridor = 1;

    //public float _gravityFactorJumpDown = 1.4f;
    //public float _gravityFactorJumpUp = 1f;
    //public float _gravityFactorCancelJump = 1.6f;

    public float _groundCheckDistance = 0.4f;
    //public float _groundCheckOffset = 1.0f;
    public Vector3 raycastOffset;

    //private Rigidbody _rigidBody;
    private bool _isGrounded = false;
    #endregion

    #region API
    public bool IsGrounded()
    {
        return _isGrounded;
    }

    public void jump()
    {
        //Ajoutez de la jump force � la v�locit� du joueur
        if (_isGrounded)
        {
            _animator.SetTrigger(jumpHash);

            events.OnJump -= jump;
            //_velocity += _jumpForce * Vector3.up;
            //_startJump = true;
        }
    }

    public void slide()
    {
        _animator.SetTrigger(slideHash);

        events.OnSlide -= slide;
    }

    public void moveLeft()
    {
        RaycastHit hit;
        // Debug.Log("moveLeft");
        if (currentCorridor == 0 || Physics.Raycast(transform.position - (Vector3.right * 2), Vector3.up, out hit, 2))
        {
            return;
        }

        currentCorridor -= 1;
        transform.position -= new Vector3(2f, 0f, 0f);
    }

    public void moveRight()
    {
        RaycastHit hit;

        if (currentCorridor == 2 || Physics.Raycast(transform.position + (Vector3.right * 2), Vector3.up, out hit, 2))
        {
            return;
        }
        else
        {
            currentCorridor += 1;
            transform.position += new Vector3(2f, 0f, 0f);
        }
    }

    public void OnDeath()
    {
        _animator.SetTrigger(deathHash);
    }

    public void Movements()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            moveRight();
        }
            

        if (Input.GetKeyDown(KeyCode.Q))
        {
            moveLeft();
        }
        
        if (Input.GetKeyDown(KeyCode.Z))
        {
            jump();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            slide();
        }

        //transform.position = corridorPosition[currentCorridor].position;
            
    }
    #endregion

    #region Unity methods
    void Start()
    {
        
    }

    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position + raycastOffset, Vector3.down, out hit, _groundCheckDistance))
            _isGrounded = true;
        else
            _isGrounded = false;


        Movements();
    }

    public void Awake() 
    {
        events.OnJump += jump;
        events.OnSlide += slide;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position + Vector3.right * 2, Vector3.up * 2);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position - Vector3.right * 2, Vector3.up * 2);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + raycastOffset, Vector3.down * _groundCheckDistance);
    }
    #endregion
}
