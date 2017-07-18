using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    private Enemy _model;

    public void Initialize(Enemy model)
    {
        _model = model;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        _model.State.OnCollisionEnter(collision);
    }
}