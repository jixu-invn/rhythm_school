[System.Serializable]
public class StateMachine
{
    public enum Check { Idle, Fail, Ok };

    public int Number;
    public int Type;
    public int Value;
    public bool LastAnimation = false;
    private StateMachine.Check check = StateMachine.Check.Idle;

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
