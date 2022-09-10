using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathListing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        this.gameObject.tag = "StartPath";
        //GameObject.Find("FormChunk").GetComponent<PathwayArrayControl>().addPathwayToList(this.gameObject);
        //GameObject.Find("FormChunk").GetComponent<PathwayArrayControl>().changeEndPath();
        //GameObject.Find("FormChunk").GetComponent<PathwayArrayControl>().changeCurrentChunk();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
