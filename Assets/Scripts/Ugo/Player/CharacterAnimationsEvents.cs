using UnityEngine;
using System;

public class CharacterAnimationsEvents : MonoBehaviour
{
    #region API
    public event Action OnJump;
    public event Action OnSlide;
    public event Action OnFootStep;
    public event Action OnDeath;
    public event Action OnLeft;
    public event Action OnRight;

    public void jump()
    {
        OnJump?.Invoke();
    }


    public void slide()
    {
        OnSlide?.Invoke();
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
