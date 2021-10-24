using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScenceManager : MonoBehaviour
{
   public void GoToPlayground()
    {
        SceneManager.LoadScene("playground");
    }

    public void ExitGame ()
    {
        Application.Quit();
    }
}
