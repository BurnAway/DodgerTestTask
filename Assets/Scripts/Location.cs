using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    private PlayerConfig _playerConfig = new PlayerConfig();
    private IPlayer _player;
    
    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _player = new Player(_playerConfig);
    }

    void Update()
    {
        _player.Update(Time.deltaTime);
    }
}
