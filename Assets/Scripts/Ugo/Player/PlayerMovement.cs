using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Attributes
    public Transform[] corridorPosition;
    public int currentCorridor = 1;

    //public float _gravityFactorJumpDown = 1.4f;
    //public float _gravityFactorJumpUp = 1f;
    //public float _gravityFactorCancelJump = 1.6f;

    //public float _groundCheckDistance = 0.4f;
    //public float _groundCheckOffset = 1.0f;
    public Vector3 raycastOffset;

    //private Rigidbody _rigidBody;
    private bool _isGrounded = false;
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
            //_velocity += _jumpForce * Vector3.up;
            //_startJump = true;
        }
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

        //transform.position = corridorPosition[currentCorridor].position;
            
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movements();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position + Vector3.right * 2, Vector3.up * 2);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position - Vector3.right * 2, Vector3.up * 2);
    }
}
