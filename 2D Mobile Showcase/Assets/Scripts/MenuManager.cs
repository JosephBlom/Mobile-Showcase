using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void enterDungeon()
    {
        SceneManager.LoadScene("Dungeon");
    }

    public void enterForest()
    {
        SceneManager.LoadScene("Forest");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
