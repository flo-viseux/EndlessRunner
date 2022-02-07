using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlateformeMovements : MonoBehaviour
{
    public float speed;
    private Generation generation;

    private void Awake() {
        generation = GameObject.Find("Generation").GetComponent<Generation>();
        speed = generation.speed;
    }

    // Update is called once per frame
    void Update()
    {
        speed = generation.speed;
        transform.position -= new Vector3 (0f, 0f, speed * Time.deltaTime);
    }

    public void Replace()
    {
        transform.position = new Vector3(0, 0, -3);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.layer == 6)
        {
            generation.plateformes.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
            
    }
    
}
