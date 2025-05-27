
public interface IState
{
    bool isComplete { get; }
    float time { get; }

    void Enter();
    void Do();
    void FixedDo();
    void Exit();
}
