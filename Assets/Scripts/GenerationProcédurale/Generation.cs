using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Generation : MonoBehaviour
{
    #region SerializedField
    [SerializeField] private GameObject Plateforme;
    [SerializeField] private GameObject Wall;
    [SerializeField] private GameObject SemiWall;
    [SerializeField] private GameObject Stairs; 
    [SerializeField] private GameObject Nothing;
    [SerializeField] private List<GameObject> Obstacles;
    #endregion

    // Start is called before the first frame update
    private void Awake() 
    {
        Obstacles.Add(Nothing);
        Obstacles.Add(Wall);
        Obstacles.Add(SemiWall);
        Obstacles.Add(Stairs);

        StartCoroutine(GenerationPlateforme());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator GenerationPlateforme()
    {
        yield return new WaitForSeconds(1.494f);

        GameObject newPlateforme = Instantiate(Plateforme, Vector3.zero, Quaternion.identity);

        GameObject newObstacleLeft = Instantiate(Obstacles[Random.Range(0, Obstacles.Count)], new Vector3(-2f, 0f, 0f), Quaternion.identity);

        newObstacleLeft.transform.SetParent(newPlateforme.transform);

        StartCoroutine(GenerationPlateforme());
    }
}
