using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveInput : MonoBehaviour
{
    #region Attributes
    //private InputAction _moveAction = null;
    //private InputAction _jumpAction = null;
    private CharacterController _characterController = null;
    
    [Header("Player")]
    public float _gravityFactorJumpUp = 1;
    //public PlayerInput _playerInput = null;

    public Animator _animator;
    public float actionDuration = 1f;
    int jumpHash = Animator.StringToHash("jump");
    int slideHash = Animator.StringToHash("slide");
    int deathHash = Animator.StringToHash("death");
    private float actionTimeRemaining = 0f;
    #endregion

    #region API
    private void Start() 
    {
        _characterController = GetComponent<CharacterController>();
        //_moveAction = _playerInput.actions.FindAction("Move", true);
        //_jumpAction = _playerInput.actions.FindAction("Jump", true);

        _animator.SetFloat("speed", 1.0f);
    }

    public void OnDeath()
    {
        _animator.SetTrigger(deathHash);
    }
    
    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.CompareTag("Obstacles"))
        {
            OnDeath();
        }
    }
    
    public void OnJump()
    {
        if (actionTimeRemaining <= 0 && _characterController.isGrounded())
        {
            // Debug.Log("startJump");
            _characterController.jump();
            _animator.SetTrigger(jumpHash);
            actionTimeRemaining = actionDuration;

        }
    }

    public void OnSlide()
    {
        if (actionTimeRemaining <= 0)
        {
            _animator.SetTrigger(slideHash);
            actionTimeRemaining = actionDuration;
        }
    }

    public void OnLeft()
    {
        // Debug.Log("Left");
        if(actionTimeRemaining <= 0 && _characterController.moveLeft())
        {
            _animator.SetTrigger("left");
            actionTimeRemaining = actionDuration;
        }
    }

    public void OnRight()
    {
        if(actionTimeRemaining <= 0 && _characterController.moveRight())
        {
            _animator.SetTrigger("right");
            actionTimeRemaining = actionDuration;
        }
    }
    #endregion

    #region Unity methods
    private void Update() 
    {
        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);

        if (actionTimeRemaining > 0)
        {
            actionTimeRemaining -= Time.deltaTime;
        }
    }

    private void FixedUpdate() 
    {
        //Récupérer le player input
        //Vector2 inputMove = _moveAction.ReadValue<Vector2>();
        //float inputJump = _jumpAction.ReadValue<float>();

        //if (inputJump > 0.5f)
        //{
            //_characterController.setCustomGravityFactor(_gravityFactorJumpUp);
        //}
    }
    #endregion
}
