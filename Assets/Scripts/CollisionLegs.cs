using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionLegs : MonoBehaviour
{
    public CharacterAnimationsEvents events;
    public bool _isJumping = false;
    // Start is called before the first frame update
    void Awake()
    {
        _isJumping = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Death" && other.gameObject.layer == 10 && !_isJumping)
        {
            events.Death();
        }
        else if (other.gameObject.tag == "Death" && other.gameObject.layer != 10)
        {
            events.Death();
        }
    }
}
