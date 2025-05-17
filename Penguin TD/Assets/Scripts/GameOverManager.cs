using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager main;
    [SerializeField] private GameObject gameOverPanel;

    void Start()
    {
        main = this;
    }
    public void GameOver()
    {
        gameOverPanel.SetActive(true);
    }

    public void Restart()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
