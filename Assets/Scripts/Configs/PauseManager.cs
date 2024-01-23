using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseManager : MonoBehaviour {
    
    Canvas canvas;
    GameObject gameOver;
    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
        gameOver = GameObject.FindGameObjectWithTag("CanvasGameOver");
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!gameOver.GetComponent<GameOver>().isGameOver)
            {
                Pause();
            }
        }
    }
    
    public void Pause()
    {
        canvas.enabled = !canvas.enabled;
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }

    public void Restart()
    {
        //Restart
    }

    public void Quit()
    {
        #if UNITY_EDITOR 
        EditorApplication.isPlaying = false;
        #else 
        Application.Quit();
        #endif
    }

    public void Level1()
    {
        //DontDestroyOnLoad(GameObject.FindGameObjectWithTag("MainCamera"));
        //DontDestroyOnLoad(GameObject.FindGameObjectWithTag("CanvasGameOver"));
        //DontDestroyOnLoad(GameObject.FindGameObjectWithTag("CanvasPause"));
        //DontDestroyOnLoad(GameObject.FindGameObjectWithTag("CanvasLive"));
        GameObject objectPlayer = GameObject.FindGameObjectWithTag("Player");
        if (objectPlayer != null)
        {
            //Destroy(GameObject.FindGameObjectWithTag("MainCamera"));
            Destroy(GameObject.FindGameObjectWithTag("CanvasGameOver"));
            Destroy(GameObject.FindGameObjectWithTag("CanvasGameOver"));
            Destroy(GameObject.FindGameObjectWithTag("CanvasPause"));           
            Destroy(GameObject.FindGameObjectWithTag("CanvasLive"));
            Destroy(GameObject.FindGameObjectWithTag("Player"));
        }
        SceneManager.LoadScene("Level1");
    }

    public void Level2()
    {
        //DontDestroyOnLoad(GameObject.FindGameObjectWithTag("MainCamera"));
        //DontDestroyOnLoad(GameObject.FindGameObjectWithTag("CanvasGameOver"));
        //DontDestroyOnLoad(GameObject.FindGameObjectWithTag("CanvasPause"));
        //DontDestroyOnLoad(GameObject.FindGameObjectWithTag("CanvasLive"));
        //DontDestroyOnLoad(GameObject.FindGameObjectWithTag("Player"));
        SceneManager.LoadScene("Level2");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
