using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public bool alive;
    public bool isTeleported;
    public bool moving;

    public float movementSpeed;

    public int index;
    public int maxIndex;

    public int health;
    public int maxHealth;
    public static int healthBonus = 0;

    public Vector2 destination;

    public SpriteRenderer spriteRenderer;

    public Sprite Goblin;
    public Sprite Skeleton;
    public Sprite Golem;

    private float previousLocationX;
    private float currentLocationX;

    private float invTime;

    public HealthBarBehaviour healthBar;

    void Awake(){
        
        invTime = Time.time + .1f;

        Tower.enemies.Add(this.gameObject); //add the enemy to the list
        health = 100 + healthBonus;

        index = 0;
        maxIndex = 0;
        movementSpeed = 1;
        alive = true;
        isTeleported = true;
        moving = false;

        determineType();
        previousLocationX = this.gameObject.transform.position.x;
    }

    // Start is called before the first frame update
    /*void Start()
    {
        health = 100 + healthBonus;

        index = 0;
        maxIndex = 0;
        movementSpeed = 1;
        alive = true;
        isTeleported = true;
        moving = false;

        determineType();
        previousLocationX = this.gameObject.transform.position.x;
    }*/

    public static void incrementHealthBonus(){
        if(StatisticsTracker.wave >= 20){
            healthBonus = (5 * StatisticsTracker.wave);
        }
    }

    void determineType(){
        if(StatisticsTracker.wave <= 5){
            movementSpeed = 1;
            health = 50 + healthBonus;
            spriteRenderer.sprite = Skeleton;
        }

        else if(StatisticsTracker.wave <= 10){
            int spawn = Random.Range(0, 10);
            if(spawn < 7){
                movementSpeed = 1;
                health = 50 + healthBonus;
                spriteRenderer.sprite = Skeleton;
            }
            else{
                movementSpeed = 2f;
                health = 20 + healthBonus;
                spriteRenderer.sprite = Goblin;
            }
        }

        else{
            int spawn = Random.Range(0, 100);
            if(spawn < 10){
                movementSpeed = .5f;
                health = 800 + healthBonus;
                spriteRenderer.sprite = Golem;
            }
            else if(spawn < 40){
                movementSpeed = 2f;
                health = 20 + healthBonus;
                spriteRenderer.sprite = Goblin;
            }
            else{
                movementSpeed = 1;
                health = 50 + healthBonus;
                spriteRenderer.sprite = Skeleton;
            }
        }

        maxHealth = health;
        healthBar.SetHealth(health, health);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if(maxIndex != EnemyMovement.path.Count - 1){
            maxIndex = EnemyMovement.path.Count - 1;
            index = EnemyMovement.path.Count - 1;
        }

        if(isTeleported && EnemyMovement.path.Count > 3){
            this.gameObject.transform.position = EnemyMovement.path[index].transform.position;
            moving = true;
            isTeleported = false;
        }

        if(health <= 0){
            alive = false;
        }

        if(moving && alive){
            Vector2 V2 = EnemyMovement.path[index].transform.position;
            this.transform.position = Vector2.MoveTowards(this.transform.position, V2, Time.deltaTime * movementSpeed * 2f);

            if(Vector2.Distance(this.gameObject.transform.position, EnemyMovement.path[index].transform.position) < .05f){
                index--;
            }

            if(Vector2.Distance(this.gameObject.transform.position, EnemyMovement.path[0].transform.position) < .05f){
                if(this.movementSpeed == .5f){
                    StatisticsTracker.lives-=20;
                }
                else if(this.movementSpeed == 1f){
                    StatisticsTracker.lives-=5;
                }
                else if(this.movementSpeed == 2f){
                    StatisticsTracker.lives-=3;
                }
                alive = false;
            }

            currentLocationX = this.gameObject.transform.position.x;
            if(previousLocationX > currentLocationX){
                this.transform.localScale = new Vector3(-1, 1, 1);
            }
            else if(previousLocationX < currentLocationX){
                this.transform.localScale = new Vector3(1, 1, 1);
            }
            previousLocationX = currentLocationX;
        }

        if(!alive){
            Tower.enemies.Remove(this.gameObject);
            StatisticsTracker.totalEnemiesLeft--;
            if(this.movementSpeed == .5f){
                StatisticsTracker.gold+=30;
            }
            else if(this.movementSpeed == 1f){
                StatisticsTracker.gold+=5;
            }
            else if(this.movementSpeed == 2f){
                StatisticsTracker.gold+=10;
            }
            Destroy(this.gameObject);
        }
    }

    public void movement(int speed, int index){
        if(index == 0){
            Destroy(this.gameObject);
            return;
        }

        Vector2 V2 = EnemyMovement.path[index - 3].transform.position;
        this.transform.position = Vector2.MoveTowards(this.transform.position, V2, Time.deltaTime * (float)movementSpeed * .25f);
        //while(Vector2.Distance(this.gameObject.transform.position, EnemyMovement.path[index - 1].transform.position) > .05){
            //this.gameObject.transform.position.MoveTowards(transform.position, EnemyMovement.path[index - 1].transform.position, Time.deltaTime * .25f);
        //}
        Debug.Log(index - 1);
        //movement(speed, index - 1);
    }

    public void takeDamage(int damage){
        if(invTime < Time.time){
            health-=damage;
            healthBar.SetHealth(health, maxHealth);
        }
    }
}
