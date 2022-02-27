using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Serialized fields
    [SerializeField] private CharacterAnimationsEvents events = null;
    [SerializeField] private GameManager gameManager = null;
    [SerializeField] private Animator _animator;
    [SerializeField] private Collider body;
    [SerializeField] private Collider legs;
    [SerializeField] private Material dissolve;
    #endregion

    #region Attributes
    private int jumpHash = Animator.StringToHash("jump");
    private int slideHash = Animator.StringToHash("slide");

    public int currentCorridor = 1;
    
    public float _groundCheckDistance = 0.4f;
    public Vector3 raycastOffset;
    public Vector3 raycastRightOffset;
    public Vector3 raycastLeftOffset;
    public Vector3 _jumpForce;
    public bool _isGrounded = false;


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

            legs.enabled = false;
            GetComponent<Rigidbody>().AddForce(_jumpForce, ForceMode.Impulse);

            events.OnJump -= Jump;
        }
    }

    public void EndJump()
    {
            legs.enabled = true;

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
        RaycastHit hit;
        // Debug.Log("moveLeft");
        if (currentCorridor == 0 || Physics.Raycast(transform.position + raycastLeftOffset, Vector3.up, out hit, 2))
        {
            return;
        }

        currentCorridor -= 1;
        transform.position -= new Vector3(3f, 0f, 0f);
    }

    public void moveRight()
    {
        RaycastHit hit;

        if (currentCorridor == 2 || Physics.Raycast(transform.position + raycastRightOffset, Vector3.up, out hit, 2))
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

        if (Physics.Raycast(transform.position + raycastOffset, Vector3.down, out hit, _groundCheckDistance))
            _isGrounded = true;
        else
            _isGrounded = false;


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
        Gizmos.DrawRay(transform.position + raycastRightOffset, Vector3.up * 2);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position + raycastLeftOffset, Vector3.up * 2);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + raycastOffset, Vector3.down * _groundCheckDistance);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Death")
        {
            Death();
        }
    }
    #endregion
}
