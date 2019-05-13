[System.Serializable]
public class BeatData
{
    public float Timer;
    public BeatInput[] Inputs;
    public StateMachine[] stateMachines;
    private bool clued = false;

    public bool GetClued()
    {
        return clued;
    }

    public void HasBeenClued()
    {
        clued = true;
    }

    public float GetNormalizedTimer()
    {
        return Timer / 1000f;
    }
}
