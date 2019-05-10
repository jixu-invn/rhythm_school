[System.Serializable]
public class BeatData
{
    public float Timer;
    public BeatInput[] Inputs;
    public StateMachine[] stateMachines;

    public float GetNormalizedtimer()
    {
        return Timer / 1000f;
    }
}
