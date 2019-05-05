[System.Serializable]
public class StateMachine
{
    public enum Check { Idle, Fail, Ok };

    public int StateMachineNumber;
    public int StateMachineType;
    public int StateMachineValue;
    public StateMachine.Check check = StateMachine.Check.Idle;
}
