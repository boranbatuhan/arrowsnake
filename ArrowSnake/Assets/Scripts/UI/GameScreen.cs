using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScreen : MonoBehaviour
{
    [SerializeField] RectTransform levelPass,levelFail;
    EnemyControler enemyControler;
    PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    public void GameFailed()
    {
        Time.timeScale = 0f;
        playerMovement.SetIsGamePlay(false);
        levelPass.gameObject.SetActive(false);
        levelFail.gameObject.SetActive(true);
    }
    public void GamePassed()
    {
        Time.timeScale = 1f;
        playerMovement.SetIsGamePlay(false);
        levelPass.gameObject.SetActive(true);
        levelFail.gameObject.SetActive(false);
    }


    public void RestartLevel()
    {
        Time.timeScale = 1f;
        enemyControler = GameObject.FindAnyObjectByType<EnemyControler>();
        enemyControler.ResetBuffCount();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    public void NextLevel() 
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetFloat("recentLevel", SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
