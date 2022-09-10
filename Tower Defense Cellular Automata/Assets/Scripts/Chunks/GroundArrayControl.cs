using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundArrayControl : MonoBehaviour
{
    //creating list system
    public static List<GameObject> groundObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addGroundToList(GameObject ground){
        groundObjects.Add(ground);
    }

    public void printList(){
        Debug.Log(groundObjects.Count);
    }

    public void printLocation(){
        for(int index = 0; index < groundObjects.Count; index++){
            Debug.Log(groundObjects[index].transform.position.y);
        }
    }
}
