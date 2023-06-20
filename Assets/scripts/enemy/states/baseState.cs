public abstract class baseState
{
    public enemy enemy;
    public stateMachine stateMachine;
    public abstract void Enter();

    public abstract void Perform();

    public abstract void Exit();
}