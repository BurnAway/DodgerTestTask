using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    public GameObject CameraController;
    public Rect Rect;

    private LevelConfig _levelConfig;
    private float _width;
    private float _height;

    private IPlayer _player;
    
    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _levelConfig = new LevelConfig();

        InitializeLocation();

        _player = new Player();
        _player.Initialize(_levelConfig.PlayerConfig);

        CameraController camera = CameraController.GetComponent<CameraController>();
        camera.Initialize(this, _player.View.transform, _levelConfig.CameraConfig);
    }

    private void InitializeLocation()
    {
        _width = _levelConfig.LocationConfig.Width;
        _height = _levelConfig.LocationConfig.Height;

        Bounds bounds = GetComponent<SpriteRenderer>().sprite.bounds;
        float ratioScaleH = _height / bounds.size.y;
        float ratioScaleW = _width / bounds.size.x;
        transform.localScale = new Vector3(ratioScaleW, ratioScaleH, 1);

        float xSide = bounds.size.x * transform.localScale.x;
        float ySide = bounds.size.y * transform.localScale.y;
        
        Rect = new Rect(new Vector2(-xSide / 2, -ySide / 2), new Vector2(xSide, ySide));
    }

    void Update()
    {
        _player.Update(Time.deltaTime);
    }
}
