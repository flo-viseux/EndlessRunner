using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rot_Bitcoin : MonoBehaviour
{
    public float rotspeed = 5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f, rotspeed, 0f));
    }
}
