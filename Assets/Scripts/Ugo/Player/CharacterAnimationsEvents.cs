using UnityEngine;
using System;

public class CharacterAnimationsEvents : MonoBehaviour
{
    #region API
    public event Action OnJump;
    public event Action OnSlide;
    public event Action OnFootStep;
    public event Action OnDeath;

    public void jump()
    {
        OnJump?.Invoke();
    }

    public void Slide()
    {
        OnSlide?.Invoke();
    }

    public void FootStep()
    {
        OnFootStep?.Invoke();
    }

    public void Death()
    {
        OnDeath?.Invoke();
    }
    #endregion
}
