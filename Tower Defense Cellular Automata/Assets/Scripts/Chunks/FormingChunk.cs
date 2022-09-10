using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormingChunk : MonoBehaviour
{
    public GameObject Grass;
    public GameObject Pathway;
    public GameObject Chunk;

    public Vector2 LocationInfo;
    public int yPos;
    public int xPos;

    // Start is called before the first frame update
    void Start()
    {
        xPos = -3;
        yPos = 4;

        makeGrassBase(xPos, yPos);
        changeLocationUp();
        makeGrassBase(xPos, yPos);
        changeLocationUp();
        makeGrassBase(xPos, yPos);
        changeLocationUp();
        makeGrassBase(xPos, yPos);
        changeLocationUp();
        makeGrassBase(xPos, yPos);
        changeLocationUp();
        makeGrassBase(xPos, yPos);
        changeLocationUp();
        makeGrassBase(xPos, yPos);
        changeLocationUp();
        makeGrassBase(xPos, yPos);
        changeLocationUp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void makePath(int directionFrom){ //0 = down, 1 = up, 2 = left, 3 = right
        
    }

    public void makeGrassBase(int startingX, int startingY){
        LocationInfo = new Vector2(startingX, startingY);
        Instantiate(Chunk, new Vector2(startingX+3, startingY-4), Quaternion.identity);
        for(int row = startingY; row < startingY + 7; row++){
            for(int col = startingX; col < startingX + 7; col++){
                if(col == startingX + 3 && row == startingY+6){
                    Instantiate(Pathway, LocationInfo, Quaternion.identity);
                }
                Instantiate(Grass, LocationInfo, Quaternion.identity);
                GameObject.Find("FormChunk").GetComponent<PathwayArrayControl>().changeEndPath();
                LocationInfo.x++;
            }
            LocationInfo.x-=7;
            LocationInfo.y++;
        }
        GameObject.Find("FormChunk").GetComponent<ChunkArrayControl>().changeCurrentChunk();
    }

    public void placePathConnector(int xPosBlock, int yBlockPos){
        
    }

    //will run one of these based on the location of object tagged "exitPath"
    public void changeLocationLeft(){
        this.xPos-=7;
    }
    
    public void changeLocationRight(){
        this.xPos+=7;
    }

    public void changeLocationUp(){
        this.yPos+=7;
    }

    public void changeLocationDown(){
        this.yPos-=7;
    }
}
