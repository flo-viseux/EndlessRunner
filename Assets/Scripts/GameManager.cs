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

    public Vector3 gravity;


    //[SerializeField] private Canvas pressKey;
    

    private void Awake() 
    {
        Physics.gravity = gravity;
        generation = GameObject.Find("Generation").GetComponent<Generation>();
        playerAlive = true;
        score = 0;
        scoreText.text = "" + score;
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

    public void Score()
    {
        if (playerAlive)
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
            
    }
}
