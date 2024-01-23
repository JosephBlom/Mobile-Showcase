
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] Canvas pauseCanvas;

    private void Start()
    {
        pauseCanvas.enabled = false;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseCanvas.enabled = false;
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pauseCanvas.enabled = true;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
