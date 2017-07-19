using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUIController : MonoBehaviour
{
    [SerializeField] private GamePanel _gamePanel;
    [SerializeField] private MenuPanel _menuPanel;
    private PlayerConfig _playerConfig;
    private float _reloadSceneTime = 3f;

    public void Initialize(PlayerConfig playerConfig)
    {
        _playerConfig = playerConfig;
        EventManager.OnPlayerDied += OnPlayerDied;
        EventManager.OnPlayerHit += OnPlayerHit;
        _gamePanel.CreateLifesPanel(_playerConfig.HealthPoint);
    }

    public void OnPlayerDied()
    {
        _gamePanel.DisplayGameOver();

        Observable.FromCoroutine(ReloadScene).Subscribe();
    }

    public IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(_reloadSceneTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 0f;
        _gamePanel.SetActive(true);
        _menuPanel.SetActive(false);
    }

    public void OnPlayerHit()
    {
        _gamePanel.RemoveHeart();
    }

    private void OnDestroy()
    {
        EventManager.OnPlayerDied -= OnPlayerDied;
        EventManager.OnPlayerHit -= OnPlayerHit;
    }
}
