using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginScreen : MonoBehaviour
{
    int recentLevel;
    private void Start()
    {
        recentLevel = PlayerPrefs.GetInt("recentLevel");
        if (recentLevel == 0) recentLevel = 1;
    }
    public void StartGame()
    {
        SceneManager.LoadScene(recentLevel);
    }
}
