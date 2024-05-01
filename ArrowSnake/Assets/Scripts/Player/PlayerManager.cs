using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    SnakeMove snakeMove;
    GameScreen gameScreen;
    int bodyLife;
    private void Start()
    {
        snakeMove= FindAnyObjectByType<SnakeMove>();
        gameScreen = FindAnyObjectByType<GameScreen>();
    }
    private void Update()
    {
        bodyLife = snakeMove.GetBodyCount();
        if(bodyLife<=0)
        {
            gameScreen.GamePassed();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Head"))
        {
            gameScreen.GameFailed();
        }
    }
}
