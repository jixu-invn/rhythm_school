using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private class AnimationState
    {
        public int Type;
        public int Value;
        public bool Fail;
        public bool Ok;
    }

    public static AnimationManager animationManager;

    public AnimationMapper[] animationMappers;

    private AnimationState old;

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

        old = new AnimationState();
    }

    public void Init(StateMachine stateMachine)
    {
        if (stateMachine.Number > animationMappers.Length || stateMachine.Number < 0)
        {
            Debug.LogError("StateMachine " + stateMachine.Number + " doesn't exist.");
            return;
        }

        animationMappers[stateMachine.Number].animator.SetInteger("Type", stateMachine.Type);
    }

    public void Play(StateMachine stateMachine)
    {
        if (stateMachine.Number > animationMappers.Length || stateMachine.Number < 0)
        {
            Debug.LogError("StateMachine " + stateMachine.Number + " doesn't exist.");
            return;
        }

        SaveOld(stateMachine.Number);

        AnimationState toPlay = new AnimationState
        {
            Type = stateMachine.Type,
            Value = stateMachine.Value,
            Fail = stateMachine.GetCheck() == StateMachine.Check.Fail ? true : false,
            Ok = stateMachine.GetCheck() == StateMachine.Check.Ok ? true : false
        };

        SetAnimationState(stateMachine.Number, toPlay);

        if (stateMachine.LastAnimation)
            Reset(stateMachine.Number);
        else
            Undo(stateMachine.Number);
    }

    private void SaveOld(int stateMachineNumber)
    {
        Animator animator = animationMappers[stateMachineNumber].animator;

        old.Type = animator.GetInteger("Type");
        old.Value = animator.GetInteger("Value");
        old.Fail = animator.GetBool("Fail");
        old.Ok = animator.GetBool("Ok");
    }

    private void SetAnimationState(int stateMachineNumber, AnimationState animationState)
    {
        Animator animator = animationMappers[stateMachineNumber].animator;

        animator.SetInteger("Type", animationState.Type);
        animator.SetInteger("Value", animationState.Value);
        animator.SetBool("Fail", animationState.Fail);
        animator.SetBool("Ok", animationState.Ok);
    }

    private void Undo(int stateMachineNumber)
    {
        SetAnimationState(stateMachineNumber, old);
    }

    private void Reset(int stateMachineNumber)
    {
        Animator animator = animationMappers[stateMachineNumber].animator;

        animator.SetInteger("Type", 0);
        animator.SetInteger("Value", 0);
        animator.SetBool("Fail", false);
        animator.SetBool("Ok", true);
    }
}
