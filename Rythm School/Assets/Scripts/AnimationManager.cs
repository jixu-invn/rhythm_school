using System.Collections;
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
            string type = stateMachine.Name.Substring(0, stateMachine.Name.IndexOf('_'));

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
            {
                string nameState = type + "_idle";

                animationMappers[stateMachine.Number].TriAnimator.BaseLayer.Play(nameState);
            }
        }
    }

    public void InitClue(StateMachine stateMachine, float time)
    {
        if (stateMachine.Number > animationMappers.Length || stateMachine.Number < 0)
        {
            Debug.LogError("StateMachine " + stateMachine.Number + " doesn't exist.");
            return;
        }

        StartCoroutine(PlayClue(stateMachine, time));
    }

    private IEnumerator PlayClue(StateMachine stateMachine, float time)
    {
        Animator clueLayer = animationMappers[stateMachine.Number].TriAnimator.ClueLayer;
        yield return new WaitForSecondsRealtime(time);
        clueLayer.Play("clue", clueLayer.GetLayerIndex("Base Layer"));
    }
    
    public void Play(StateMachine stateMachine)
    {
        if (stateMachine.Number > animationMappers.Length || stateMachine.Number < 0)
        {
            Debug.LogError("StateMachine " + stateMachine.Number + " doesn't exist.");
            return;
        }

        Animator baseLayer = animationMappers[stateMachine.Number].TriAnimator.BaseLayer;
        Animator effectLayer = animationMappers[stateMachine.Number].TriAnimator.EffectLayer;
        
        baseLayer.SetBool("LastAnimation", stateMachine.LastAnimation);

        string baseName = "Base Layer.";

        string stateName = baseName + stateMachine.Name;

        switch (stateMachine.GetCheck())
        {
            case StateMachine.Check.Fail:
                effectLayer.Play(baseName + "fail");
                break;
            case StateMachine.Check.Ok:
                effectLayer.Play(baseName + "ok");
                break;
        }

        baseLayer.Play(stateName);
    }
}
