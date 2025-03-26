using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public GameObject menuUI;
    private bool isPaused = false;
    private PlayerController player;

    void Start()
    {
        menuUI.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    void ToggleMenu()
    {
        isPaused = !isPaused;
        menuUI.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void ToggleImmortality()
    {
        player.ToggleImmortality();
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f; // Asegura que el tiempo vuelva a la normalidad
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }

    public void ResumeGame()
    {
        ToggleMenu();
    }
}
