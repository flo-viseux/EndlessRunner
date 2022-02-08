using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject Timeline;
    [SerializeField] private InputAction startAction;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Generation generation;


    private void Start() 
    {
        generation = GameObject.Find("Generation").GetComponent<Generation>();
        startAction = playerInput.actions.FindAction("StartGame");
    }

    public void OnStartGame()
    {
        if(Timeline.activeInHierarchy == false)
        {
            Timeline.SetActive(true);
            generation.hasStarted = true;
        }
            
    }
}
