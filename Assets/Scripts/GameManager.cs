using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    //[SerializeField] private GameObject Timeline;
    //[SerializeField] private InputAction startAction;
    //[SerializeField] private PlayerInput playerInput;
    [SerializeField] private Generation generation;

    public bool playerAlive;
    public int score;
    public TMP_Text scoreText;


    //[SerializeField] private Canvas pressKey;
    

    private void Awake() 
    {
        generation = GameObject.Find("Generation").GetComponent<Generation>();
        playerAlive = true;
        score = 0;
        scoreText.text = "" + score;
        //startAction = playerInput.actions.FindAction("StartGame");
        //pressKey.enabled = true;
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
        //Timeline.SetActive(true);
        //pressKey.enabled = false;

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
