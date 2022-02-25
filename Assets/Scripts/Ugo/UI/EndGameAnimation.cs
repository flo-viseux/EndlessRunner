using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameAnimation : MonoBehaviour
{
    public Animator Panel;

    public void OpenPanel()
    {
        if (Panel != null)
        {
            Animator animator = Panel.GetComponent<Animator>();
            if(animator != null)
            {
                bool isOpen = animator.GetBool("open");
            }
        }
    }
}
