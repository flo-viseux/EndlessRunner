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

    public float speed;
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
            speed = speed * 1.0001f;
        }
        
    }

    public IEnumerator GenerationPlateforme()
    {
        yield return new WaitForSeconds(0.2f);
        
        GameObject newPlateforme = Instantiate(Plateforme, Vector3.zero, Quaternion.identity);

        newPlateforme.transform.SetParent(GameObject.Find("Plateformes").transform);
        plateformes.Add(newPlateforme);
        newPlateforme.transform.position = plateformes[plateformes.Count - 2].transform.position + new Vector3(0f, 0f, 3f);

        GameObject newObstacleLeft = Instantiate(Obstacles[Random.Range(0, Obstacles.Count)], new Vector3(-2f, 0f, 0f), Quaternion.identity);

        newObstacleLeft.transform.SetParent(newPlateforme.transform);
        newObstacleLeft.transform.localPosition = new Vector3(-2f, 0f, 0f);

        StartCoroutine(GenerationPlateforme());
    }
}
