using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour
{
    public Transform LifesContainer;
    public Text ScoreText;
    public GameObject GameOverText;

    private List<GameObject> _lifesObject;

    private void Start()
    {
        EventManager.OnScoreUp += SetScore;
    }

    public void CreateLifesPanel(int hp)
    {
        _lifesObject = new List<GameObject>();
        for (int i = 0; i < hp; i++)
        {
            GameObject heartObject = Instantiate(Resources.Load("Prefabs/HealthPoint") as GameObject, LifesContainer);
            heartObject.transform.localScale = Vector3.one;
            _lifesObject.Add(heartObject);
        }
    }

    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }

    public void RemoveHeart()
    {
        _lifesObject.First().SetActive(false);
        _lifesObject.RemoveAt(0);
    }

    public void SetScore(int score)
    {
        ScoreText.text = score.ToString();
    }

    private void OnDestroy()
    {
        EventManager.OnScoreUp -= SetScore;
    }

    public void DisplayGameOver()
    {
        GameOverText.SetActive(true);
        LifesContainer.gameObject.SetActive(false);
    }
}
