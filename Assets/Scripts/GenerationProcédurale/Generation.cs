using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generation : MonoBehaviour
{
    #region SerializedField
    public List<GameObject> plateformes;
    [SerializeField] private GameObject Plateforme;
    [SerializeField] private GameObject Wall;
    [SerializeField] private GameObject SemiWall;
    [SerializeField] private GameObject Stairs; 
    [SerializeField] private GameObject Nothing;
    [SerializeField] private List<GameObject> Obstacles;
    [SerializeField] private GameObject currentObstacleLeft;
    [SerializeField] private GameObject currentObstacleCenter;
    [SerializeField] private GameObject currentObstacleRight;
    #endregion

    #region Attributes
    public float intervalleDuration;
    public float speed;

    public bool hasStarted;
    #endregion

    // Start is called before the first frame update
    private void Awake() 
    {
        hasStarted = true;
        Obstacles.Add(Nothing);
        Obstacles.Add(Wall);
        Obstacles.Add(SemiWall);
        Obstacles.Add(Stairs);

        StartCoroutine(GenerationPlateforme());
    }

    private void Update() 
    {
        if(speed == 0)
        {
            intervalleDuration = 8f;
        }
        else if(speed < 5 && speed != 0)
        {
            intervalleDuration = 0.7f;
        }
        else if(speed > 5f && speed < 8f)
        {
            intervalleDuration = 0.45f;
        }
        else if(speed > 8f && speed < 11f)
        {
            intervalleDuration = 0.36f;
        }
        else if(speed > 11f)
        {
            intervalleDuration = 0.25f;
        }


        if(speed < 11f && hasStarted == true)
        {
            speed = speed + 0.001f;
        }

        currentObstacleLeft = plateformes[plateformes.Count - 1].transform.GetChild(1).gameObject;
        currentObstacleCenter = plateformes[plateformes.Count - 1].transform.GetChild(2).gameObject;
        currentObstacleRight = plateformes[plateformes.Count - 1].transform.GetChild(3).gameObject;

    }

    public IEnumerator GenerationPlateforme()
    {
        yield return new WaitForSeconds(intervalleDuration);
        
        GameObject newPlateforme = Instantiate(Plateforme, Vector3.zero, Quaternion.identity);

        newPlateforme.transform.SetParent(GameObject.Find("Plateformes").transform);
        plateformes.Add(newPlateforme);
        newPlateforme.transform.position = plateformes[plateformes.Count - 2].transform.position + new Vector3(0f, 0f, 3f);

        GameObject newObstacleLeft = null;
        GameObject newObstacleCenter = null;
        GameObject newObstacleRight= null;

        CreateNewObstacle(currentObstacleLeft, newObstacleLeft, newPlateforme, new Vector3(-2f, 0f, 0f));
        CreateNewObstacle(currentObstacleCenter, newObstacleCenter, newPlateforme, new Vector3(0f, 0f, 0f));
        CreateNewObstacle(currentObstacleRight, newObstacleRight, newPlateforme, new Vector3(2f, 0f, 0f));

        if(newObstacleLeft == Obstacles[1] && newObstacleCenter == Obstacles[1] && newObstacleRight == Obstacles[1])
        {
            newObstacleRight = Obstacles[2];
        }

        StartCoroutine(GenerationPlateforme());
    }

    // public void FindCurrentObstacle(GameObject currentObstacle, int index)
    // {
    //     if(currentObstacle.layer == 7)
    //     {
    //         index = 0;
    //     }
        
    //     if(currentObstacle.layer == 8)
    //     {
    //         index = 1;
    //     }
        
    //     if(currentObstacle.layer == 9)
    //     {
    //         index = 2;
    //     }
        
    //     if(currentObstacle.layer == 10)
    //     {
    //         index = 3;
    //     }
    // }

    public void CreateNewObstacle(GameObject currentObstacle, GameObject newObstacle, GameObject plateforme, Vector3 localPos)
    {
        if(currentObstacle.layer == 7)
        {
            int numeroObstacle = Random.Range(0, Obstacles.Count);

            if(numeroObstacle > 1)
                numeroObstacle = 0;

            newObstacle = Instantiate(Obstacles[numeroObstacle], plateforme.transform);
            newObstacle.transform.localPosition = localPos;
        }
        
        if(currentObstacle.layer == 8)
        {
            newObstacle = Instantiate(Obstacles[Random.Range(0, Obstacles.Count)], plateforme.transform);
            newObstacle.transform.localPosition = localPos;
        }
        
        if(currentObstacle.layer == 9)
        {
            newObstacle = Instantiate(Obstacles[1], plateforme.transform);
            newObstacle.transform.localPosition = localPos;
        }
        
        if(currentObstacle.layer == 10)
        {
            int numeroObstacle = Random.Range(0, Obstacles.Count);
                
            newObstacle = Instantiate(Obstacles[0], plateforme.transform);
            newObstacle.transform.localPosition = localPos;
        }
    }
}
