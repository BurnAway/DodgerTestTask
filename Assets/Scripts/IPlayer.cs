using System;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayer
{
    Vector2 Position { get; }

    void Update(float deltaTime);

}