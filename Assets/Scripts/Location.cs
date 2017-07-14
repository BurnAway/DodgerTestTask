using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    private LevelConfig _levelConfig;

    private IPlayer _player;
    
    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _levelConfig = new LevelConfig();
        _player = new Player();
        _player.Initialize(_levelConfig.PlayerConfig);

    }

    void Update()
    {
        _player.Update(Time.deltaTime);
    }
}
