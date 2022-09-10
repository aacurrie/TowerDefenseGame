using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneScript : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioSource buttonSound;
    void Start()
    {
        //SceneManager.LoadScene("Title");
        buttonSound = GameObject.FindWithTag("Audio").GetComponent<AudioSource>();
    }

    void FixedUpdate(){
        if(Input.GetKeyDown("escape")){
            QuitGame();
        }
    }
    public static void LoadGameOver(){
        SceneManager.LoadScene("Game Over");
    }

    public static void LoadTitle(){
        SceneManager.LoadScene("Title");
    }

    public static void LoadInstructions(){
        SceneManager.LoadScene("Instructions");
    }

    public static void LoadGame(){
        SceneManager.LoadScene("Chunk Generation");
    }

    public static void LoadTowerGuide(){
        SceneManager.LoadScene("Tower Guide");
    }

    public static void LoadTowerGuide2(){
        SceneManager.LoadScene("Tower Guide Page 2");
    }

    public static void LoadEnemyGuide(){
        SceneManager.LoadScene("Enemy Guide");
    }

    public static void QuitGame(){
        Application.Quit();
    }

    public void ButtonPress()
    {
        buttonSound.Play();
    }
}
