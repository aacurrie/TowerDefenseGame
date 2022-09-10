using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathwayArrayControl : MonoBehaviour
{
    //creating list system
    public static List<GameObject> pathObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addPathwayToList(GameObject path){
        pathObjects.Add(path);
    }

    public void printList(){
        Debug.Log(pathObjects.Count);
    }

    public void printLocation(){
        for(int index = 0; index < pathObjects.Count; index++){
            Debug.Log(pathObjects[index].transform.position.y);
        }
    }
    public void changeEndPath(){
        for(int index = 0; index < pathObjects.Count-1; index++){
            pathObjects[index].tag = "Path";
            
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "open"){
            Destroy(other.gameObject);
        }
    }
    public void deleteGroundAtLocation(GameObject compare){
        for(int index = 0; index < GroundArrayControl.groundObjects.Count; index++){
            if((int)GroundArrayControl.groundObjects[index].transform.position.y == (int)compare.transform.position.y && (int)compare.transform.position.x == (int)GroundArrayControl.groundObjects[index].transform.position.x){
                GroundArrayControl.groundObjects.RemoveAt(index);
                //Debug.Log("Deleted Ground");
            }
        }
    }
}
