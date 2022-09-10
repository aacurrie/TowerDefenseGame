using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementScript : MonoBehaviour
{
    public Camera playerCamera;
    public GameObject crossbowTower;
    public GameObject flameTower;
    public GameObject cannonTower;
    
    private GameObject ghostTower;
    private bool visualized = false;
    private GameObject selectedTower;

    void Update()
    {

        GetSelectedUI();

        if(selectedTower != null && !visualized)
        {
            PlacementVisualizer(selectedTower);
        }
        
        RaycastHit2D hit = Physics2D.Raycast(playerCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if(ghostTower != null)
        {
            if(hit.collider != null)
            {
                CheckIfValid(hit.collider.gameObject);
                ghostTower.transform.position = hit.transform.gameObject.transform.position;
            }
        }
        if(Input.GetMouseButtonDown(0))
        {
            if(CheckIfValid(hit.collider.gameObject))
            {
                Place();
                hit.collider.gameObject.tag = "Taken";
                Debug.Log("Placed");
            }
        }
    }

    void PlacementVisualizer(GameObject tower)
    {
        ghostTower = Instantiate(tower, Input.mousePosition, Quaternion.identity);
        visualized = true;
    }
    bool CheckIfValid(GameObject cell)
    {
        if(cell.tag.Equals("Open"))
        {
            ghostTower.GetComponent<SpriteRenderer>().color = Color.green;
            return true;
        }
        else
        {
            ghostTower.GetComponent<SpriteRenderer>().color = Color.red;
            return false;
        }
    }
    void Place()
    {
        RaycastHit2D hit = Physics2D.Raycast(playerCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);    
        if(hit.collider != null)
        {
            Debug.Log("RAY HIT");
            GameObject hitObj = hit.collider.gameObject;
            Vector3 placePosition = hitObj.transform.position;
            Instantiate(crossbowTower, placePosition, Quaternion.identity);
        }
    }
    void GetSelectedUI()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(playerCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 1 << LayerMask.NameToLayer("UI"));

            switch(hit.collider.gameObject.tag)
            {
                case "CrossbowUI":
                    selectedTower = crossbowTower;
                    visualized = false;
                    Destroy(ghostTower);
                    break;
                case "CannonUI":
                    selectedTower = cannonTower;
                    visualized = false;
                    Destroy(ghostTower);
                    break;
                case "FlameUI":
                    selectedTower = flameTower;
                    visualized = false;
                    Destroy(ghostTower);
                    break;
            }
        }
    }
}
