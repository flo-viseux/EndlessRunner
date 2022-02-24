using UnityEngine;
using System;

public class CharacterAnimationsEvents : MonoBehaviour
{
    #region Private 
    private PlayerMovement movement = null;

    private void Awake()
    {
        movement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }
    #endregion
    #region API
    public event Action OnJump;
    public event Action OnEndJump;
    public event Action OnSlide;
    public event Action OnEndSlide;
    public event Action OnFootStep;
    public event Action OnDeath;
    public event Action OnLeft;
    public event Action OnRight;

    public void jump()
    {
        OnJump?.Invoke();
    }

    public void endJump()
    {
        OnEndJump?.Invoke();
        movement.EndJump();
    }

    public void slide()
    {
        OnSlide?.Invoke();
    }

    public void endSlide()
    {
        OnEndSlide?.Invoke();
        movement.EndSlide();
    }


    public void FootStep()
    {
        OnFootStep?.Invoke();
    }

    public void Left()
    {
        OnLeft?.Invoke();
    }

    public void Right()
    {
        OnRight?.Invoke();
    }

    public void Death()
    {
        OnDeath?.Invoke();
    }
    #endregion
}
