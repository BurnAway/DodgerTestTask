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
        _view.transform.rotation = _model.Direction;
        _view.transform.localScale = new Vector3(playerConfig.Scale / heightInPixels, playerConfig.Scale / heightInPixels);
    }

    public void MoveTo()
    {
        _view.transform.position = new Vector2(_model.Position.x, _model.Position.y);
        _view.transform.rotation = _model.Direction;
    }

    public void Update()
    {

    }
}
