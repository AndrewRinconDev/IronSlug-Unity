using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Quaternion = UnityEngine.Quaternion;

public class ChangeLevel : MonoBehaviour
{
    public int level;
    public GameObject audioAparicion;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("ColliderNextLevel").GetComponent<Animator>().SetBool("go", false);
            //DontDestroyOnLoad(GameObject.FindGameObjectWithTag("MainCamera"));
            DontDestroyOnLoad(GameObject.FindGameObjectWithTag("CanvasGameOver"));
            //GameObject.FindGameObjectWithTag("ButtonGameOver").gameObject.SetActive(true);
            DontDestroyOnLoad(GameObject.FindGameObjectWithTag("CanvasPause"));
            DontDestroyOnLoad(GameObject.FindGameObjectWithTag("CanvasLive"));
            DontDestroyOnLoad(collision.gameObject);
            SceneManager.LoadScene(level);
            collision.gameObject.GetComponent<TimeManager>().addLevelTime();
            collision.gameObject.transform.position = new UnityEngine.Vector3(0, 1, -9);
            emitirSonido(audioAparicion);
        }
    }

    public void emitirSonido(GameObject prefab)
    {
        Instantiate(prefab, transform.position, Quaternion.identity);
    }
}
