using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    private Action<IEnemy> _onEnemyLeftArea = (enemy) => { };

    public void Initialize(Action<IEnemy> onEnemyLeftArea)
    {
        _onEnemyLeftArea = onEnemyLeftArea;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject collosoinGameObject = collision.gameObject;
        if (collosoinGameObject.CompareTag(LiteralConstant.EnemyTag))
        {
            _onEnemyLeftArea(collosoinGameObject.transform.GetComponent<EnemyView>().Model);
        }
    }
}
