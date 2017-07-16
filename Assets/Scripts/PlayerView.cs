using System;
using System.Collections.Generic;
using CnControls;
using UnityEngine;

class PlayerView : MonoBehaviour
{
    private GameObject _view;
    private Player _model;

    public PlayerView(Player model, PlayerConfig playerConfig)
    {
        _model = model;
        
        _view = Instantiate(Resources.Load("Prefabs/Player")) as GameObject;
        float heightInPixels = _view.GetComponent<SpriteRenderer>().bounds.size.y;

        _view.transform.position = _model.Position;
        _view.transform.localScale = new Vector3(playerConfig.Scale / heightInPixels, playerConfig.Scale / heightInPixels);
    }

    public void MoveTo()
    {
        float dx = CnInputManager.GetAxis("Horizontal");
        float dy = CnInputManager.GetAxis("Vertical");

        Vector2 direction = new Vector2(dx, dy).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        //Direction = Quaternion.AngleAxis(angle, Vector3.forward);
        //Position = new Vector2(Position.x + direction.x * Speed * Time.deltaTime, Position.y + direction.y * Speed * Time.deltaTime);

        _view.transform.position = new Vector2(_view.transform.position.x + direction.x * _model.Speed * Time.deltaTime, _view.transform.position.y + direction.y * _model.Speed * Time.deltaTime);
        _view.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void Update()
    {
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collisionGameObject = collision.gameObject;
    }
}
