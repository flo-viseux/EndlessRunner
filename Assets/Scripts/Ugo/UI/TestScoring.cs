using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestScoring : MonoBehaviour
{
    public int score;
    public TMP_Text scoreText;
    public bool playerAlive;
    
    private void Awake() 
    {
        score = 0;
        scoreText.text = "" + score;
        playerAlive = true;
    }

    public void Score()
    {
        if (playerAlive)
        {
            score = (int) (Time.time * (Time.time / 5));
        }

        scoreText.text = "" + score;
    }

    // Update is called once per frame
    private void Update()
    {
        Score();
    }
}
