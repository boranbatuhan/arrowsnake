using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{

 public void PlayAgain()
    {
        PlayerPrefs.SetFloat("recentLevel", 0);
        SceneManager.LoadScene(0);
    }
}
