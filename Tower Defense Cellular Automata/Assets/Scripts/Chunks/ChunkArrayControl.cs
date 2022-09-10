using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkArrayControl : MonoBehaviour
{
    //creating list system
    public static List<GameObject> chunkObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addChunkToList(GameObject chunk){
        chunkObjects.Add(chunk);
    }

    public void printList(){
        Debug.Log(chunkObjects.Count);
    }

    public void printLocation(){
        for(int index = 0; index < chunkObjects.Count; index++){
            Debug.Log(chunkObjects[index].transform.position.y);
        }
    }

    public void changeCurrentChunk(){
        for(int index = 0; index < chunkObjects.Count-1; index++){
            chunkObjects[index].tag = "Chunk";
        }
    }
}
