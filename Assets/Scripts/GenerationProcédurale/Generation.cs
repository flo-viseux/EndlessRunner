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
    public float speed;
    #endregion

    #region API
    public enum obstacles
    {
        nothing,
        stairs,
        wall,
        semiWall
    }

    public int a;
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

    private void Update() {
        if(speed < 15f)
        {
            speed = speed * 1.0005f;
        }

        currentObstacleLeft = plateformes[plateformes.Count - 1].transform.GetChild(1).gameObject;
        FindCurrentObstacle(currentObstacleLeft);

        currentObstacleCenter = plateformes[plateformes.Count - 1].transform.GetChild(2).gameObject;
        FindCurrentObstacle(currentObstacleCenter);

        currentObstacleRight = plateformes[plateformes.Count - 1].transform.GetChild(3).gameObject;
        FindCurrentObstacle(currentObstacleRight);

    }

    public IEnumerator GenerationPlateforme()
    {
        yield return new WaitForSeconds(0.4f);
        
        GameObject newPlateforme = Instantiate(Plateforme, Vector3.zero, Quaternion.identity);

        newPlateforme.transform.SetParent(GameObject.Find("Plateformes").transform);
        plateformes.Add(newPlateforme);
        newPlateforme.transform.position = plateformes[plateformes.Count - 2].transform.position + new Vector3(0f, 0f, 3f);

        GameObject newObstacleLeft = null;
        GameObject newObstacleCenter = null;
        GameObject newObstacleRight= null;

        CreateNewObstacle(newObstacleLeft, newPlateforme, new Vector3(-2f, 0f, 0f));
        CreateNewObstacle(newObstacleCenter, newPlateforme, new Vector3(0f, 0f, 0f));
        CreateNewObstacle(newObstacleRight, newPlateforme, new Vector3(2f, 0f, 0f));

        StartCoroutine(GenerationPlateforme());
    }

    public void FindCurrentObstacle(GameObject currentObstacle)
    {
        if(currentObstacle.layer == 7)
        {
            a = ((int)obstacles.wall);
            Debug.Log("wall");
        }
        else if(currentObstacle.layer == 8)
        {
            a = ((int)obstacles.nothing);
            Debug.Log("nothing");
        }
        else if(currentObstacle.layer == 9)
        {
            a = ((int)obstacles.stairs);
            Debug.Log("stairs");
        }
        else if(currentObstacle.layer == 10)
        {
            a = ((int)obstacles.semiWall);
            Debug.Log("semi wall");
        }
    }

    public void CreateNewObstacle(GameObject newObstacle, GameObject plateforme, Vector3 localPos)
    {
        if(a == ((int)obstacles.wall))
        {
            int numeroObstacle = Random.Range(0, Obstacles.Count);

            if(numeroObstacle > 1)
                numeroObstacle = 0;

            newObstacle = Instantiate(Obstacles[numeroObstacle], plateforme.transform);
            newObstacle.transform.localPosition = localPos;
        }

        if(a == ((int)obstacles.stairs))
        {
            newObstacle = Instantiate(Obstacles[1], plateforme.transform);
            newObstacle.transform.localPosition = localPos;
        }

        if(a == ((int)obstacles.semiWall))
        {
            int numeroObstacle = Random.Range(0, Obstacles.Count);

            if(numeroObstacle == 2)
                numeroObstacle = 0;
                
            newObstacle = Instantiate(Obstacles[numeroObstacle], plateforme.transform);
            newObstacle.transform.localPosition = localPos;
        }

        if(a == ((int)obstacles.nothing))
        {
            newObstacle = Instantiate(Obstacles[Random.Range(0, Obstacles.Count)], plateforme.transform);
            newObstacle.transform.localPosition = localPos;
        }
    }
}
