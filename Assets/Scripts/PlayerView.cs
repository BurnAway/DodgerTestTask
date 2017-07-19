﻿using System;
using System.Collections.Generic;
using CnControls;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private Player _model;

    public void Initialize(Player model)
    {
        _model = model;
    }

    void Update()
    {
        if (InputController.AcceptInput)
        {
            float dx = CnInputManager.GetAxis("Horizontal");
            float dy = CnInputManager.GetAxis("Vertical");

            Vector2 direction = new Vector2(dx, dy).normalized;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

            transform.position = new Vector2(transform.position.x + direction.x * _model.Speed * Time.deltaTime, transform.position.y + direction.y * _model.Speed * Time.deltaTime);
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        if (CnInputManager.GetButtonDown("Jump"))
        {
            _model.StartFire();
        }
        else if (CnInputManager.GetButtonUp("Jump"))
        {
            _model.EndFire();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collisionGameObject = collision.gameObject;

        if (collisionGameObject.CompareTag(LiteralConstant.EnemyTag))
        {
            _model.ApplyDamage();
        }
    }
}
