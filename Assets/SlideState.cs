using UnityEngine;

public class SlideState : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) 
    {
        animator.GetComponent<CharacterAnimationsEvents>().Slide();
    }
}
