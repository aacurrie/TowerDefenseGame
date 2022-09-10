using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class DragCamera : MonoBehaviour
{
    private Vector3 Origin;
    private Vector3 Difference;
    private Vector3 ResetCamera;

    public GameObject background;

    private bool drag = false;

    void Start(){
        ResetCamera = Camera.main.transform.position;
    }

    private void LateUpdate(){
        if(Input.GetMouseButton(1)){
            Difference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;
            if(!drag){
                drag = true;
                Origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else{
            drag = false;
        }

        if(drag){
            Camera.main.transform.position = Origin - Difference;
            background.transform.position = Origin - Difference;
        }
    }
}
