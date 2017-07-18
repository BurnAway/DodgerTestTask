using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    [HideInInspector]
    public Player Player { get; private set; }

    public GameObject CameraController;
    public Rect Rect;

    private LevelConfig _levelConfig;
    private float _width;
    private float _height;

    private Enemy enemy;
    private Enemy enemy1;

    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _levelConfig = new LevelConfig();

        InitializeLocation();

        Player = new Player();
        Player.Initialize(_levelConfig.PlayerConfig);

        enemy = new Enemy(this);
        enemy.Initialize(_levelConfig.EnemyConfig);
        enemy.Transform.position = new Vector3(1f, -1f);

        enemy1 = new Enemy(this);
        enemy1.Initialize(_levelConfig.EnemyConfig);
        enemy1.Transform.position = new Vector3(-1f, -1f);

        CameraController cameraController = CameraController.GetComponent<CameraController>();
        cameraController.Initialize(this, Player.View.transform, _levelConfig.CameraConfig);
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
        Player.Update(Time.deltaTime);
        enemy.Update(Time.deltaTime);
        enemy1.Update(Time.deltaTime);
    }
}
