using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chunkListing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("FormChunk").GetComponent<ChunkArrayControl>().addChunkToList(this.gameObject);
        GameObject.Find("FormChunk").GetComponent<ChunkArrayControl>().changeCurrentChunk();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
