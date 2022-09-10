using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class copyScript : MonoBehaviour
{
    private Vector3 mousePosition;
    private Rigidbody2D rb;
    private Vector2 direction;
    private float moveSpeed = 100f;

    public static bool placed;

    // Update is called once per frame
    void Start(){
        placed = false;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePosition - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);

        if(Input.GetKeyDown("escape")){
            Debug.Log("destroy");
        }

        if(placed || Input.GetKeyDown("escape")){
            placed = false;
            Destroy(this.gameObject);
        }
    }

    public static void togglePlace(){
        placed = true;
    }
}
