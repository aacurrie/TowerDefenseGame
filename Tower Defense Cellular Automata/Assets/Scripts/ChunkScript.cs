using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkScript : MonoBehaviour
{
    [SerializeField] private int chunkWidth;
    [SerializeField] private int chunkHeight;

    //blocks
    public GameObject pathBlock;
    public GameObject grassBlock;
    public GameObject castle;

    //lists
    public List<GameObject> pathwayTiles = new List<GameObject>();
    public List<GameObject> groundTiles = new List<GameObject>();

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
    private GameObject randomMiddle;
    private int randomMiddleRow = -1;

    //coloring
    public Color pathColor;

    //addingLocation
    public static int offSet = 0;
    public static int TotalChunksCreated = 0;
    public static int referencePrevChunk = 3;

    public static string nextDirection;

    // Start is called before the first frame update
    void Awake()
    {
        //nextDirection = "UP";
        //TotalChunksCreated = 0;
        chunkWidth = 7;
        chunkHeight = 7;

        //generateMap();
        chunkCreation = true;

        generateMap();
    }

    private void createStartChunk(){ //done
        List<GameObject> startChunk = new List<GameObject>();
        for(int y = 0 + GenerateMap.offSetUp - 7; y < chunkHeight + GenerateMap.offSetUp - 7; y++){
            for(int x = 0 + GenerateMap.offSetLR; x < chunkWidth + GenerateMap.offSetLR; x++){
                if(x == 3 && y == 3){
                    GameObject newCastle = Instantiate(castle);
                    newCastle.transform.position = new Vector2(x, y);
                    startChunk.Add(newCastle);
                }
                
                GameObject newGrass = Instantiate(grassBlock);
                newGrass.transform.position = new Vector2(x, y);
                startChunk.Add(newGrass);
            }
        }

        pathwayTiles.Add(startChunk[18+14]);
        pathwayTiles.Add(startChunk[18+21]);
        pathwayTiles.Add(startChunk[18+28]);

        foreach(GameObject ground in pathwayTiles){
            ground.GetComponent<SpriteRenderer>().color = pathColor;
            ground.tag = "Path";
            //pathwayTiles[lp] = pathBlock;
            //Destroy(ground);
        }

        GenerateMap.path.Add(pathwayTiles);
        GenerateMap.path.Add(startChunk);

        TotalChunksCreated++;
        nextDirection = "UP";
        //GenerateMap.incrementOffSetUp();
    }

    private void StayUP(){ //done
        for(int y = 0 + GenerateMap.offSetUp; y < chunkHeight + GenerateMap.offSetUp; y++){
            for(int x = 0 + GenerateMap.offSetLR; x < chunkWidth + GenerateMap.offSetLR; x++){
                GameObject newGrass = Instantiate(grassBlock);
                newGrass.transform.position = new Vector2(x, y);
                groundTiles.Add(newGrass);
            }
        }

        List<GameObject> topEdge = getEdgeTop();
        List<GameObject> bottomEdge = getEdgeBottom();
        List<GameObject> middleRow = null;

        if(Random.Range(0, 3) < 2){ //2/3 chance
            randomMiddleRow = Random.Range(3, 6); //random row from 3 to 6
            middleRow = getRandomRow(randomMiddleRow);
        }

        //determine random start and end
        int randomTop = Random.Range(1, chunkWidth-1);
        int Bottom = referencePrevChunk;

        referencePrevChunk = randomTop;



        //set start and end chunk
        endOfChunk = topEdge[randomTop];

        if(randomMiddleRow > 0){ //if randomMiddle is positive - meaning it is selected
            int randomMiddleIndex = Random.Range(0, chunkWidth);
            randomMiddle = middleRow[randomMiddleIndex];
        }

        startOfChunk = bottomEdge[Bottom];
        currentPath = startOfChunk;

        moveUp();
        if(randomMiddleRow <= -1){
            while(!reachedX){
                if(currentPath.transform.position.x > endOfChunk.transform.position.x){ //needs to move left
                    moveLeft();
                }
                else if(currentPath.transform.position.x < endOfChunk.transform.position.x){ //needs to move right
                    moveRight();
                }
                else{ //needs to go up
                    reachedX = true;
                }
            }

            while(!reachedY){
                if(currentPath.transform.position.y < endOfChunk.transform.position.y){ //needs to move left
                    moveUp();
                }
                else{
                    reachedY = true;
                }
            }
        }

        else{
            Debug.Log(randomMiddleRow);
            while(!reachedX){
                if(currentPath.transform.position.x > randomMiddle.transform.position.x){ //needs to move left
                    moveLeft();
                }
                else if(currentPath.transform.position.x < randomMiddle.transform.position.x){ //needs to move right
                    moveRight();
                }
                else{ //needs to go up
                    reachedX = true;
                }
            }
            while(!reachedY){
                if(currentPath.transform.position.y < randomMiddle.transform.position.y){ //needs to move left
                    moveUp();
                }
                else{
                    reachedY = true;
                }
            }

            reachedX = false;
            reachedY = false;

            while(!reachedX){
                if(currentPath.transform.position.x > endOfChunk.transform.position.x){ //needs to move left
                    moveLeft();
                }
                else if(currentPath.transform.position.x < endOfChunk.transform.position.x){ //needs to move right
                    moveRight();
                }
                else{ //needs to go up
                    reachedX = true;
                }
            }
            while(!reachedY){
                if(currentPath.transform.position.y < endOfChunk.transform.position.y){ //needs to move left
                    moveUp();
                }
                else{
                    reachedY = true;
                }
            }
        }

        //destroy all of those cutouts for the path
        pathwayTiles.Add(endOfChunk);
        foreach(GameObject ground in pathwayTiles){
            Debug.Log(ground.transform.position.x);
            ground.GetComponent<SpriteRenderer>().color = pathColor;
            ground.tag = "Path";

            GenerateMap.pathFinding.Add(ground);
        }

        TotalChunksCreated++;
        //updating the chunks total
        GenerateMap.path.Add(pathwayTiles);
        GenerateMap.path.Add(groundTiles);

        nextDirection = "UP";
        GenerateMap.incrementOffSetUp();
    }

    private void UpToLeft(){ //done
        for(int y = 0 + GenerateMap.offSetUp; y < chunkHeight + GenerateMap.offSetUp; y++){
            for(int x = 0 + GenerateMap.offSetLR; x < chunkWidth + GenerateMap.offSetLR; x++){
                GameObject newGrass = Instantiate(grassBlock);
                newGrass.transform.position = new Vector2(x, y);
                groundTiles.Add(newGrass);
            }
        }


        List<GameObject> leftEdge = getEdgeLeft();
        List<GameObject> bottomEdge = getEdgeBottom();

        //determine random start and end
        int randomLeft = Random.Range(2, 5);
        int Bottom = referencePrevChunk;

        referencePrevChunk = randomLeft;

        //set start and end chunk
        endOfChunk = leftEdge[randomLeft];

        startOfChunk = bottomEdge[Bottom];
        currentPath = startOfChunk;

        moveUp();   

        if(1 != 1){
            //will put the more random patterns here
        }
        else{
            //Debug.Log(randomMiddleRow);
            reachedX = false;
            reachedY = false;

            Debug.Log(reachedX + "   " + reachedY);
            while(!reachedY){
                if(currentPath.transform.position.y < endOfChunk.transform.position.y){ //needs to move left
                    Debug.Log("testing6");
                    moveUp();
                }
                else{
                    reachedY = true;
                }
            }
            while(!reachedX){
                if(currentPath.transform.position.x > endOfChunk.transform.position.x){ //needs to move left
                    moveLeft();
                }
                else if(currentPath.transform.position.x < endOfChunk.transform.position.x){ //needs to move right
                    moveRight();
                }
                else{ //needs to go up
                    reachedX = true;
                }
            }
        }

        //destroy all of those cutouts for the path
        pathwayTiles.Add(endOfChunk);
        foreach(GameObject ground in pathwayTiles){
            Debug.Log(ground.transform.position.x);
            ground.GetComponent<SpriteRenderer>().color = pathColor;
            ground.tag = "Path";

            GenerateMap.pathFinding.Add(ground);
        }

        TotalChunksCreated++;
        //updating the chunks total
        GenerateMap.path.Add(pathwayTiles);
        GenerateMap.path.Add(groundTiles);

        nextDirection = "LEFT";
        GenerateMap.incrementOffSetLeft();
    }

    private void UpToRight(){ //done
        for(int y = 0 + GenerateMap.offSetUp; y < chunkHeight + GenerateMap.offSetUp; y++){
            for(int x = 0 + GenerateMap.offSetLR; x < chunkWidth + GenerateMap.offSetLR; x++){
                GameObject newGrass = Instantiate(grassBlock);
                newGrass.transform.position = new Vector2(x, y);
                groundTiles.Add(newGrass);
            }
        }


        List<GameObject> rightEdge = getEdgeRight();
        List<GameObject> bottomEdge = getEdgeBottom();

        //determine random start and end
        int randomRight = Random.Range(2, 5);
        int Bottom = referencePrevChunk;

        referencePrevChunk = randomRight;

        //set start and end chunk
        endOfChunk = rightEdge[randomRight];

        startOfChunk = bottomEdge[Bottom];
        currentPath = startOfChunk;

        moveUp();   

        if(1 != 1){
            //will put the more random patterns here
        }
        else{
            //Debug.Log(randomMiddleRow);
            reachedX = false;
            reachedY = false;

            Debug.Log(reachedX + "   " + reachedY);
            while(!reachedY){
                if(currentPath.transform.position.y < endOfChunk.transform.position.y){ //needs to move left
                    Debug.Log("testing6");
                    moveUp();
                }
                else{
                    reachedY = true;
                }
            }
            while(!reachedX){
                if(currentPath.transform.position.x > endOfChunk.transform.position.x){ //needs to move left
                    moveLeft();
                }
                else if(currentPath.transform.position.x < endOfChunk.transform.position.x){ //needs to move right
                    moveRight();
                }
                else{ //needs to go up
                    reachedX = true;
                }
            }
        }

        //destroy all of those cutouts for the path
        pathwayTiles.Add(endOfChunk);
        foreach(GameObject ground in pathwayTiles){
            Debug.Log(ground.transform.position.x);
            ground.GetComponent<SpriteRenderer>().color = pathColor;
            ground.tag = "Path";

            GenerateMap.pathFinding.Add(ground);
        }

        TotalChunksCreated++;
        //updating the chunks total
        GenerateMap.path.Add(pathwayTiles);
        GenerateMap.path.Add(groundTiles);

        nextDirection = "RIGHT";
        GenerateMap.incrementOffSetRight();
    }

    private void StayLEFT(){ //done
        for(int y = 0 + GenerateMap.offSetUp; y < chunkHeight + GenerateMap.offSetUp; y++){
            for(int x = 0 + GenerateMap.offSetLR; x < chunkWidth + GenerateMap.offSetLR; x++){
                GameObject newGrass = Instantiate(grassBlock);
                newGrass.transform.position = new Vector2(x, y);
                groundTiles.Add(newGrass);
            }
        }


        List<GameObject> leftEdge = getEdgeLeft();
        List<GameObject> rightEdge = getEdgeRight();
        List<GameObject> middleRow = null;

        //determine random start and end
        int randomLeft = Random.Range(2, 5);
        int Right = referencePrevChunk + 1;
        referencePrevChunk = randomLeft;

        if(Random.Range(0, 3) < 2){ //2/3 chance
            randomMiddleRow = Random.Range(2, 4); //random col from 3 to 6
            middleRow = getRandomRowLR(randomMiddleRow);
        }

        //set start and end chunk
        endOfChunk = leftEdge[randomLeft];
        startOfChunk = rightEdge[Right];
        currentPath = startOfChunk;

        if(randomMiddleRow > 0){ //if randomMiddle is positive - meaning it is selected
            int randomMiddleIndex = Random.Range(0, chunkHeight);
            randomMiddle = middleRow[randomMiddleIndex];
        }


        moveLeft();   

        if(randomMiddleRow > -1){ //meaning alternate pathway
            reachedX = false;
            reachedY = false;
            while(!reachedY){
                if(currentPath.transform.position.y < randomMiddle.transform.position.y){ //needs to move left
                    moveUp();
                }
                else if(currentPath.transform.position.y > randomMiddle.transform.position.y){
                    moveDown();
                }
                else{
                    reachedY = true;
                }
            }

            while(!reachedX){
                if(currentPath.transform.position.x > randomMiddle.transform.position.x){ //needs to move left
                    moveLeft();
                }
                else if(currentPath.transform.position.x < randomMiddle.transform.position.x){ //needs to move right
                    moveRight();
                }
                else{ //needs to go up
                    reachedX = true;
                }
            }

            reachedX = false;
            reachedY = false;

            while(!reachedY){
                if(currentPath.transform.position.y < endOfChunk.transform.position.y){ //needs to move left
                    Debug.Log("testing6");
                    moveUp();
                }
                else if(currentPath.transform.position.y > endOfChunk.transform.position.y){
                    moveDown();
                }
                else{
                    reachedY = true;
                }
            }

            Debug.Log("change directions");
            while(!reachedX){
                if(currentPath.transform.position.x > endOfChunk.transform.position.x){ //needs to move left
                    moveLeft();
                }
                else if(currentPath.transform.position.x < endOfChunk.transform.position.x){ //needs to move right
                    moveRight();
                }
                else{ //needs to go up
                    reachedX = true;
                }
            }
        }
        else{
            //Debug.Log(randomMiddleRow);
            reachedX = false;
            reachedY = false;

            
            while(!reachedY){
                if(currentPath.transform.position.y < endOfChunk.transform.position.y){ //needs to move left
                    Debug.Log("testing6");
                    moveUp();
                }
                else if(currentPath.transform.position.y > endOfChunk.transform.position.y){
                    moveDown();
                }
                else{
                    reachedY = true;
                }
            }

            Debug.Log("change directions");
            while(!reachedX){
                if(currentPath.transform.position.x > endOfChunk.transform.position.x){ //needs to move left
                    moveLeft();
                }
                else if(currentPath.transform.position.x < endOfChunk.transform.position.x){ //needs to move right
                    moveRight();
                }
                else{ //needs to go up
                    reachedX = true;
                }
            }
        }

        //destroy all of those cutouts for the path
        pathwayTiles.Add(endOfChunk);
        foreach(GameObject ground in pathwayTiles){
            Debug.Log(ground.transform.position.x);
            ground.GetComponent<SpriteRenderer>().color = pathColor;
            ground.tag = "Path";

            GenerateMap.pathFinding.Add(ground);
        }

        TotalChunksCreated++;
        //updating the chunks total
        GenerateMap.path.Add(pathwayTiles);
        GenerateMap.path.Add(groundTiles);

        nextDirection = "LEFT";
        GenerateMap.incrementOffSetLeft();
    }

    private void RightToUp(){ //done
        for(int y = 0 + GenerateMap.offSetUp; y < chunkHeight + GenerateMap.offSetUp; y++){
            for(int x = 0 + GenerateMap.offSetLR; x < chunkWidth + GenerateMap.offSetLR; x++){
                GameObject newGrass = Instantiate(grassBlock);
                newGrass.transform.position = new Vector2(x, y);
                groundTiles.Add(newGrass);
            }
        }


        List<GameObject> leftEdge = getEdgeLeft();
        List<GameObject> topEdge = getEdgeTop();

        //determine random start and end
        int randomTop = Random.Range(2, 5);
        int Left = referencePrevChunk - 1;

        referencePrevChunk = randomTop;

        //set start and end chunk
        endOfChunk = topEdge[randomTop];
        startOfChunk = leftEdge[Left];

        currentPath = startOfChunk;

        moveRight();   

        if(1 != 1){
            //will put the more random patterns here
        }
        else{
            //Debug.Log(randomMiddleRow);
            reachedX = false;
            reachedY = false;

            Debug.Log(reachedX + "   " + reachedY);
            while(!reachedX){
                if(currentPath.transform.position.x > endOfChunk.transform.position.x){ //needs to move left
                    moveLeft();
                }
                else if(currentPath.transform.position.x < endOfChunk.transform.position.x){ //needs to move right
                    moveRight();
                }
                else{ //needs to go up
                    reachedX = true;
                }
            }

            while(!reachedY){
                if(currentPath.transform.position.y < endOfChunk.transform.position.y){ //needs to move left
                    Debug.Log("testing6");
                    moveUp();
                }
                else{
                    reachedY = true;
                }
            }
        }

        //destroy all of those cutouts for the path
        pathwayTiles.Add(endOfChunk);
        foreach(GameObject ground in pathwayTiles){
            Debug.Log(ground.transform.position.x);
            ground.GetComponent<SpriteRenderer>().color = pathColor;
            ground.tag = "Path";

            GenerateMap.pathFinding.Add(ground);
        }

        TotalChunksCreated++;
        //updating the chunks total
        GenerateMap.path.Add(pathwayTiles);
        GenerateMap.path.Add(groundTiles);

        nextDirection = "UP";
        GenerateMap.incrementOffSetUp();
    }

    private void LeftToUp(){ //done
        for(int y = 0 + GenerateMap.offSetUp; y < chunkHeight + GenerateMap.offSetUp; y++){
            for(int x = 0 + GenerateMap.offSetLR; x < chunkWidth + GenerateMap.offSetLR; x++){
                GameObject newGrass = Instantiate(grassBlock);
                newGrass.transform.position = new Vector2(x, y);
                groundTiles.Add(newGrass);
            }
        }


        List<GameObject> rightEdge = getEdgeRight();
        List<GameObject> topEdge = getEdgeTop();

        //determine random start and end
        int randomTop = Random.Range(2, 5);
        int Right = referencePrevChunk + 1;

        referencePrevChunk = randomTop;

        //set start and end chunk
        endOfChunk = topEdge[randomTop];
        startOfChunk = rightEdge[Right];

        currentPath = startOfChunk;

        moveLeft();   

        if(1 != 1){
            //will put the more random patterns here
        }
        else{
            //Debug.Log(randomMiddleRow);
            reachedX = false;
            reachedY = false;

            Debug.Log(reachedX + "   " + reachedY);
            while(!reachedX){
                if(currentPath.transform.position.x > endOfChunk.transform.position.x){ //needs to move left
                    moveLeft();
                }
                else if(currentPath.transform.position.x < endOfChunk.transform.position.x){ //needs to move right
                    moveRight();
                }
                else{ //needs to go up
                    reachedX = true;
                }
            }

            while(!reachedY){
                if(currentPath.transform.position.y < endOfChunk.transform.position.y){ //needs to move left
                    Debug.Log("testing6");
                    moveUp();
                }
                else{
                    reachedY = true;
                }
            }
        }

        //destroy all of those cutouts for the path
        pathwayTiles.Add(endOfChunk);
        foreach(GameObject ground in pathwayTiles){
            Debug.Log(ground.transform.position.x);
            ground.GetComponent<SpriteRenderer>().color = pathColor;
            ground.tag = "Path";

            GenerateMap.pathFinding.Add(ground);
        }

        TotalChunksCreated++;
        //updating the chunks total
        GenerateMap.path.Add(pathwayTiles);
        GenerateMap.path.Add(groundTiles);

        nextDirection = "UP";
        GenerateMap.incrementOffSetUp();
    }

    private void StayRIGHT(){ //done
        for(int y = 0 + GenerateMap.offSetUp; y < chunkHeight + GenerateMap.offSetUp; y++){
            for(int x = 0 + GenerateMap.offSetLR; x < chunkWidth + GenerateMap.offSetLR; x++){
                GameObject newGrass = Instantiate(grassBlock);
                newGrass.transform.position = new Vector2(x, y);
                groundTiles.Add(newGrass);
            }
        }


        List<GameObject> leftEdge = getEdgeLeft();
        List<GameObject> rightEdge = getEdgeRight();
        List<GameObject> middleRow = null;

        //determine random start and end
        int randomRight = Random.Range(2, 5);
        int Left = referencePrevChunk - 1;
        referencePrevChunk = randomRight;

        if(Random.Range(0, 3) < 2){ //2/3 chance
            randomMiddleRow = Random.Range(3, 6); //random col from 3 to 6
            middleRow = getRandomRowLR(randomMiddleRow);
        }

        //set start and end chunk
        endOfChunk = rightEdge[randomRight];
        startOfChunk = leftEdge[Left];
        currentPath = startOfChunk;

        if(randomMiddleRow > 0){ //if randomMiddle is positive - meaning it is selected
            int randomMiddleIndex = Random.Range(0, chunkHeight);
            randomMiddle = middleRow[randomMiddleIndex];
        }


        moveRight();   

        if(randomMiddleRow > -1){ //meaning alternate pathway
            reachedX = false;
            reachedY = false;
            while(!reachedY){
                if(currentPath.transform.position.y < randomMiddle.transform.position.y){ //needs to move left
                    moveUp();
                }
                else if(currentPath.transform.position.y > randomMiddle.transform.position.y){
                    moveDown();
                }
                else{
                    reachedY = true;
                }
            }

            while(!reachedX){
                if(currentPath.transform.position.x > randomMiddle.transform.position.x){ //needs to move left
                    moveLeft();
                }
                else if(currentPath.transform.position.x < randomMiddle.transform.position.x){ //needs to move right
                    moveRight();
                }
                else{ //needs to go up
                    reachedX = true;
                }
            }

            reachedX = false;
            reachedY = false;

            while(!reachedY){
                if(currentPath.transform.position.y < endOfChunk.transform.position.y){ //needs to move left
                    Debug.Log("testing6");
                    moveUp();
                }
                else if(currentPath.transform.position.y > endOfChunk.transform.position.y){
                    moveDown();
                }
                else{
                    reachedY = true;
                }
            }

            Debug.Log("change directions");
            while(!reachedX){
                if(currentPath.transform.position.x > endOfChunk.transform.position.x){ //needs to move left
                    moveLeft();
                }
                else if(currentPath.transform.position.x < endOfChunk.transform.position.x){ //needs to move right
                    moveRight();
                }
                else{ //needs to go up
                    reachedX = true;
                }
            }
        }
        else{
            //Debug.Log(randomMiddleRow);
            reachedX = false;
            reachedY = false;

            
            while(!reachedY){
                if(currentPath.transform.position.y < endOfChunk.transform.position.y){ //needs to move left
                    Debug.Log("testing6");
                    moveUp();
                }
                else if(currentPath.transform.position.y > endOfChunk.transform.position.y){
                    moveDown();
                }
                else{
                    reachedY = true;
                }
            }

            Debug.Log("change directions");
            while(!reachedX){
                if(currentPath.transform.position.x > endOfChunk.transform.position.x){ //needs to move left
                    moveLeft();
                }
                else if(currentPath.transform.position.x < endOfChunk.transform.position.x){ //needs to move right
                    moveRight();
                }
                else{ //needs to go up
                    reachedX = true;
                }
            }
        }

        //destroy all of those cutouts for the path
        pathwayTiles.Add(endOfChunk);
        foreach(GameObject ground in pathwayTiles){
            Debug.Log(ground.transform.position.x);
            ground.GetComponent<SpriteRenderer>().color = pathColor;
            ground.tag = "Path";

            GenerateMap.pathFinding.Add(ground);
        }

        TotalChunksCreated++;
        //updating the chunks total
        GenerateMap.path.Add(pathwayTiles);
        GenerateMap.path.Add(groundTiles);

        nextDirection = "RIGHT";
        GenerateMap.incrementOffSetRight();
    }

    private void generateMap(){
        if(TotalChunksCreated == 0){
            createStartChunk();
        }
        
        if(TotalChunksCreated == 1 && nextDirection == "UP"){
            Debug.Log("hello world");
            StayUP();
        }

        else if(TotalChunksCreated > 1 && nextDirection == "UP"){
            int pathWaySelect = Random.Range(0, 3);
            if(pathWaySelect == 0){
                UpToLeft();
                Debug.Log(nextDirection);
            }
            else if(pathWaySelect == 1){
                UpToRight();
                Debug.Log(nextDirection);
            }
            else{
                StayUP();
                Debug.Log(nextDirection);
            }
            Debug.Log("Direction Number: " + pathWaySelect);
        }

        else if(TotalChunksCreated > 1 && nextDirection == "LEFT"){
            int pathWaySelect = Random.Range(0, 2);
            if(pathWaySelect == 0){
                LeftToUp();
                Debug.Log(nextDirection);
            }
            else{
                StayLEFT();
                Debug.Log(nextDirection);
            }
            Debug.Log("Direction Number: " + pathWaySelect);
        }
        
        else if(TotalChunksCreated > 1 && nextDirection == "RIGHT"){
            int pathWaySelect = Random.Range(0, 2);
            if(pathWaySelect == 0){
                RightToUp();
                Debug.Log(nextDirection);
            }
            else{
                StayRIGHT();
                Debug.Log(nextDirection);
            }
            Debug.Log("Direction Number: " + pathWaySelect);
        }
    }
    

    private List<GameObject> getEdgeTop(){
        List<GameObject> edgeTiles = new List<GameObject>();

        for(int lp = chunkWidth * (chunkHeight-1); lp < (chunkWidth * chunkHeight); lp++){ //finds the final 7 tiles
            edgeTiles.Add(groundTiles[lp]);
        }

        return edgeTiles;
    }

    private List<GameObject> getRandomRow(int row){
        List<GameObject> middleTiles = new List<GameObject>();

        for(int lp = chunkWidth * row; lp < (chunkWidth * row) + 7; lp++){ //finds the final 7 tiles
            middleTiles.Add(groundTiles[lp]);
        }

        return middleTiles;
    }

    private List<GameObject> getRandomRowLR(int col){
        List<GameObject> middleTiles = new List<GameObject>();

        for(int lp = col; lp < 49; lp+=chunkWidth){ //finds the final 7 tiles
            middleTiles.Add(groundTiles[lp]);
            Debug.Log(groundTiles[lp].transform.position.x + "    " + groundTiles[lp].transform.position.y);
        }
        return middleTiles;
    }

    private List<GameObject> getEdgeBottom(){
        List<GameObject> edgeTiles = new List<GameObject>();

        for(int lp = 0; lp < chunkWidth; lp++){ //finds the first 7 tiles
            edgeTiles.Add(groundTiles[lp]);
        }

        return edgeTiles;
    }

    private List<GameObject> getEdgeLeft(){
        List<GameObject> edgeTiles = new List<GameObject>();

        for(int lp = 7; lp < 49; lp+=7){
            edgeTiles.Add(groundTiles[lp]);
        }

        return edgeTiles;
    }

    private List<GameObject> getEdgeRight(){
        List<GameObject> edgeTiles = new List<GameObject>();

        for(int lp = 6; lp < 49; lp+=7){
            edgeTiles.Add(groundTiles[lp]);
        }

        return edgeTiles;
    }

    private void moveUp(){
        //Debug.Log("Up");
        pathwayTiles.Add(currentPath);
        index = groundTiles.IndexOf(currentPath);
        nextIndex = index + chunkWidth; //goes up a single row
        currentPath = groundTiles[nextIndex];
    }

    private void moveDown(){
        //Debug.Log("Up");
        pathwayTiles.Add(currentPath);
        index = groundTiles.IndexOf(currentPath);
        nextIndex = index - chunkWidth; //goes up a single row
        currentPath = groundTiles[nextIndex];
    }

    private void moveLeft(){
        //Debug.Log("Left");
        pathwayTiles.Add(currentPath);
        index = groundTiles.IndexOf(currentPath);
        nextIndex = index-1;
        currentPath = groundTiles[nextIndex];
    }

    private void moveRight(){
        //Debug.Log("Right");
        pathwayTiles.Add(currentPath);
        index = groundTiles.IndexOf(currentPath);
        nextIndex = index+1;
        currentPath = groundTiles[nextIndex];
    }
}