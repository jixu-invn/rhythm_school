using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public static AnimationManager animationManager;

    public int ClueNbState = 8;
    public AnimationMapper[] animationMappers;

    private bool[] isPlayingAClue;
    
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

    private void Start()
    {
        isPlayingAClue = new bool[animationMappers.Length];
        int i = 0;
        for (i = 0; i<isPlayingAClue.Length;i++)
        {
            isPlayingAClue[i] = false;
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

    public bool InitClue(StateMachine stateMachine, float time, float duration)
    {
        if (stateMachine.Number > animationMappers.Length || stateMachine.Number < 0)
        {
            Debug.LogError("StateMachine " + stateMachine.Number + " doesn't exist.");
            return false;
        }
        /*
        Debug.Log("time : " + time);
        Debug.Log("duration : " + duration);
        */
        if (!isPlayingAClue[stateMachine.Number])
        {
            isPlayingAClue[stateMachine.Number] = true;
            StartCoroutine(animationMappers[stateMachine.Number].TriAnimator.ClueLayer.Go(stateMachine.Number, ClueNbState, time - duration, duration));
            return true;
        }

        return false;
        
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

    public void ResetClue(int stateMachineNumber)
    {
        isPlayingAClue[stateMachineNumber] = false;
    }
}
