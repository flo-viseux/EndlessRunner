using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Serialized fields
    [SerializeField] private CharacterAnimationsEvents events = null;
    [SerializeField] private GameManager gameManager = null;
    [SerializeField] private CollisionBody body = null;
    [SerializeField] private CollisionLegs legs = null;
    [SerializeField] private Collider C_body;
    [SerializeField] private Collider C_legs;
    [SerializeField] private Material dissolve;
    [SerializeField] private LayerMask[] rampes;
    #endregion

    #region Attributes
    public Animator _animator;
    private int jumpHash = Animator.StringToHash("jump");
    private int slideHash = Animator.StringToHash("slide");
    public int playHash = Animator.StringToHash("play");
    private int groundHash = Animator.StringToHash("ground");

    public int currentCorridor = 1;
    
    public float _groundCheckDistance = 0.4f;
    public Vector3 raycastOffset;
    public Vector3 halfExtentGround;
    public Vector3 halfExtentsObstacles;
    public Vector3 raycastRightOffset;
    public Vector3 raycastLeftOffset;
    public Vector3 _jumpForce;
    public bool _isGrounded = false;

    public bool alive;
    public bool hasStarting;
    public float lerpTimeDeath;
    public float lerpTimeStart;
    #endregion

    #region API

    public void Jump()
    {
        //Ajoutez de la jump force � la v�locit� du joueur
        if (_isGrounded)
        {
            legs._isJumping = true;
            _animator.SetTrigger(jumpHash);
            C_legs.enabled = false;
            GetComponent<Rigidbody>().AddForce(_jumpForce, ForceMode.Impulse);

            events.OnJump -= Jump;
        }
    }

    public void EndJump()
    {
        C_legs.enabled = true;
        legs._isJumping = false;
        events.OnEndJump -= EndJump;
    }

    public void Slide()
    {
        body._isSliding = true;
        _animator.SetTrigger(slideHash);
        
        if (!_isGrounded)
        {
            GetComponent<Rigidbody>().AddForce( - _jumpForce, ForceMode.Impulse);
        }

        C_body.enabled = false;
        events.OnSlide -= Slide;
    }

    public void EndSlide()
    {
        body._isSliding = false;
        C_body.enabled = true;

        events.OnEndSlide -= EndSlide;
    }

    public void moveLeft()
    {
        bool hasChange = false;

        if (currentCorridor == 0)
        {
            return;
        }
        else
        {
            currentCorridor -= 1;
            transform.position -= new Vector3(3f, 0f, 0f);
        }
        
    }

    public void moveRight()
    {
        if (currentCorridor == 2)
        {
            return;
        }
        else
        {
            currentCorridor += 1;
            transform.position += new Vector3(3f, 0f, 0f);

        }
    }

    public void Death()
    {
        dissolve.SetFloat("Vector1_01e307ea533142d29e8670cdc9eb4872", Mathf.Lerp(0f, 1f, 1 * Time.deltaTime));
        alive = false;
        gameManager.EndGame();
        events.OnDeath -= Death;
    }

    public void Movements()
    {
        RaycastHit hitRight;
        bool right = (Physics.BoxCast(transform.position + raycastRightOffset, halfExtentsObstacles, transform.right, out hitRight, Quaternion.identity, _groundCheckDistance));

        RaycastHit hitLeft;
        bool left = (Physics.BoxCast(transform.position + raycastLeftOffset, halfExtentsObstacles, -transform.right, out hitLeft, Quaternion.identity, _groundCheckDistance));

        if (Input.GetKeyDown(KeyCode.D) && !right)
        {
            moveRight();
        }
            
        if (Input.GetKeyDown(KeyCode.Q) && !left)
        {
            moveLeft();
        }
        
        if (Input.GetKeyDown(KeyCode.Z) && legs._isJumping == false)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Slide();
        }

        //transform.position = corridorPosition[currentCorridor].position;
            
    }
    #endregion

    #region Unity methods

    void Update()
    {
        RaycastHit hit;

        if ((Physics.BoxCast(transform.position + raycastOffset, halfExtentGround, - transform.up, out hit, Quaternion.identity,_groundCheckDistance, rampes[0])) 
            || (Physics.BoxCast(transform.position + raycastOffset, halfExtentGround, -transform.up, out hit, Quaternion.identity, _groundCheckDistance, rampes[1]))
            || (Physics.BoxCast(transform.position + raycastOffset, halfExtentGround, -transform.up, out hit, Quaternion.identity, _groundCheckDistance, rampes[2])))
        {
            _isGrounded = true;
            _animator.SetBool(groundHash, true);
            C_legs.enabled = true;
        }   
        else
        {
            _animator.SetBool(groundHash, false);
            _isGrounded = false;
        }


        if(alive)
            Movements();

        if (dissolve.GetFloat("Vector1_01e307ea533142d29e8670cdc9eb4872") > 0 && hasStarting == false)
        {
            dissolve.SetFloat("Vector1_01e307ea533142d29e8670cdc9eb4872", Mathf.Lerp(dissolve.GetFloat("Vector1_01e307ea533142d29e8670cdc9eb4872"), 0f, lerpTimeStart * Time.deltaTime));
        }      

        if(hasStarting == false && dissolve.GetFloat("Vector1_01e307ea533142d29e8670cdc9eb4872") <= 0.05f)
        {
            hasStarting = true;
            alive = true;
        }
            

        if (hasStarting == true && !alive && dissolve.GetFloat("Vector1_01e307ea533142d29e8670cdc9eb4872") < 1)
            dissolve.SetFloat("Vector1_01e307ea533142d29e8670cdc9eb4872", Mathf.Lerp(dissolve.GetFloat("Vector1_01e307ea533142d29e8670cdc9eb4872"), 1f, lerpTimeDeath * Time.deltaTime));
    }

    public void Awake() 
    {
        hasStarting = false;
        alive = false;
        dissolve.SetFloat("Vector1_01e307ea533142d29e8670cdc9eb4872", 1f);

        events.OnJump += Jump;
        events.OnSlide += Slide;
        currentCorridor = 1;
        events.OnEndJump += EndJump;
        events.OnEndSlide += EndSlide;
        events.OnDeath += Death;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube((transform.position + raycastRightOffset + transform.right) * _groundCheckDistance, halfExtentsObstacles);

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube((transform.position + raycastLeftOffset - transform.right) * _groundCheckDistance, halfExtentsObstacles);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((transform.position + raycastOffset - transform.up) * _groundCheckDistance , halfExtentGround);
    }
    #endregion
}
