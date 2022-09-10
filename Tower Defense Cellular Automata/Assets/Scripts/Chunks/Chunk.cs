using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public GameObject[,] chunkMap;
    public int width = 7;
    public int height = 7;

    public int endX, endY;
    public int startX, startY;
    public bool startChunk = false;
    public GameObject pathBlock;
    public GameObject grassBlock;
    public GameObject castle;

    public bool filled = false;
    private void Awake() 
    {
        GenerateChunk();
        if (startChunk)
        {
            /*switch (Random.Range(0, 2))
            {
                case 0:
                    FillStartChunkUp();
                    break;
                case 1:
                    FillStartChunkLeft();
                    break;
                case 2:
                    FillStartChunkRight();
                    break;
                default:
                    FillStartChunkUp();
                    break;
            }*/

            FillStartChunkUp();
            
            DrawStartChunk();
            filled = true;
            
            
        }

        
    }

    void GenerateChunk()
    {
        chunkMap = new GameObject[width, height];
    }

    void FillStartChunkUp()
    {
        for (int i = 0; i < width; i++)
        {
            for (int y = 0; y < height; y++)
            {
                chunkMap[i,y] = grassBlock;
            }
        }

        chunkMap[width/2, height/2] = castle;

        for (int i = height/2 + 1; i < height; i++)
        {
            chunkMap[width/2, i] = pathBlock;
        }


        chunkMap[width/2, height - 1].tag = "EndPath";

        endX = width/2;
        endY = height - 1;
    }

    void FillStartChunkRight()
    {
        for (int i = 0; i < width; i++)
        {
            for (int y = 0; y < height; y++)
            {
                chunkMap[i,y] = grassBlock;
            }
        }

        chunkMap[width/2, height/2] = castle;

        for (int i = width/2 + 1; i < width; i++)
        {
            chunkMap[i, height/2] = pathBlock;
        }


        chunkMap[width - 1, height/2].tag = "EndPath";

        endX = width - 1;
        endY = height/2;
    }

    void FillStartChunkLeft()
    {
        for (int i = 0; i < width; i++)
        {
            for (int y = 0; y < height; y++)
            {
                chunkMap[i,y] = grassBlock;
            }
        }

        chunkMap[width/2, height/2] = castle;

        for (int i = width/2 - 1; i >= 0; i--)
        {
            chunkMap[i, height/2] = pathBlock;
        }


        chunkMap[0, height/2].tag = "EndPath";

        endX = 0;
        endY = height/2;
    }

    void DrawStartChunk()
    {
        for (int i = 0; i <width; i++)
        {
            for (int y = 0; y < height; y++)
            {
                Instantiate(chunkMap[i, y], new Vector2(i, y), Quaternion.identity);
            }
        }
    }

    public override string ToString()
    {
        return "Chunk Location: " + transform.position + "\n" + "Chunk EndPath: (" + endX + "," + endY + ")" + "\n" + "Chunk StartPath: (" + startX + "," + startY + ")";
    }
}
