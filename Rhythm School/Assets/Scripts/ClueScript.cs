using System.Collections;
using UnityEngine;

public class ClueScript : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private AnimationManager animationManager;

    private void Start()
    {
        animationManager = AnimationManager.animationManager;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public IEnumerator Go(int stateMachineNumber, int nbState, float time, float duration)
    {
        Color c = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f, 1f, 1f);
        spriteRenderer.color = c;
        float normalTime = (time + duration + Time.timeSinceLevelLoad);
        float startingTime = Time.timeSinceLevelLoad;


        float stepDuration = duration / nbState;
        int j = 0;

        if (time > 0)
        {
            yield return new WaitForSecondsRealtime(time);
        }
        else
        {
            time *= -1;
            j = (int)Mathf.Floor(time / stepDuration);
            if (j < nbState)
            {
                animator.Play("clue_" + j);
                yield return new WaitForSecondsRealtime(stepDuration - (time - stepDuration * j));
                j++;
            }
        }


        int i;

        for (i = j; i < nbState; i++)
        {
            animator.SetTrigger("Next");
            yield return new WaitForSecondsRealtime(stepDuration);
        }
        animator.SetTrigger("Next");
        animationManager.ResetClue(stateMachineNumber);
    }
}
