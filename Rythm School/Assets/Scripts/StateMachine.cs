[System.Serializable]
public class StateMachine
{
    public enum Check { Idle, Fail, Ok };

    public int Number;
    public string Name;
    public bool NeedInit = false;
    public bool LastAnimation = false;
    private StateMachine.Check check = StateMachine.Check.Idle;
    private bool clued = false;

    public void HasBeenClued()
    {
        clued = true;
    }

    public bool GetClued()
    {
        return clued;
    }

    public StateMachine.Check GetCheck()
    {
        return check;
    }

    public void SetFail()
    {
        check = StateMachine.Check.Fail;
    }

    public void SetOk()
    {
        check = StateMachine.Check.Ok;
    }
}
