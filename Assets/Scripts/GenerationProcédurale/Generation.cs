using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generation : MonoBehaviour
{
    #region SerializedField
    public List<GameObject> plateformes;
    [SerializeField] private GameObject Plateforme;

    [Header("Obstacles")]
    [SerializeField] private GameObject Wall;
    [SerializeField] private GameObject SemiWall;
    [SerializeField] private GameObject Stairs; 
    [SerializeField] private GameObject Nothing;
    [SerializeField] private List<GameObject> Obstacles;
    [SerializeField] private GameObject lastObstacleLeft;
    [SerializeField] private GameObject lastObstacleCenter;
    [SerializeField] private GameObject lastObstacleRight;

    [Header("Collectibles")]
    [SerializeField] private GameObject NoCoin;
    [SerializeField] private GameObject TwoCoins;
    [SerializeField] private GameObject ThreeCoins;
    [SerializeField] private List<GameObject> Collectibles;

    #endregion

    #region Attributes
    public float intervalleDuration;
    public float speed;

    public bool isPlaying;
    public bool canCreateStairs;
    public bool canCreateCoins;
    public int initialCdStairs;
    public int CdStairs;
    public int initialCdCoins;
    public int CdCoins;
    #endregion

    // Start is called before the first frame update
    private void Awake() 
    {
        isPlaying = false;

        canCreateStairs = false;
        CdStairs = initialCdStairs;

        canCreateCoins = false;
        CdCoins = initialCdCoins;

        speed = 0;

        Obstacles.Add(Wall);
        Obstacles.Add(Nothing);
        Obstacles.Add(SemiWall);
        Obstacles.Add(Stairs);

        Collectibles.Add(NoCoin);
        Collectibles.Add(TwoCoins);
        Collectibles.Add(ThreeCoins);

        StartCoroutine(GenerationPlateforme());
    }

    private void Update() 
    {
        if(Input.anyKeyDown && isPlaying == false)
        {
            isPlaying = true;
            speed = 3;
        }

        if (speed < 11f && isPlaying == true)
        {
            speed += 0.001f;
        }

        if (speed == 0)
        {
            intervalleDuration = 8f;
        }
        else if(speed < 3f)
        {
            intervalleDuration = 0.40f;
        }
        else if(speed < 5.5f && speed > 3f)
        {
            intervalleDuration = 0.36f;
        }
        else if(speed < 11f && speed > 5.5f)
        {
            intervalleDuration = 0.3f;
        }
        else if (speed > 11f)
        {
            intervalleDuration = 0.27f;
        }

        

        lastObstacleCenter = plateformes[plateformes.Count - 1].transform.GetChild(1).gameObject;
        lastObstacleLeft = plateformes[plateformes.Count - 1].transform.GetChild(2).gameObject;
        lastObstacleRight = plateformes[plateformes.Count - 1].transform.GetChild(3).gameObject;

        if(CdStairs <= 0)
        {
            canCreateStairs = !canCreateStairs;
            CdStairs = initialCdStairs;
        }

        if (CdCoins <= 0)
        {
            canCreateCoins = !canCreateCoins;
            CdCoins = initialCdCoins;
        }

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
                CreateNewObstacleSides(lastObstacleLeft, newObstacleLeft, newPlateforme, new Vector3(-3f, 0f, 0f));
                CreateNewObstacleSides(lastObstacleRight, newObstacleRight, newPlateforme, new Vector3(3f, 0f, 0f));
            }
            else if(lastObstacleLeft.layer != 9 && lastObstacleRight.layer == 9)
            {
                CreateNewObstacleSides(lastObstacleLeft, newObstacleLeft, newPlateforme, new Vector3(-3f, 0f, 0f));
                CreateNewObstacle(lastObstacleRight, newObstacleRight, newPlateforme, new Vector3(3f, 0f, 0f));

            }
            else if(lastObstacleLeft.layer == 9 && lastObstacleRight.layer != 9)
            {
                CreateNewObstacle(lastObstacleLeft, newObstacleLeft, newPlateforme, new Vector3(-3f, 0f, 0f));
                CreateNewObstacleSides(lastObstacleRight, newObstacleRight, newPlateforme, new Vector3(3f, 0f, 0f));
            }
            else if(lastObstacleLeft.layer == 9 && lastObstacleRight.layer == 9)
            {
                CreateNewObstacle(lastObstacleLeft, newObstacleLeft, newPlateforme, new Vector3(-3f, 0f, 0f));
                CreateNewObstacle(lastObstacleRight, newObstacleRight, newPlateforme, new Vector3(3f, 0f, 0f));
            }
        }
        else if(currentCenterObstacleLayer.value != 7 && lastObstacleCenter.layer == 7 && lastObstacleLeft.layer != 9 && lastObstacleRight.layer != 9)
        {
            CreateNewObstacleSides(lastObstacleCenter, newObstacleLeft, newPlateforme, new Vector3(-3f, 0f, 0f));
            CreateNewObstacleSides(lastObstacleCenter, newObstacleRight, newPlateforme, new Vector3(3f, 0f, 0f));

        }
        else if(currentCenterObstacleLayer.value != 7 && lastObstacleCenter.layer == 7 && lastObstacleLeft.layer == 9 && lastObstacleRight.layer != 9)
        {
            CreateNewObstacle(lastObstacleLeft, newObstacleLeft, newPlateforme, new Vector3(-3f, 0f, 0f));
            CreateNewObstacleSides(lastObstacleCenter, newObstacleRight, newPlateforme, new Vector3(3f, 0f, 0f));
        }
        else if (currentCenterObstacleLayer.value != 7 && lastObstacleCenter.layer == 7 && lastObstacleLeft.layer != 9 && lastObstacleRight.layer == 9)
        {
            CreateNewObstacleSides(lastObstacleCenter, newObstacleLeft, newPlateforme, new Vector3(-3f, 0f, 0f));
            CreateNewObstacle(lastObstacleRight, newObstacleRight, newPlateforme, new Vector3(3f, 0f, 0f));
        }
        else if (currentCenterObstacleLayer.value != 7 && lastObstacleCenter.layer == 7 && lastObstacleLeft.layer == 9 && lastObstacleRight.layer == 9)
        {
            CreateNewObstacle(lastObstacleLeft, newObstacleLeft, newPlateforme, new Vector3(-3f, 0f, 0f));
            CreateNewObstacle(lastObstacleRight, newObstacleRight, newPlateforme, new Vector3(3f, 0f, 0f));
        }
        else
        {
            CreateNewObstacle(lastObstacleLeft, newObstacleLeft, newPlateforme, new Vector3(-3f, 0f, 0f));
            CreateNewObstacle(lastObstacleRight, newObstacleRight, newPlateforme, new Vector3(3f, 0f, 0f));
        }

        if (canCreateCoins)
        {
            CreateCoins(newPlateforme.transform.GetChild(1).transform, newPlateforme.transform.GetChild(2).transform, newPlateforme.transform.GetChild(3).transform);
        }

            

        StartCoroutine(GenerationPlateforme());
    }

    public void CreateNewObstacle(GameObject lastObstacle, GameObject newObstacle, GameObject plateforme, Vector3 localPos)
    {
        if(lastObstacle.layer == 7)
        {
            newObstacle = Instantiate(Obstacles[1], plateforme.transform);
            newObstacle.transform.localPosition = localPos;
        }
        
        if(lastObstacle.layer == 8)
        {
            if(!canCreateStairs)
            {
                newObstacle = Instantiate(Obstacles[Random.Range(0, Obstacles.Count)], plateforme.transform);
            }
            else
            {
                int i = Random.Range(0, Obstacles.Count);

                if (i == 3)
                    i = 1;

                newObstacle = Instantiate(Obstacles[i], plateforme.transform);
            }

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

        CdStairs -= 1;
        CdCoins -= 1;
    }

    public void CreateNewObstacleSides(GameObject lastObstacle, GameObject newObstacle, GameObject plateforme, Vector3 localPos)
    {
        int i = Random.Range(1, 3);

        if ((i == 2 || i == 3) && lastObstacle.layer == 7)
        {
            i = 1;
        }
        else if ((i == 2 || i ==3) && canCreateStairs && lastObstacle.layer != 7)
        {
            i = 1;
        }
        else if(i == 2 && !canCreateStairs)
        {
            i = 3;
        }

        CdStairs -= 1;
        CdCoins -= 1;

        newObstacle = Instantiate(Obstacles[i], plateforme.transform);
        newObstacle.transform.localPosition = localPos;

    }

    public void CreateCoins(Transform left, Transform center, Transform right)
    {
        int i = Random.Range(0, 3);
        GameObject Coins = null;

        if (i == 0)
        {
            if (left.gameObject.layer == 10)
                Coins = Instantiate(Collectibles[2], left);
            else
                Coins = Instantiate(Collectibles[Random.Range(0, Collectibles.Count)], left);


            Coins.transform.localPosition = Vector3.zero;
        }
        else if (i == 1)
        {
            if (center.gameObject.layer == 10)
                Coins = Instantiate(Collectibles[2], center);
            else
                Coins = Instantiate(Collectibles[Random.Range(0, Collectibles.Count)], center);

            Coins.transform.localPosition = Vector3.zero;
        }
        else if (i == 2)
        {
            if (right.gameObject.layer == 10)
                Coins = Instantiate(Collectibles[2], right);
            else
                Coins = Instantiate(Collectibles[Random.Range(0, Collectibles.Count)], right);

            Coins.transform.localPosition = Vector3.zero;
        }
    }
}
