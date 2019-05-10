using System.Collections;
using UnityEngine;

public class ClueScript : MonoBehaviour
{
    private Animator animator;
    private AnimationManager animationManager;

    private void Start()
    {
        animationManager = AnimationManager.animationManager;
    }

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
    }

    public IEnumerator Go(int stateMachineNumber, int nbState, float time, float duration)
    {
        yield return new WaitForSecondsRealtime(time);
        
        animator.SetTrigger("Next");

        int j;

        duration = time < 0 ? duration + time : duration;

        for (j = 0; j < nbState; j++)
        {
            yield return new WaitForSecondsRealtime(duration / nbState);
            animator.SetTrigger("Next");
        }

        animationManager.ResetClue(stateMachineNumber);
    }
}
