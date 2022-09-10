using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCamera : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject background;
    public float multiplier;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera.orthographicSize = 5;
        background.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(mainCamera.orthographicSize >= 5 && mainCamera.orthographicSize <= 30){
            mainCamera.orthographicSize -= 20f * (Input.GetAxis("Mouse ScrollWheel"));
            if(Input.GetAxis("Mouse ScrollWheel") < 0){
                background.transform.localScale = new Vector3(mainCamera.orthographicSize/5f, mainCamera.orthographicSize/5f, mainCamera.orthographicSize/5f);
            }
            if(Input.GetAxis("Mouse ScrollWheel") > 0){
                background.transform.localScale = new Vector3(mainCamera.orthographicSize/5f, mainCamera.orthographicSize/5f, mainCamera.orthographicSize/5f);
            }
        }
        else if(mainCamera.orthographicSize < 5){
            mainCamera.orthographicSize = 5;
            background.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else{
            mainCamera.orthographicSize = 30;
            background.transform.localScale = new Vector3(6f, 6f, 6f);
        }
    }
}
