using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    public bool inObstacle = false;
    #region API
    private void Update()
    {
        if(inObstacle)
            transform.position += Vector3.up * 0.01f;

    }
    private void OnTriggerEnter(Collider col) 
    {
        if (col.CompareTag("Player"))
        {
            Inventory.instance.AddCoins(1);
            Destroy(gameObject);
        }

        if(col.CompareTag("Obstacles"))
        {
            Debug.Log("hit");
            inObstacle = true;
        }

    }

    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Obstacles"))
        {
            Debug.Log("out");
            inObstacle = false;
        }
    }
    #endregion
}
