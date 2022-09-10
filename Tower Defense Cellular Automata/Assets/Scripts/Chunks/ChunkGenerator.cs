using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour
{ //change chunk to a class that we modify
    
    public GameObject chunkTemplate;
    public GameObject currentChunk;
    public GameObject startChunk;
    public GameObject previousChunk;
    int seed;

    public int fillPercent = 15;

    public GameObject pathBlock;
    public GameObject grassBlock;

    //public GameObject previousChunk;

    public Vector3 endPathLocation;

    public bool chunkCreation = true;

    public GameObject newPath;
    public GameObject currentPath;
    public GameObject chunkReference;
    public static int totalInArea = 0;


    public static GameObject[] pathways;
    
    void Start() {
        currentChunk = startChunk;
        newPath = pathBlock;
        chunkCreation = true;

    }
    private void Update() 
    {
        pathways = GameObject.FindGameObjectsWithTag("Path");
        
        if(chunkCreation)
        {
            GameObject tmp = currentChunk;
            previousChunk = tmp;
            Debug.Log("Previous Chunk: " + previousChunk.GetComponent<Chunk>());
            currentChunk = Instantiate(chunkTemplate, new Vector3(0, previousChunk.transform.position.y, 0), Quaternion.identity);
            //Debug.Log("Current Chunk: " + currentChunk.GetComponent<Chunk>());
            if (!currentChunk.GetComponent<Chunk>().filled) FillMap();
            //DrawMap();            

            /*for (int y = 0; y < 5; y++)
            {
                SmoothPath();
            }*/

            totalInArea = 0;
            makePathway();
            //currentChunk.GetComponent<Chunk>().chunkMap[0, 6] = pathBlock;


            DrawMap();
            //makePathway(PathwayArrayControl.pathObjects[PathwayArrayControl.pathObjects.Count]);
            Debug.Log("Current Chunk: " + currentChunk.GetComponent<Chunk>());
            chunkCreation = false;
        }
    }
    

    void FillMap()
    {
        
        seed = (int)System.DateTime.Now.Ticks;
        System.Random numGen = new System.Random( seed );

        int maxX = previousChunk.GetComponent<Chunk>().width - 1;
        int maxY = previousChunk.GetComponent<Chunk>().height - 1;

        if (previousChunk.GetComponent<Chunk>().endX == 0) //left
        {
            currentChunk.GetComponent<Chunk>().startX = maxX;
            currentChunk.GetComponent<Chunk>().startY = previousChunk.GetComponent<Chunk>().endY;
            currentChunk.transform.position = new Vector3(previousChunk.transform.position.x - previousChunk.GetComponent<Chunk>().width, previousChunk.transform.position.y, 0);
        }
        else if (previousChunk.GetComponent<Chunk>().endX >= maxX - 1) // right
        {
            currentChunk.GetComponent<Chunk>().startX = 0;
            currentChunk.GetComponent<Chunk>().startY = previousChunk.GetComponent<Chunk>().endY;
            currentChunk.transform.position = new Vector3(previousChunk.transform.position.x + previousChunk.GetComponent<Chunk>().width, previousChunk.transform.position.y, 0);
        }

        else if (previousChunk.GetComponent<Chunk>().endY >= maxY) //top
        {
            currentChunk.GetComponent<Chunk>().startY = 0;
            currentChunk.GetComponent<Chunk>().startX = previousChunk.GetComponent<Chunk>().endX;
            currentChunk.transform.position = new Vector3(previousChunk.transform.position.x, previousChunk.transform.position.y + previousChunk.GetComponent<Chunk>().height, 0);
        }
/*else if (previousChunk.GetComponent<Chunk>().endY == 0) //bottom
        {
            currentChunk.GetComponent<Chunk>().startY = maxY;
            currentChunk.GetComponent<Chunk>().startX = previousChunk.GetComponent<Chunk>().endX;
            //not really a case tbh
        }*/
        
    
        //give the right block the start path tag (will remove after smoothing)
        //currentPath = Instantiate(pathBlock, new Vector3(currentChunk.GetComponent<Chunk>().startX, currentChunk.GetComponent<Chunk>().startY, 0), Quaternion.identity)
        currentChunk.GetComponent<Chunk>().chunkMap[currentChunk.GetComponent<Chunk>().startX, currentChunk.GetComponent<Chunk>().startY] = pathBlock;
        currentChunk.GetComponent<Chunk>().chunkMap[currentChunk.GetComponent<Chunk>().startX, currentChunk.GetComponent<Chunk>().startY].tag = "StartPath";

        

        

        for (int x = 0; x < currentChunk.GetComponent<Chunk>().width; x++)
        {
            for (int y = 0; y < currentChunk.GetComponent<Chunk>().height; y++)
            {
                if (currentChunk.GetComponent<Chunk>().chunkMap[x,y] == null)
                    //currentChunk.GetComponent<Chunk>().chunkMap[x,y] = (numGen.Next(0, 100) < fillPercent)? pathBlock: grassBlock;
                    currentChunk.GetComponent<Chunk>().chunkMap[x,y] = grassBlock;
            }
        }

    }

    void DrawMap()
    {
        for (int i = 0; i < currentChunk.GetComponent<Chunk>().width; i++)
        {
            for (int y = 0; y < currentChunk.GetComponent<Chunk>().height; y++)
            {
                chunkReference = Instantiate(currentChunk.GetComponent<Chunk>().chunkMap[i, y], new Vector2(i + currentChunk.transform.position.x, y + currentChunk.transform.position.y), Quaternion.identity);
            }
        }
    }

    private void generateMap(){
        for(int y = 0; y < currentChunk.GetComponent<Chunk>().height; y++){
            for(int x = 0; x < currentChunk.GetComponent<Chunk>().width; x++){
                currentChunk.GetComponent<Chunk>().chunkMap[y,x] = grassBlock;
            }
        }
    }

    void SmoothPath()
    {
        //start from end path location
        //check cardinal directions 2x out
        //if its okay, place path block
        //continue until it reaches a corner block

        for (int i = 0; i < currentChunk.GetComponent<Chunk>().width; i++)
        {
            for (int y = 0; y < currentChunk.GetComponent<Chunk>().height; y++)
            {
                int nearPath = GetSurroundingPathCount(i, y);
                //Debug.Log (nearPath);
                currentChunk.GetComponent<Chunk>().chunkMap[i,y] = (nearPath >= 2 )? pathBlock: grassBlock;


            }
        }

    }

    int GetSurroundingPathCount(int x, int y)
    {
        int pathCount = 0;

        for (int nX = x - 2; nX <= x + 2; nX++)
        {
            if (nX >= 0 && nX < currentChunk.GetComponent<Chunk>().width && nX != x && (currentChunk.GetComponent<Chunk>().chunkMap[nX, y].tag == "Path" || currentChunk.GetComponent<Chunk>().chunkMap[nX, y].tag == "StartPath" ))
            {
                pathCount++;
            }
        }

        for (int nY = y - 2; nY <= y + 2; nY++)
        {
            if (nY >= 0 && nY < currentChunk.GetComponent<Chunk>().height && nY != y && (currentChunk.GetComponent<Chunk>().chunkMap[x, nY].tag == "Path" || currentChunk.GetComponent<Chunk>().chunkMap[x, nY].tag == "StartPath" ))
            {
                pathCount++;
            }
        }

        return pathCount;

    }



    void makePathway(){
        int maxY = currentChunk.GetComponent<Chunk>().height;
        int maxX = currentChunk.GetComponent<Chunk>().width;


        //reset the location within the entire chunk
        currentChunk.GetComponent<Chunk>().endX = 0;
        currentChunk.GetComponent<Chunk>().endY = 0;

        GameObject currentPathway = PathwayArrayControl.pathObjects[PathwayArrayControl.pathObjects.Count-1];

        for(int col = 0; col < maxY; col++){
            for(int row = 0; row < maxX; row++){

                if(currentChunk.GetComponent<Chunk>().chunkMap[col, row].tag == "StartPath"){ //determines when end pathway is found

                    Debug.Log("Found StartPathway");


                    //increment totalInArea
                    totalInArea++;

                    //update values
                    currentChunk.GetComponent<Chunk>().endX = col;
                    currentChunk.GetComponent<Chunk>().endY = row;

                    if((col == 0 || row == 0 || row == maxY || col == maxX) && totalInArea != 1){
                        Debug.Log(col == 0);
                        Debug.Log(row == 0);
                        Debug.Log(row == maxY);
                        Debug.Log(col == maxX);

                        Debug.Log("Edge Detected");
                        totalInArea = 0;
                        return;
                    }

                    Debug.Log(currentChunk.GetComponent<Chunk>().endX + "    " + currentChunk.GetComponent<Chunk>().endY);

                    Debug.Log("Found Pathway End");

                    //for if the first block in the series
                    determineFirstBlock(currentChunk.GetComponent<Chunk>().endX, currentChunk.GetComponent<Chunk>().endY, totalInArea);

                    //for normal blocks
                    placeNewPath(true, true, true);

                    row = currentChunk.GetComponent<Chunk>().endY - 1;
                    col = currentChunk.GetComponent<Chunk>().endX - 1;

                    //do another iteration
                }

                Debug.Log("X coordinate = " + col + "    Y coordinate = " + row);
            }
        }
    }

    void placeNewPath(bool UpWorks, bool DownWorks, bool RightWorks){
        //seed setup for random number generator
        seed = (int)System.DateTime.Now.Ticks;
        System.Random numGen = new System.Random(seed);

        if(UpWorks && DownWorks && RightWorks){ //can go in any direction
            int place = numGen.Next(0, 3);

            if(place == 0){ //Move Up
                placeUp();
            }

            else if(place == 1){ //Move Down
                placeRight();
            }

            else{ //Move Right
                placeRight();
            }
        }
    }

    void placeUp(){
        
        Debug.Log("GOING UP");

        currentChunk.GetComponent<Chunk>().chunkMap[currentChunk.GetComponent<Chunk>().endX, currentChunk.GetComponent<Chunk>().endY].tag = "Path";
        currentChunk.GetComponent<Chunk>().chunkMap[currentChunk.GetComponent<Chunk>().endX, currentChunk.GetComponent<Chunk>().endY+1] = pathBlock;
        currentChunk.GetComponent<Chunk>().chunkMap[currentChunk.GetComponent<Chunk>().endX, currentChunk.GetComponent<Chunk>().endY+1].tag = "StartPath";

        //signify it went up
        currentChunk.GetComponent<Chunk>().endY+=1;
    }

    void placeDown(){
        
        Debug.Log("GOING DOWN");

        currentChunk.GetComponent<Chunk>().chunkMap[currentChunk.GetComponent<Chunk>().endX, currentChunk.GetComponent<Chunk>().endY].tag = "Path";
        currentChunk.GetComponent<Chunk>().chunkMap[currentChunk.GetComponent<Chunk>().endX, currentChunk.GetComponent<Chunk>().endY-1] = pathBlock;
        currentChunk.GetComponent<Chunk>().chunkMap[currentChunk.GetComponent<Chunk>().endX, currentChunk.GetComponent<Chunk>().endY-1].tag = "StartPath";

        //signify it went down
        currentChunk.GetComponent<Chunk>().endY-=1;
    }

    void placeRight(){
        
        Debug.Log("GOING RIGHT");

        currentChunk.GetComponent<Chunk>().chunkMap[currentChunk.GetComponent<Chunk>().endX, currentChunk.GetComponent<Chunk>().endY].tag = "Path";
        currentChunk.GetComponent<Chunk>().chunkMap[currentChunk.GetComponent<Chunk>().endX+1, currentChunk.GetComponent<Chunk>().endY] = pathBlock;
        currentChunk.GetComponent<Chunk>().chunkMap[currentChunk.GetComponent<Chunk>().endX+1, currentChunk.GetComponent<Chunk>().endY].tag = "StartPath";

        //signify it went down
        currentChunk.GetComponent<Chunk>().endY+=1;
    }

    void determineFirstBlock(int x, int y, int totalInArea){
        if(totalInArea == 1){
            if(y == 0){ //forced to go up
                currentChunk.GetComponent<Chunk>().chunkMap[x, y].tag = "Path";
                currentChunk.GetComponent<Chunk>().chunkMap[x, y+1] = pathBlock;
                currentChunk.GetComponent<Chunk>().chunkMap[x, y+1].tag = "StartPath";
                
                currentChunk.GetComponent<Chunk>().endX = x;
                currentChunk.GetComponent<Chunk>().endY =  y + 1;;
            }

            else if(x == 0){ //forced to go right
                currentChunk.GetComponent<Chunk>().chunkMap[x, y].tag = "Path";
                currentChunk.GetComponent<Chunk>().chunkMap[x+1, y] = pathBlock;
                currentChunk.GetComponent<Chunk>().chunkMap[x+1, y].tag = "StartPath";

                currentChunk.GetComponent<Chunk>().endX = x + 1;
                currentChunk.GetComponent<Chunk>().endY =  y;
            }

            else if(y == currentChunk.GetComponent<Chunk>().height - 1){ //forced to go down
                currentChunk.GetComponent<Chunk>().chunkMap[x, y].tag = "Path";
                currentChunk.GetComponent<Chunk>().chunkMap[x, y-1] = pathBlock;
                currentChunk.GetComponent<Chunk>().chunkMap[x, y-1].tag = "StartPath";

                currentChunk.GetComponent<Chunk>().endX = x;
                currentChunk.GetComponent<Chunk>().endY =  y-1;
            }
            Debug.Log("Found pathway at: " + x + ", " + y);
            //makePathway();
        }
    }

    public bool determineEdge(int x, int y){
        if(y == 0 || y == currentChunk.GetComponent<Chunk>().height - 1 || x == currentChunk.GetComponent<Chunk>().width - 1 || x == 0) //on edge
        {
            return true;
        }
        return false;
    }
}
