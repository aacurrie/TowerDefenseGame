using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private int damage;
    [SerializeField] private float shotDelay; //in seconds
    private float nextShot;

    [SerializeField] private GameObject currentTarget;

    public GameObject crossBow;
    public GameObject flameThrowerTube;
    public GameObject cannonBarrel;
    public Animator animator;

    public GameObject rangeDepiction;

    public static int totalTowers = 0;

    public static bool freeSpot;

    public static List<GameObject> enemies = new List<GameObject>();



    [SerializeField] public Text ClickLeftToSell;

    public GameObject top;

    public int targetingSystem = 0;
    bool textChange = false;
    public Text targetUpdate;

    public AudioSource towerSource;
    public AudioClip towerShoot;
    public AudioClip towerSell;
    public AudioClip placeTower;

    void Awake(){
        freeSpot = false;
        totalTowers++;
        currentTarget = null;
        PlaceSFX();
        //ClickLeftToSell = this.gameObject.GetComponent<Text>();
        //targetUpdate = GameObject.FindWithTag("TextChange").GetComponent<Text>();
        //targetUpdate.enabled = false;
        //targetingSystem = 0;
        Debug.Log("working");
    }   

    void Start(){
        nextShot = Time.time;
        //ClickLeftToSell.SetActive(false);
    }

    public void delayShootingStart(){
        nextShot = Time.time + .25f;
    }

    private void updateFirstEnemy(){ //determines first enemy in range
        foreach(GameObject enemy in enemies){
            float distanceToTarget = (transform.position - enemy.transform.position).magnitude;

            if(distanceToTarget < range && currentTarget == null){ //if no target yet or target has been destroyed, then it can change which to target first
                currentTarget = enemy;
            }
        }

        if((transform.position - currentTarget.transform.position).magnitude > range){
            currentTarget = null;
        }
    }

    private void updateLastEnemy(){ //determines closest to tower and within range
        GameObject currentNearestEnemy = null;
        float distance = Mathf.Infinity;

        foreach(GameObject enemy in enemies){
            float distanceToTarget = (transform.position - enemy.transform.position).magnitude;

            if(distanceToTarget < range){ //if no target yet or target has been destroyed, then it can change which to target first
                distance = distanceToTarget;
                currentNearestEnemy = enemy;
            }
        }

        if(distance <= range){
            currentTarget = currentNearestEnemy;
        }

        else{
            currentTarget = null;
        }
    }

    private void updateCloseEnemy(){ //determines closest to tower and within range
        GameObject currentNearestEnemy = null;
        float distance = Mathf.Infinity;

        foreach(GameObject enemy in enemies){
            float distanceToTarget = (transform.position - enemy.transform.position).magnitude;

            if(distanceToTarget < distance){ //if no target yet or target has been destroyed, then it can change which to target first
                distance = distanceToTarget;
                currentNearestEnemy = enemy;
            }
        }

        if(distance <= range){
            currentTarget = currentNearestEnemy;
        }

        else{
            currentTarget = null;
        }
    }

    private void updateFarEnemy(){ //determines closest to tower and within range
        GameObject currentNearestEnemy = null;
        float distance = 0;

        foreach(GameObject enemy in enemies){
            float distanceToTarget = (transform.position - enemy.transform.position).magnitude;

            if(distanceToTarget > distance){ //if no target yet or target has been destroyed, then it can change which to target first
                distance = distanceToTarget;
                currentNearestEnemy = enemy;
            }
        }

        if(distance <= range){
            currentTarget = currentNearestEnemy;
        }

        else{
            currentTarget = null;
        }
    }

    private void attack(){
        if(currentTarget != null){ //still alive
            FollowPath FollowPathScript = currentTarget.GetComponent<FollowPath>();
            FollowPathScript.takeDamage(damage);
            PlayShootSFX();

            if(cannonBarrel != null)
            {
                animator.SetTrigger("fire");
            }
            else
            {
                animator.SetBool("firing", true);
            }
        }
    }

    void FixedUpdate(){
        //ClickLeftToSell = this.gameObject.GetComponent<Text>();
        //updateFirstEnemy();
        
        switch (targetingSystem)
        {
            case 0: 
                updateFirstEnemy();
                break;
            case 1:
                updateLastEnemy();
                break;
            case 2:
                updateCloseEnemy();
                break;
            case 3:
                updateFarEnemy();
                break;
            default: 
                updateFirstEnemy();
                break;
        }

        /*
        if (textChange)
        {
           StartCoroutine(ShowTargetMessage(2));
        }*/

        //currentTarget does exist
        Debug.Log("testing 3");
        if(currentTarget!=null){
            float angle = Mathf.Atan2(currentTarget.transform.position.y-top.transform.position.y, currentTarget.transform.position.x-top.transform.position.x) * Mathf.Rad2Deg;
            top.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
            //Quaternion rotation = Quaternion.LookRotation(currentTarget.transform.position - top.transform.position, transform.TransformDirection(Vector3.up));
        }

        if(Time.time >= nextShot){
            if(currentTarget != null){
                nextShot = Time.time + shotDelay;
                Debug.Log("testing 2");
                attack();
                //nextShot = Time.time + shotDelay;
            }
        }
    }
    
        void Update()
        {
            if(currentTarget == null)
            {
                animator.SetBool("firing", false);
            }
        }


    /*IEnumerator ShowTargetMessage(float delay)
    {
        if (targetingSystem == 0) targetUpdate.text = "First";
        else if (targetingSystem == 1) targetUpdate.text = "Last";
        else if (targetingSystem == 2) targetUpdate.text = "Close";
        else if (targetingSystem == 3) targetUpdate.text = "Far";

        targetUpdate.enabled = true;
        yield return new WaitForSeconds(delay);
        targetUpdate.enabled = false;
    }*/

    private void OnMouseOver(){
        /*if(ClickLeftToSell != null){
            ClickLeftToSell.enabled = true;
        }*/

        rangeDepiction.GetComponent<SpriteRenderer>().enabled = true;
        if(Input.GetMouseButtonDown(0)){
            freeSpot = true;
            if(damage == 1){
                StatisticsTracker.gold+=160;
            }
            else if(damage == 10){
                StatisticsTracker.gold+=80;
            }
            else if(damage == 250){
                StatisticsTracker.gold+=280;
            }
            PlaySellSFX();
            Destroy(this.gameObject);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            targetingSystem = 0;
            textChange = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            targetingSystem = 1;
            textChange = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            targetingSystem = 2;
            textChange = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            targetingSystem = 3;
            textChange = true;
        }

       Debug.Log("testing 1"); 
    }

    private void OnMouseExit(){
        /*if(ClickLeftToSell != null){
            ClickLeftToSell.enabled = false;
        }*/
        rangeDepiction.GetComponent<SpriteRenderer>().enabled = false;
    }

    void PlayShootSFX()
    {
        towerSource.PlayOneShot(towerShoot);
    }

    void PlaySellSFX()
    {
        towerSource.PlayOneShot(towerSell);
    }

    void PlaceSFX()
    {
        towerSource.PlayOneShot(placeTower);
    }
}