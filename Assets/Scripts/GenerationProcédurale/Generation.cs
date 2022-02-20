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
    [SerializeField] private GameObject lastObstacleLeft;
    [SerializeField] private GameObject lastObstacleCenter;
    [SerializeField] private GameObject lastObstacleRight;
    #endregion

    #region Attributes
    public float intervalleDuration;
    public float speed;

    public bool hasStarted;
    public bool walls;
    public bool stairs;
    #endregion

    // Start is called before the first frame update
    private void Awake() 
    {
        hasStarted = true;
        walls = true;
        stairs = true;
        Obstacles.Add(Wall);
        Obstacles.Add(Nothing);
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
            intervalleDuration = 0.5f;
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

        lastObstacleCenter = plateformes[plateformes.Count - 1].transform.GetChild(1).gameObject;
        lastObstacleLeft = plateformes[plateformes.Count - 1].transform.GetChild(2).gameObject;
        lastObstacleRight = plateformes[plateformes.Count - 1].transform.GetChild(3).gameObject;

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

        if(lastObstacleLeft.layer ==  7 && lastObstacleRight.layer == 7 && lastObstacleCenter.layer != 9)
            CreateNewObstacleSides(lastObstacleCenter, newObstacleCenter, newPlateforme, new Vector3(0f, 0f, 0f));
        else
            CreateNewObstacle(lastObstacleCenter, newObstacleCenter, newPlateforme, new Vector3(0f, 0f, 0f));

        LayerMask currentCenterObstacleLayer = newPlateforme.transform.GetChild(1).gameObject.layer;

        if (currentCenterObstacleLayer.value == 7)
        {
            if (lastObstacleLeft.layer != 9 && lastObstacleRight.layer != 9)
            {
                CreateNewObstacleSides(lastObstacleLeft, newObstacleLeft, newPlateforme, new Vector3(-2f, 0f, 0f));
                CreateNewObstacleSides(lastObstacleRight, newObstacleRight, newPlateforme, new Vector3(2f, 0f, 0f));
            }
            else if(lastObstacleLeft.layer != 9 && lastObstacleRight.layer == 9)
            {
                CreateNewObstacleSides(lastObstacleLeft, newObstacleLeft, newPlateforme, new Vector3(-2f, 0f, 0f));
                CreateNewObstacle(lastObstacleRight, newObstacleRight, newPlateforme, new Vector3(2f, 0f, 0f));

            }
            else if(lastObstacleLeft.layer == 9 && lastObstacleRight.layer != 9)
            {
                CreateNewObstacle(lastObstacleLeft, newObstacleLeft, newPlateforme, new Vector3(-2f, 0f, 0f));
                CreateNewObstacleSides(lastObstacleRight, newObstacleRight, newPlateforme, new Vector3(2f, 0f, 0f));
            }
            else if(lastObstacleLeft.layer == 9 && lastObstacleRight.layer == 9)
            {
                CreateNewObstacle(lastObstacleLeft, newObstacleLeft, newPlateforme, new Vector3(-2f, 0f, 0f));
                CreateNewObstacle(lastObstacleRight, newObstacleRight, newPlateforme, new Vector3(2f, 0f, 0f));
            }
        }
        else
        {
            CreateNewObstacle(lastObstacleLeft, newObstacleLeft, newPlateforme, new Vector3(-2f, 0f, 0f));
            CreateNewObstacle(lastObstacleRight, newObstacleRight, newPlateforme, new Vector3(2f, 0f, 0f));
        }

        StartCoroutine(GenerationPlateforme());
    }

    public void CreateNewObstacle(GameObject lastObstacle, GameObject newObstacle, GameObject plateforme, Vector3 localPos)
    {
        if(lastObstacle.layer == 7)
        {
            int i = Random.Range(0, 1);

            if(i == 0 && walls)
            {
                i = 1;
                walls = !walls;
            }
            else if(i == 0 && !walls)
            {
                walls = !walls;
            }

            newObstacle = Instantiate(Obstacles[i], plateforme.transform);
            newObstacle.transform.localPosition = localPos;
        }
        
        if(lastObstacle.layer == 8)
        {
            newObstacle = Instantiate(Obstacles[Random.Range(0, Obstacles.Count)], plateforme.transform);
            newObstacle.transform.localPosition = localPos;
        }
        
        if(lastObstacle.layer == 9)
        {
            newObstacle = Instantiate(Obstacles[0], plateforme.transform);
            newObstacle.transform.localPosition = localPos;
        }
        
        if(lastObstacle.layer == 10)
        {       
            newObstacle = Instantiate(Obstacles[1], plateforme.transform);
            newObstacle.transform.localPosition = localPos;
        }
    }

    public void CreateNewObstacleSides(GameObject lastObstacle, GameObject newObstacle, GameObject plateforme, Vector3 localPos)
    {
        Debug.Log(lastObstacle.layer);

        int i = Random.Range(1, 3);

        if ((i == 2 || i == 3) && lastObstacle.layer == 7)
        {
            i = 1;
        }
        else if ((i == 2 || i ==3) && stairs && lastObstacle.layer != 7)
        {
            i = 1;
            stairs = !stairs;
        }
        else if(i == 2 && !stairs)
        {
            i = 3;
            stairs = !stairs;
        }
        
            


        newObstacle = Instantiate(Obstacles[i], plateforme.transform);
        newObstacle.transform.localPosition = localPos;

    }
}
