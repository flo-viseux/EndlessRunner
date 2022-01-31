using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformeMovements : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3 (0f, 0f, speed * Time.deltaTime);
    }
}
