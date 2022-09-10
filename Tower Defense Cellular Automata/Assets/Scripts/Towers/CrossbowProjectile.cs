using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowProjectile : MonoBehaviour
{
    public GameObject Target;
    public float projectileSpeed = 5.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, projectileSpeed * Time.deltaTime);
    }
}
