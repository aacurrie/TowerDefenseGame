using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{

    [SerializeField] private int chunkWidth;
    [SerializeField] private int chunkHeight;

    //blocks
    public GameObject chunk;

    //lists
    public List<GameObject> pathwayTiles = new List<GameObject>();
    public List<GameObject> groundTiles = new List<GameObject>();

    //lists of lists
    public static List<List<GameObject>> ground = new List<List<GameObject>>();

    public static List<GameObject> pathFinding = new List<GameObject>();
    public static List<List<GameObject>> path = new List<List<GameObject>>();

    //boolean creation
    public static bool chunkCreation;
    private bool reachedX = false;
    private bool reachedY = false;

    //referencePoints
    public GameObject startOfChunk;
    public GameObject endOfChunk;

    //locating
    private GameObject currentPath;
    private int index;
    private int nextIndex;

    //coloring
    public Color pathColor;

    //addingLocation
    public static int offSetUp = 7;
    public static int offSetLR = 0;
    //public static int TotalChunksCreated = 0;
    public static int referencePrevChunk = 3;

    // Start is called before the first frame update
    void Start()
    {
        //TotalChunksCreated = 0;
        chunkWidth = 7;
        chunkHeight = 7;

        //generateMap();
        chunkCreation = true;
    }

    public static void changeChunkCreation(){
        chunkCreation  = true;
    }

    void FixedUpdate(){        
        if(chunkCreation)
        {
            Instantiate(chunk, new Vector2(offSetLR, offSetUp), Quaternion.identity);
            chunkCreation = false;
        }
    }

    public static void incrementOffSetUp(){
        offSetUp+=7;
        Debug.Log("Next chunk: up");
    }

    public static void incrementOffSetRight(){
        offSetLR+=7;
    }

    public static void incrementOffSetLeft(){
        offSetLR-=7;
    }

    


}
