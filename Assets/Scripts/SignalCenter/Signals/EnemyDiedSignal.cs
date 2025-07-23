using EnemyModule;

public struct EnemyDiedSignal
{
    public EnemyView EnemyView { get; }

    public EnemyDiedSignal(EnemyView view)
    {
        EnemyView = view;
    }
}