using System;
using System.Collections.Generic;

public interface IEnemyState
{
    void Enter();
    void Update();
    void Exit();
}

public abstract class EnemyState : IEnemyState
{
    public void Enter()
    {
        throw new NotImplementedException();
    }

    public void Update()
    {
        throw new NotImplementedException();
    }

    public void Exit()
    {
        throw new NotImplementedException();
    }
}