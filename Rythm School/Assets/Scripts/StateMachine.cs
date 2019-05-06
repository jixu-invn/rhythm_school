[System.Serializable]
public class StateMachine
{
    public enum Check { Idle, Fail, Ok };

    public int StateMachineNumber;
    public int StateMachineType;
    public int StateMachineValue;
    private StateMachine.Check check = StateMachine.Check.Idle;

    public void SetFail()
    {
        check = StateMachine.Check.Fail;
    }

    public void SetOk()
    {
        check = StateMachine.Check.Ok;
    }
}
