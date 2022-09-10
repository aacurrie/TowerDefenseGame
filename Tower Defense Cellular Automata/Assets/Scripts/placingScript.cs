using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class placingScript : MonoBehaviour
{
    public GameObject CrossBow;
    public GameObject CrossBowCopy;

    public GameObject Turret;
    public GameObject TurretCopy;

    public GameObject Cannon;
    public GameObject CannonCopy;

    public static bool placingCrossBow;
    public static bool placingTurret;
    public static bool placingCannon;

    public GameObject copy;

    public Sprite grassOne;
    public Sprite grassTwo;
    public Sprite grassThree;

    public Sprite pathOne;

    public SpriteRenderer sr;

    void Start(){
        copy = null;
        placingCrossBow = false;
        placingTurret = false;
        placingCannon = false;
    }

    private void OnMouseOver(){
        if(Tower.freeSpot){
            this.gameObject.tag = "Open";
            Tower.freeSpot = false;
        }
        if(placingCrossBow || placingCannon || placingTurret){

        }
        if(Input.GetMouseButtonDown(0)){
            if(placingCrossBow){
                placeCrossBow();
            }
            else if(placingTurret){
                placeTurret();
            }
            else if(placingCannon){
                placeCannon();
            }
        }
    }

    public void createCrossBowCopy(){
        if(StatisticsTracker.gold - 100 >= 0 && !placingCrossBow){
            copy = Instantiate(CrossBowCopy, Vector3.zero, Quaternion.identity);
            placingCrossBow = true;
        }
    }

    private void placeCrossBow(){
        if(this.gameObject.tag == "Open" && StatisticsTracker.gold - 100 >= 0){
            Instantiate(CrossBow, transform.position, Quaternion.identity);
            this.gameObject.tag = "Taken";
            StatisticsTracker.gold-=100;
            placingCrossBow = false;
            copyScript.togglePlace();
        }
    }




    public void createTurretCopy(){
        if(StatisticsTracker.gold - 200 >= 0 && !placingTurret){
            copy = Instantiate(TurretCopy, Vector3.zero, Quaternion.identity);
            placingTurret = true;
        }
    }

    private void placeTurret(){
        if(this.gameObject.tag == "Open" && StatisticsTracker.gold - 200 >= 0){
            Instantiate(Turret, transform.position, Quaternion.identity);
            this.gameObject.tag = "Taken";
            StatisticsTracker.gold-=200;
            placingTurret = false;
            copyScript.togglePlace();
        }
    }

    public void createCannonCopy(){
        if(StatisticsTracker.gold - 350 >= 0 && !placingTurret){
            copy = Instantiate(CannonCopy, Vector3.zero, Quaternion.identity);
            placingCannon = true;
        }
    }

    private void placeCannon(){
        if(this.gameObject.tag == "Open" && StatisticsTracker.gold - 350 >= 0){
            Instantiate(Cannon, transform.position, Quaternion.identity);
            this.gameObject.tag = "Taken";
            StatisticsTracker.gold-=350;
            placingCannon = false;
            copyScript.togglePlace();
        }
    }

    void FixedUpdate(){
        if(Input.GetKeyDown("escape") && (placingCrossBow || placingCannon || placingTurret)){
            placingCannon = false;
            placingCrossBow = false;
            placingTurret = false;
        }

        if(this.gameObject.tag == "Path" && sr.sprite != grassOne){
            sr.sprite = pathOne;
        }
        else if((this.gameObject.tag == "Open" || this.gameObject.tag == "Taken") && (sr.sprite != grassOne || sr.sprite != grassTwo || sr.sprite != grassThree || sr.sprite != pathOne)){
            sr.sprite = grassOne;
        }
    }
}
