using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    //[SerializeField] private GameObject Timeline;
    [SerializeField] private Generation generation;
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private Inventory inventory;

    public Vector3 gravity;

    public bool playerAlive;

    public int score;
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
        startPanel.SetActive(true);
        EndPanel.SetActive(false);
    }
    
    public void OnStartGame()
    {

        if (Input.anyKeyDown && generation.isPlaying == false && movement.alive == true)
        {
            if(playerAlive)
            {
                startPanel.SetActive(false);
                generation.isPlaying = true;
                generation.speed = 3f;
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
            score = (int) (Time.time * (Time.time / 5));

        scoreText.text = "" + score;
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
                

            if(coinsF < inventory.coinsCount)
            {
                coinsF = Mathf.Lerp(coinsF, inventory.coinsCount + 1, t * Time.deltaTime);
                coinsPanel.text = "" + (int) coinsF;
            }
                

            if((int) coinsF == inventory.coinsCount && (int) runF == score)
            {
                if (totalF < score + inventory.coinsCount)
                {
                    totalF = Mathf.Lerp(totalF, score + inventory.coinsCount, t * Time.deltaTime);
                    TotalPanel.text = "" + (int) totalF;
                }
                    


            }

            generation.isPlaying = false;
            generation.speed = 0;
        }

        
            
    }
}
