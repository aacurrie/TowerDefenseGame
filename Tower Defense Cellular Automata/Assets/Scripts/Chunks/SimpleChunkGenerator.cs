using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleChunkGenerator : MonoBehaviour
{ 
    public GameObject chunkTemplate;
    public SimpleChunk currentChunk;
    public SimpleChunk startChunk;
    public SimpleChunk previousChunk;
    int seed;

    public int fillPercent = 15;

    public bool chunkCreation = true;

    
    void Start() 
    {
        currentChunk = startChunk;
    }
    private void Update() 
    {
        
        if(chunkCreation)
        {
            previousChunk = currentChunk;
            currentChunk = Instantiate(chunkTemplate, Vector3.zero, Quaternion.identity).GetComponent<SimpleChunk>();
            if (!currentChunk.filled) FillMap();
            
            for (int i = 0; i < 5; i++) SmoothPath();
            
            currentChunk.DrawChunk();
            
            chunkCreation = false;
        }
    }
    

    void FillMap()
    {
        
        seed = (int)System.DateTime.Now.Ticks;
        System.Random numGen = new System.Random( seed );

        int maxX = previousChunk.width - 1;
        int maxY = previousChunk.height - 1;

        if (previousChunk.endX == 0) //left
        {
            currentChunk.startX = maxX;
            currentChunk.startY = previousChunk.endY;
            //currentChunk.transform.position = new Vector3(previousChunk.transform.position.x - currentChunk.width, previousChunk.transform.position.y, 0);
        }
        else if (previousChunk.endX == maxX) // right
        {
            currentChunk.startX = 0;
            currentChunk.startY = previousChunk.endY;
            //currentChunk.transform.position = new Vector3(previousChunk.transform.position.x + currentChunk.GetComponent<Chunk>().width, previousChunk.transform.position.y, 0);
        }

        else if (previousChunk.endY == 0) //top
        {
            currentChunk.startY = 0;
            currentChunk.startX = currentChunk.endX;
            //currentChunk.transform.position = new Vector3(previousChunk.transform.position.x, previousChunk.transform.position.y + currentChunk.GetComponent<Chunk>().height, 0);
        }

        

        for (int x = 0; x < currentChunk.width; x++)
        {
            for (int y = 0; y < currentChunk.height; y++)
            {
                currentChunk.chunkMap[x,y] = (numGen.Next(0, 100) < fillPercent)? (int)SimpleChunk.blocks.pathBlock: (int)SimpleChunk.blocks.grassBlock;
            }
        }

        //start path here
        currentChunk.chunkMap[currentChunk.startX, currentChunk.startY] = (int)SimpleChunk.blocks.startPath;

    }

    void SmoothPath()
    {
        //start from end path location
        //check cardinal directions 2x out
        //if its okay, place path block
        //continue until it reaches a corner block

        for (int i = 0; i < currentChunk.width; i++)
        {
            for (int y = 0; y < currentChunk.height; y++)
            {
                int nearPath = GetSurroundingPathCount(i, y);
                //Debug.Log (nearPath);
                
                if (i != currentChunk.startX && y != currentChunk.startY)
                {
                    if (nearPath >= 1 && nearPath <= 4 && (currentChunk.chunkMap[i, y] >= 1 && currentChunk.chunkMap[i, y] <= 3))
                    {
                        currentChunk.chunkMap[i, y] = (int)SimpleChunk.blocks.pathBlock;
                    }
                    else if (nearPath == 3 && currentChunk.chunkMap[i, y] == 0)
                    {
                        currentChunk.chunkMap[i, y] = (int)SimpleChunk.blocks.pathBlock;
                    }

                    else if (nearPath == 5)
                    {
                        currentChunk.chunkMap[i, y] = (int)SimpleChunk.blocks.grassBlock;
                    }
                }


            }
        }

    }

    private int GetSurroundingPathCount(int x, int y)
    {
        int pathCount = 0;

        for (int nX = x - 1; nX <= x + 1; nX++)
        {
            if (nX >= 0 && nX < currentChunk.width && nX != x)
            {
                if (currentChunk.chunkMap[nX, y] >= 1 && currentChunk.chunkMap[nX, y] <= 3)
                {
                    pathCount++;
                }
            }
            
        }

        for (int nY = y - 1; nY < y + 1; nY++)
        {
            if (nY >= 0 && nY < currentChunk.height && nY != y)
            {
                if (currentChunk.chunkMap[x, nY] >= 1 && currentChunk.chunkMap[x, nY] <= 3)
                {
                    pathCount++;
                }
            }
            
        }

        return pathCount;

    }

    /*bool FloodFillTesting(int x, int y)
    {

        int width = currentChunk.width;
        int height = currentChunk.height;

        bool inRange = x < width - 1 && x > 0 && y < height - 1 && y > 0;

        //check if there is a path north south east
        
        if (inRange && (currentChunk.chunkMap[x + 1, y] >= 1 && currentChunk.chunkMap[x + 1, y] <= 3))
        {
            return FloodFillTesting (x + 1, y);
        }
        else if (inRange && (currentChunk.chunkMap[x - 1, y] >= 1 && currentChunk.chunkMap[x - 1, y] <= 3))
        {
            return FloodFillTesting (x - 1, y);
        }
        else if (inRange && (currentChunk.chunkMap[x, y + 1] >= 1 && currentChunk.chunkMap[x, y + 1] <= 3))
        {
            return FloodFillTesting (x, y + 1);
        }    

        else
        {
            
            if (x == width - 1 || y == height - 1 || x == 0 || y == 0)
            {
                if (currentChunk.chunkMap[x, y] >= 1 && currentChunk.chunkMap[x, y] <= 3)
                {
                    return true;
                }
            }

            return false;
        }

    }*/

    bool PathAvailable(int x, int y)
    {
        //uses flood fill algorithm to see if there is a path available
        if (isEdge(x, y) && (currentChunk.chunkMap[x, y] >= 1 && currentChunk.chunkMap[x, y] >= 3))
        {
            return true;
        }

        if (currentChunk.chunkMap[x, y] == 0)
        {
            return false;
        }
        
        PathAvailable(x + 1, y);
        PathAvailable(x - 1, y);
        PathAvailable(x, y + 1);
        PathAvailable(x, y - 1);

        return true;
    }

    bool isEdge(int x, int y)
    {
        if (x == 0 || y == 0 || x == currentChunk.width - 1 || y == currentChunk.height - 1) return true;
        else return false;
    }

}
