using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class Location : MonoBehaviour
{
    public Player Player { get; private set; }
    public Rect Rect;

    [SerializeField] private GameObject _spawnPoints;
    [SerializeField] private GameObject _cameraController;
    [SerializeField] private Floor _floor;
    [SerializeField] private GUIController _gui;

    private IDisposable _spawnController;
    private LevelConfig _levelConfig;
    private EnemyPool _enemyPool;
    private List<IEnemy> _enemies;
    private float _width;
    private float _height;
    private int _score;

    void Start()
    {
        Initialize();
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

    private void Initialize()
    {
        _score = 0;

        _levelConfig = new LevelConfig();

        InitializeLocation();

        _floor.Initialize(OnEnemyLeftArea);

        Player = new Player();
        Player.Initialize(_levelConfig.PlayerConfig, _levelConfig.ProjectileConfig);
        
        CameraController cameraController = _cameraController.GetComponent<CameraController>();
        cameraController.Initialize(this, Player.View.transform, _levelConfig.CameraConfig);

        _enemyPool = new EnemyPool(this, _levelConfig.EnemyConfig, _spawnPoints);
        _enemyPool.Initialize();

        EventManager.OnEnemyDied += OnEnemyDied;

        _gui.Initialize(_levelConfig.PlayerConfig);
        _spawnController = Observable.FromCoroutine(SpawnEnemy).Subscribe();
    }

    private void OnEnemyLeftArea(IEnemy enemy)
    {
        _enemyPool.DisposeEnemy(enemy);
    }
    
    private void OnEnemyDied(IEnemy enemy)
    {
        _enemyPool.DisposeEnemy(enemy);
        ScoreUp();
    }

    private void ScoreUp()
    {
        _score++;
        EventManager.OnScoreUp(_score);
    }

    void Update()
    {
        foreach (IEnemy enemy in _enemyPool.ActiveEnemy)
        {
            enemy.Tick();
        }
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(_levelConfig.LocationConfig.EnemySpawnDelay);
            _enemyPool.TakeEnemy();
        }
    }
    
    private void OnDestroy()
    {
        EventManager.OnEnemyDied -= OnEnemyDied;
        _spawnController.Dispose();
    }
}
