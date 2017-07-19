using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    public Enemy Model { get; private set; }

    public void Initialize(Enemy model)
    {
        Model = model;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Model.State.OnCollisionEnter(collision);
    }
}