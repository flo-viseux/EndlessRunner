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

    [SerializeField] private Canvas pressKey;

    private void Start() 
    {
        generation = GameObject.Find("Generation").GetComponent<Generation>();
        startAction = playerInput.actions.FindAction("StartGame");
        pressKey.enabled = true;
    }

    public void OnStartGame()
    {
        if(Timeline.activeInHierarchy == false)
        {
            StartCoroutine(StartGame());
        }
            
    }

    public IEnumerator StartGame()
    {
        Timeline.SetActive(true);
        pressKey.enabled = false;

        yield return new WaitForSeconds(3f);

        generation.hasStarted = true;
    }
}
