using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SimpleChunk: MonoBehaviour
{
    public int[,] chunkMap;
    public int width = 7;
    public int height = 7;

    public int endX, endY;
    public int startX, startY;
    public bool startChunk = false;

    public bool filled;

    void Awake() 
    {
        GenerateChunk();
        if (startChunk) GenerateStartChunk();
        
    }
    public enum blocks
    {
        grassBlock, pathBlock, startPath, endPath, castle
    }
    

    void GenerateChunk()
    {
        chunkMap = new int[width, height];
    }

    void GenerateStartChunk()
    {
        switch (UnityEngine.Random.Range(0, 3))
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
        }

        DrawChunk();
    }

    void FillStartChunkUp()
    {
        for (int i = 0; i < width; i++)
        {
            for (int y = 0; y < height; y++)
            {
                chunkMap[i,y] = (int)blocks.grassBlock;
            }
        }

        chunkMap[width/2, height/2] = (int)blocks.castle;

        for (int i = height/2 + 1; i < height; i++)
        {
            chunkMap[width/2, i] = (int)blocks.pathBlock;
        }


        chunkMap[width/2, height - 1]= (int)blocks.endPath;

        endX = width/2;
        endY = height - 1;
        filled = true;
    }

    void FillStartChunkRight()
    {
        for (int i = 0; i < width; i++)
        {
            for (int y = 0; y < height; y++)
            {
                chunkMap[i,y] = (int)blocks.grassBlock;
            }
        }

        chunkMap[width/2, height/2] = (int)blocks.castle;

        for (int i = width/2 + 1; i < width; i++)
        {
            chunkMap[i, height/2] = (int)blocks.pathBlock;
        }


        chunkMap[width - 1, height/2] =  (int)blocks.endPath;

        endX = width - 1;
        endY = height/2;
        filled = true;
    }

    void FillStartChunkLeft()
    {
        for (int i = 0; i < width; i++)
        {
            for (int y = 0; y < height; y++)
            {
                chunkMap[i,y] = (int)blocks.grassBlock;
            }
        }

        chunkMap[width/2, height/2] = (int)blocks.castle;

        for (int i = width/2 - 1; i >= 0; i--)
        {
            chunkMap[i, height/2] = (int)blocks.pathBlock;
        }


        chunkMap[0, height/2]= (int)blocks.endPath;

        endX = 0;
        endY = height/2;
        filled = true;
    }

    public void DrawChunk()
    {
        string rowStr = "";
        for (int i = 0; i < height; i++)
        {
            for (int y = 0; y < width; y++)
            {
                rowStr += chunkMap[i, y] + " ";
            }

            rowStr += "\n";
        }

        Debug.Log(rowStr);
        Debug.Log(this);
    }

    public override string ToString()
    {
        return "Chunk EndPath: (" + endX + "," + endY + ")" + "\n" + "Chunk StartPath: (" + startX + "," + startY + ")";
    }
}