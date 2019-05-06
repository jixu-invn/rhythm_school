using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public static AnimationManager animationManager;

    public AnimationMapper[] animationMappers;
    
    private void Awake()
    {
        if (animationManager == null)
        {
            animationManager = this;
        }
        else
        {
            if (animationManager != this)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Init(StateMachine stateMachine)
    {
        if (stateMachine.Number > animationMappers.Length || stateMachine.Number < 0)
        {
            Debug.LogError("StateMachine " + stateMachine.Number + " doesn't exist.");
            return;
        }

        if (animationMappers[stateMachine.Number].TypeCodes.Length > 0)
        {
            string type = stateMachine.Name.Substring(0, animationMappers[stateMachine.Number].TypeCodes[0].Length);

            bool found = false;

            foreach (string s in animationMappers[stateMachine.Number].TypeCodes)
            {
                if (s.Equals(type))
                {
                    found = true;
                    break;
                }
            }

            if (found)
                animationMappers[stateMachine.Number].animator.Play(type);
        }
    }

    public void Play(StateMachine stateMachine)
    {
        if (stateMachine.Number > animationMappers.Length || stateMachine.Number < 0)
        {
            Debug.LogError("StateMachine " + stateMachine.Number + " doesn't exist.");
            return;
        }

        Animator animator = animationMappers[stateMachine.Number].animator;
        
        if (stateMachine.LastAnimation)
        {
            animator.SetTrigger("LastAnimation");
        }

        string code = "";

        switch (stateMachine.GetCheck())
        {
            case StateMachine.Check.Fail:
                code = "_Fail";
                break;
            case StateMachine.Check.Ok:
                code = "_Ok";
                break;
        }

        string stateName = stateMachine.Name + code;

        animator.Play(stateName);
    }
}
