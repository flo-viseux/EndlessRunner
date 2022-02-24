using UnityEngine;

public class SlideState : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) 
    {
        animator.GetComponent<CharacterAnimationsEvents>().slide();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        animator.GetComponent<CharacterAnimationsEvents>().endSlide();
    }
}
