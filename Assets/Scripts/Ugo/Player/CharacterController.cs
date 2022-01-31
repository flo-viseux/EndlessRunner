using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    #region Attributes
    public Transform[] corridorPosition;
    private const float EPSILON = 0.1f;
    private Vector3 _velocity = Vector3.zero;
    [Range(1, 8)] public float _jumpForce;

    public float _gravityFactorJumpDown = 1.4f;
    public float _gravityFactorJumpUp = 1f;
    public float _gravityFactorCancelJump = 1.6f;
    private bool _isGrounded = false;

    public float _groundCheckDistance =  0.4f;
    public float _groundCheckOffset = 1.0f;

    private Rigidbody _rigidBody;
    private bool _startJump = false;
    private int currentCorridor = 1;
    #endregion

    #region API
    public bool isGrounded()
    {
        return _isGrounded;
    }

    public void jump()
    {
        //Ajoutez de la jump force à la vélocité du joueur
        if (_isGrounded)
        {
            _velocity += _jumpForce * Vector3.up;
            _startJump = true;
        }
    }

    public bool moveLeft()
    {
        Debug.Log("moveLeft");
        if(currentCorridor == 0)
        {
            return false;
        }

        currentCorridor -= 1;

        return true;
    }

    public bool moveRight()
    {
        if(currentCorridor == 2)
        {
            return false;
        }

        currentCorridor += 1;
        
        return true;
    }
    #endregion

    #region Private
    private void Awake() 
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() 
    {
        Vector3 groundCorrection = Vector3.zero;
        _isGrounded = false;

        if (!_startJump && _velocity.y < EPSILON)
        {
            RaycastHit hit;
            if (Physics.Raycast(_rigidBody.position + Vector3.up * _groundCheckOffset, Vector3.down, out hit, _groundCheckDistance + _groundCheckOffset, Physics.AllLayers, QueryTriggerInteraction.Ignore))
            {
                _isGrounded = true;
                groundCorrection = Vector3.down * (hit.distance - _groundCheckOffset);
            }
        }

        //Changer le facteur gravité pour un saut meilleur
        float gravityFactor = 1.0f;

        if(_velocity.y < 0)
        {
            gravityFactor = _gravityFactorJumpDown;
        }
        else
        {
            gravityFactor = _gravityFactorCancelJump;
        }

        //Appliquer la gravité au joueur
        if (!_isGrounded)
        {
            _velocity += Vector3.down * 9.81f * gravityFactor * Time.fixedDeltaTime;
        }
        else if (_velocity.y < EPSILON)
        {
            _velocity.y = 0;
        }

        float displacementHorizontal = Mathf.MoveTowards(0, corridorPosition[currentCorridor].position.x - _rigidBody.position.x, 0.5f);
        _velocity.x = displacementHorizontal / Time.deltaTime;

        _startJump = false;

        //Bouger le joueur pour une meilleure physique
        _rigidBody.velocity = Vector3.zero;
        _rigidBody.MovePosition(_rigidBody.position + _velocity * Time.fixedDeltaTime + groundCorrection);
    }
    #endregion
}
