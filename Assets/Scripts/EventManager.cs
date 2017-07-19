using System;

public static class EventManager
{
    public static Action<IEnemy> OnEnemyDied = (enemy) => { };
    public static Action OnPlayerDied = () => { };
    public static Action OnPlayerHit = () => { };
    public static Action<int> OnScoreUp = (score) => { };
}