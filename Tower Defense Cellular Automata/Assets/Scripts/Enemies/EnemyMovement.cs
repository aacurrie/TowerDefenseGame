using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public static List<GameObject> path = new List<GameObject>();

    public static GameObject[] currentPathwaysOnScreen;

    // Start is called before the first frame update
    void Start()
    {
        currentPathwaysOnScreen = GameObject.FindGameObjectsWithTag("Path");
        foreach(GameObject pathway in currentPathwaysOnScreen){
            path.Add(pathway);
        }

        currentPathwaysOnScreen = GameObject.FindGameObjectsWithTag("StartPath");

        foreach(GameObject pathway in currentPathwaysOnScreen){
            path.Add(pathway);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        currentPathwaysOnScreen = GameObject.FindGameObjectsWithTag("Path");
        foreach(GameObject pathway in currentPathwaysOnScreen){
            if(!path.Contains(pathway)){
                path.Add(pathway);
            }
        }

        currentPathwaysOnScreen = GameObject.FindGameObjectsWithTag("StartPath");

        foreach(GameObject pathway in currentPathwaysOnScreen){
            if(!path.Contains(pathway)){
                path.Add(pathway);
            }
        }

        if(Input.GetKeyDown(KeyCode.Space)){
            foreach(GameObject pathway in path){
                Debug.Log("X: " + pathway.transform.position.x + "     Y: " + pathway.transform.position.y);
            }
        }
    }
}
