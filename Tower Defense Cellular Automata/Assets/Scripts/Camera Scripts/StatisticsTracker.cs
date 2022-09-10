using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StatisticsTracker : MonoBehaviour
{
    public Text livesTracker;
    public Text goldTracker;
    public Text waveNumber;

    public static int lives;
    public static int gold;
    public static int wave;

    public static bool isInWave;

    public static GameObject[] enemies;

    public GameObject EnemyPrefab;

    public float previousSpawn;

    public Tower TScript;

    public static int totalEnemiesLeft;

    public AudioSource liveLoss;


    public static List<GameObject> path = new List<GameObject>();
    public static List<List<GameObject>> ground = new List<List<GameObject>>();

    // Start is called before the first frame update
    void Start()
    {
        //GameObject.Find("TextPopUpSell").SetActive(false);
        totalEnemiesLeft = 0;
        previousSpawn = Time.time;

        lives = 50;
        gold = 300;
        wave = 1;

        isInWave = false;
    }

    void FixedUpdate(){
        if(Input.GetKeyDown("l")){
            for(int lp = 0; lp < 20; lp++){
                GenerateMap.chunkCreation = true;
            }
        }
        if(lives <= 0){
            Debug.Log("Game Over"); //load Game Over
            SceneManager.LoadScene("Game Over");
        }

        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if(enemies.Length == 0 && isInWave && totalEnemiesLeft == 0){
            isInWave = false;
            GenerateMap.chunkCreation = true;
            endLevelGold();
            wave++;
        }

        livesTracker.text = "Lives: " + lives.ToString();
        goldTracker.text = "Gold: " + gold.ToString();
        waveNumber.text = "Wave " + wave.ToString();

        if(lives <= 0){
            SceneManager.LoadScene("Game Over");
        }


        
    }

    public void incrementGoldNormal(){
        gold+=25;
    }

    public void incrementGoldFast(){
        gold+=10;
    }

    public void incrementGoldLarge(){
        gold+=100;
    }

     public void endLevelGold(){
        gold+=(19 + wave);
    }



    public void livesIncrementNormal(){
        lives-=5;
        PlaySFX();
    }

    public void livesIncrementFast(){
        lives-=1;
        PlaySFX();
    }

    public void livesIncrementLarge(){
        lives-=20;
        PlaySFX();
    }

    public void endGame(){
        lives = 0;
    }




    public void startWave(){
        if(isInWave){
            return;
        }
        if(wave >= 13){
            FollowPath.healthBonus+=50;
        }
        totalEnemiesLeft = wave;
        //TScript.delayShootingStart();

        StartCoroutine(startWaveSpawn());
        isInWave = true;
        //put time gap here
    }
    IEnumerator startWaveSpawn(){
        for(int lp = 0; lp < wave; lp++){
            Instantiate(EnemyPrefab, new Vector2(0, -1000), Quaternion.identity); //spawns enemy at startLocation
            yield return new WaitForSeconds(1);
        }
    }

    void PlaySFX()
    {
        liveLoss.Play();
        liveLoss.Stop();
    }
}
