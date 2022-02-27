using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    //[SerializeField] private GameObject Timeline;
    [SerializeField] private Generation generation;

    public bool playerAlive;
    public int score;
    public TMP_Text scoreText;
    public GameObject startPanel;
    public Vector3 gravity;
    public GameObject EndPanel;

    //[SerializeField] private Canvas pressKey;
    

    private void Awake() 
    {
        Physics.gravity = gravity;
        generation = GameObject.Find("Generation").GetComponent<Generation>();
        playerAlive = true;
        score = 0;
        scoreText.text = "" + score;
        startPanel.SetActive(true);
        EndPanel.SetActive(false);
    }
    
    /*public void OnStartGame()
    {
        if(Timeline.activeInHierarchy == false)
        {
            StartCoroutine(StartGame());
        }
            
    }*/

    public IEnumerator StartGame()
    {
        yield return new WaitForSeconds(3f);

        generation.isPlaying = true;
    }

    public void EndGame()
    {
        playerAlive = false;
        EndPanel.SetActive(true);
    }

    public void Score()
    {
        if (playerAlive && generation.isPlaying == true)
            score = (int) (Time.time * (Time.time / 5));

        scoreText.text = "" + score;
    }

    private void Update()
    {
        Score();

        if (!playerAlive)
        {
            generation.isPlaying = false;
            generation.speed = 0;
        }

        if(Input.anyKeyDown && playerAlive)
        {
            startPanel.SetActive(false);
        }
            
    }
}
