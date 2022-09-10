using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToArray : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        checkObject();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*if(Input.GetKeyDown("escape")){
            GameObject.Find("FormChunk").GetComponent<PathwayArrayControl>().printList();
            GameObject.Find("FormChunk").GetComponent<GroundArrayControl>().printList();
            GameObject.Find("FormChunk").GetComponent<ChunkArrayControl>().printList();
        }*/
    }

    void checkObject(){
        /*if(this.gameObject == null && (this.gameObject.tag != "CurrentChunk" ||  this.gameObject.tag != "Chunk")){
            return;
        }*/
        if(this.gameObject.tag == "EndPath" || this.gameObject.tag == "Path" || this.gameObject.tag == "StartPath"){
            GameObject.Find("FormChunk").GetComponent<PathwayArrayControl>().addPathwayToList(this.gameObject);
            GameObject.Find("FormChunk").GetComponent<PathwayArrayControl>().changeEndPath();
            GameObject.Find("FormChunk").GetComponent<PathwayArrayControl>().deleteGroundAtLocation(this.gameObject); //evetually have it only compare the final 49
        }
        if(this.gameObject.tag == "CurrentChunk" ||  this.gameObject.tag == "Chunk"){
            GameObject.Find("FormChunk").GetComponent<ChunkArrayControl>().addChunkToList(this.gameObject);
            GameObject.Find("FormChunk").GetComponent<ChunkArrayControl>().changeCurrentChunk();
        }
        else{
            GameObject.Find("FormChunk").GetComponent<GroundArrayControl>().addGroundToList(this.gameObject);
        }
    }
}
