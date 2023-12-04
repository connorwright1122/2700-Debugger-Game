using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LogicScript : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject gameWinScreen;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0f;
        player.GetComponent<PlayerController2>().frozen = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void GameWin()
    {
        gameWinScreen.SetActive(true);
        Time.timeScale = 0f;
        player.GetComponent<PlayerController2>().frozen = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
