using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour
{
    public Button PlayButton;
    public Button ExitButton;

    void Start()
    {
        Time.timeScale = 0f;
        PlayButton.onClick.AddListener(Play);
        ExitButton.onClick.AddListener(CloseApp);
    }

    private void Play()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }

    private void CloseApp()
    {
        Application.Quit();
    }
}
