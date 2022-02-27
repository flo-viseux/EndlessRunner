using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Serialized fields
    [SerializeField] private CharacterAnimationsEvents events = null;
    [SerializeField] private GameManager gameManager = null;
    [SerializeField] private Collider body;
    [SerializeField] private Collider legs;
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
    public bool _isJumping = false;


    public bool alive;
    public float lerpTime;
    #endregion

    #region API

    public void Jump()
    {
        //Ajoutez de la jump force � la v�locit� du joueur
        if (_isGrounded)
        {
            _animator.SetTrigger(jumpHash);

            _isJumping = true;
            legs.enabled = false;
            GetComponent<Rigidbody>().AddForce(_jumpForce, ForceMode.Impulse);

            events.OnJump -= Jump;
        }
    }

    public void EndJump()
    {
        legs.enabled = true;
        _isJumping = false;
        events.OnEndJump -= EndJump;
    }

    public void Slide()
    {
        _animator.SetTrigger(slideHash);

        if (!_isGrounded)
        {
            GetComponent<Rigidbody>().AddForce( - _jumpForce, ForceMode.Impulse);
        }

        body.enabled = false;
        events.OnSlide -= Slide;
    }

    public void EndSlide()
    {
        body.enabled = true;

        events.OnEndSlide -= EndSlide;
    }

    public void moveLeft()
    {
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
    }

    public void Movements()
    {
        RaycastHit hitRight;
        bool right = (Physics.BoxCast(transform.position + raycastRightOffset, halfExtentsObstacles, transform.right, out hitRight, Quaternion.identity, _groundCheckDistance));
        if(right)
            Debug.Log(hitRight.collider.name);

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
        
        if (Input.GetKeyDown(KeyCode.Z))
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
            legs.enabled = true;
        }   
        else
        {
            _animator.SetBool(groundHash, false);
            _isGrounded = false;
        }


        if(alive)
            Movements();

        if(alive && dissolve.GetFloat("Vector1_01e307ea533142d29e8670cdc9eb4872") > 0)
            dissolve.SetFloat("Vector1_01e307ea533142d29e8670cdc9eb4872", Mathf.Lerp(dissolve.GetFloat("Vector1_01e307ea533142d29e8670cdc9eb4872"), 0f, lerpTime * Time.deltaTime));

        if(!alive && dissolve.GetFloat("Vector1_01e307ea533142d29e8670cdc9eb4872") < 1)
            dissolve.SetFloat("Vector1_01e307ea533142d29e8670cdc9eb4872", Mathf.Lerp(dissolve.GetFloat("Vector1_01e307ea533142d29e8670cdc9eb4872"), 1f, lerpTime * Time.deltaTime));
    }

    public void Awake() 
    {
        alive = true;
        dissolve.SetFloat("Vector1_01e307ea533142d29e8670cdc9eb4872", 1f);

        events.OnJump += Jump;
        events.OnSlide += Slide;
        currentCorridor = 1;
        events.OnEndJump += EndJump;
        events.OnEndSlide += EndSlide;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    // private void Start() {
    //     //dissolve.SetFloat("Vector1_01e307ea533142d29e8670cdc9eb4872", Mathf.Lerp(1f, 0f, 20 * Time.deltaTime));
    // }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube((transform.position + raycastRightOffset + transform.right) * _groundCheckDistance, halfExtentsObstacles);

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube((transform.position + raycastLeftOffset - transform.right) * _groundCheckDistance, halfExtentsObstacles);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((transform.position + raycastOffset - transform.up) * _groundCheckDistance , halfExtentGround);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Death" && other.gameObject.layer == 10 && !_isJumping)
        {
            Death();
        }
        if(other.gameObject.tag == "Death" && other.gameObject.layer != 10)
        {
            Death();
        }

        
    }
    #endregion
}
