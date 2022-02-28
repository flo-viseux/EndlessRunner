using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //[SerializeField] private GameObject Timeline;
    [SerializeField] private Generation generation;
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private Inventory inventory;
    [SerializeField] private CharacterAudio audio = null;

    public Vector3 gravity;

    public bool playerAlive;

    public float score;
    public TMP_Text scoreText;

    public GameObject startPanel;
    public GameObject EndPanel;

    public float t = 0f;
    public float runF;
    public TMP_Text RunPanel;
    public float coinsF;
    public TMP_Text coinsPanel;
    public float totalF;
    public TMP_Text TotalPanel;


    //[SerializeField] private Canvas pressKey;


    private void Awake() 
    {
        Physics.gravity = gravity;
        generation = GameObject.Find("Generation").GetComponent<Generation>();
        movement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();

        playerAlive = true;

        score = 0;
        scoreText.text = "" + score;

        runF = 0f;
        coinsF = 0f;
        totalF = 0f;

        startPanel.SetActive(true);
        EndPanel.SetActive(false);
    }
    
    public void OnStartGame()
    {
        if (Input.anyKeyDown && generation.isPlaying == false && movement.alive == true)
        {
            if(playerAlive)
            {
                audio.PlayStartSound();
                startPanel.SetActive(false);
                generation.isPlaying = true;
                generation.speed = 4f;
                movement._animator.SetTrigger(movement.playHash);
            }
        }
    }

    public void EndGame()
    {
        playerAlive = false;
        EndPanel.SetActive(true);
    }

    public void Score()
    {
        if (playerAlive && generation.isPlaying == true)
            score += ((generation.speed + generation.speed) / 200);

        scoreText.text = "" + (int) score;
    }

    private void Update()
    {
        OnStartGame();

        Score();

        if (!playerAlive)
        {
            if (runF < score)
            {
                runF = Mathf.Lerp(runF, score + 1, t * Time.deltaTime);
                RunPanel.text = "" + (int) runF;
            }
                

            if(inventory.coinsCount != 0 && coinsF < inventory.coinsCount)
            {
                coinsF = Mathf.Lerp(coinsF, inventory.coinsCount + 1, t * Time.deltaTime);
                coinsPanel.text = "" + (int) coinsF;

            }
            else if(inventory.coinsCount == 0)
            {
                coinsF = 0;
                coinsPanel.text = "" + (int) coinsF;
            }
                
                

            if((int) coinsF == inventory.coinsCount && (int) runF == score)
            {
                if (totalF < score + inventory.coinsCount)
                {
                    totalF = Mathf.Lerp(totalF, score + inventory.coinsCount + 1, t * Time.deltaTime);
                    TotalPanel.text = "" + (int) totalF;
                }
            }

            generation.isPlaying = false;
            generation.speed = 0;

            if (Input.anyKeyDown)
                SceneManager.LoadScene("Game");

        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
            
    }
}
