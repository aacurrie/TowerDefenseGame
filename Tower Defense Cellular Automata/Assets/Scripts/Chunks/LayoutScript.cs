using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutScript : MonoBehaviour
{
    public Vector2 LocationInfo;

    public GameObject Grass;
    public GameObject Pathway;

    public GameObject [,] chunkPieces;
    // Start is called before the first frame update
    void Start()
    {
        chunkPieces = new GameObject[7, 7];
        fillGround((int)this.gameObject.transform.position.y, (int)this.gameObject.transform.position.x); //creates the ground itself
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fillGround(int startingY, int startingX){
        LocationInfo = new Vector2(startingX, startingY);
        for(int row = startingY; row < startingY + 7; row++){ //height
            for(int col = startingX; col < startingX + 7; col++){ //width
                GameObject block = Instantiate(Grass, LocationInfo, Quaternion.identity);
                chunkPieces[row, col] = block;
            }
        }
    }

    public void checkHowMany(int startingY, int startingX){
        int howMany = 0;
        for(int row = startingY; row < startingY + 7; row++){ //height
            for(int col = startingX; col < startingX + 7; col++){ //width
                if(chunkPieces[row, col] != null){
                    howMany++;
                }
            }
        }
        Debug.Log(howMany);
    }
}
