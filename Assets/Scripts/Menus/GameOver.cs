using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void tryAgain()
    {
        SceneManager.LoadScene(1);
    }

    public void toMenu()
    {
        SceneManager.LoadScene(0);
    }
}
