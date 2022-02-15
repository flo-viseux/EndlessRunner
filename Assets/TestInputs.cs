using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInputs : MonoBehaviour
{
    public bool aze;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            transform.position = new Vector3(2f, 0f, -27f);
        }
    }
}
