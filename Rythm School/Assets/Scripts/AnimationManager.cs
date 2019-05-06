using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public static AnimationManager animationManager;

    public AnimationMapper[] animationMappers;
    public string[] TypeCodes;

    private bool[] initialised;

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

        initialised = new bool[animationMappers.Length];

        int i = 0;

        for (i = 0; i < initialised.Length; i++)
        {
            initialised[i] = false;
        }
    }

    public void Init(StateMachine stateMachine)
    {
        if (stateMachine.Number > animationMappers.Length || stateMachine.Number < 0)
        {
            Debug.LogError("StateMachine " + stateMachine.Number + " doesn't exist.");
            return;
        }

        if (TypeCodes.Length > 0 && initialised[stateMachine.Number] == false)
        {
            string type = stateMachine.Name.Substring(0, TypeCodes[0].Length);

            bool found = false;

            foreach (string s in TypeCodes)
            {
                if (s.Equals(type))
                {
                    found = true;
                    break;
                }
            }

            if (found)
                animationMappers[stateMachine.Number].animator.Play(type);

            initialised[stateMachine.Number] = true;
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
            initialised[stateMachine.Number] = false;
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
