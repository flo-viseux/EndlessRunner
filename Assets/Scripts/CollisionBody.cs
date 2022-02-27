using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionBody : MonoBehaviour
{
    public CharacterAnimationsEvents events;
    public bool _isSliding = false;
    // Start is called before the first frame update
    void Awake()
    {
        _isSliding = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Death" && other.gameObject.layer == 10 && !_isSliding)
        {
            events.Death();
        }
        else if(other.gameObject.tag == "Death" && other.gameObject.layer != 10)
        {
            events.Death();
        }
    }
}
