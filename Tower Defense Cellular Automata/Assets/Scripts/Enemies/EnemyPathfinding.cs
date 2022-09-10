using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyPathfinding : MonoBehaviour
{
    [Header("References")]
    public List<GameObject> pathway = new List<GameObject>();
    public Transform Target;
    public Rigidbody2D rb;

    [Header("Properties")]
    private Seeker seeker;
    public float speed = 300f;
    public float updateRate = 2f;
    public float nextWaypointDistance =3f;
    public Path path;
    public bool pathIsEnded = false;
    public ForceMode2D fMode;
    private int currentWaypoint = 0;
    public bool seekingStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        //Target = GameObject.Find("Castle").transform;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        Target = GameObject.FindWithTag("Castle").transform;

        StartCoroutine(UpdatePath());

        if(Target == null)
        {
            //Debug.LogError("No Target Found");
            return;
        }
        
    }

    IEnumerator UpdatePath()
    {
        if(Target == null)
        {
            yield return false;
        }

        seeker.StartPath(transform.position, Target.position, OnPathComplete); 

        yield return new WaitForSeconds(1f/updateRate);
        StartCoroutine(UpdatePath());
    }

    public void OnPathComplete(Path p)
    {
        //Debug.Log("Path Found. Errors: " + p.error);
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    public void FixedUpdate()
    {
        if(Target != null)
            //Debug.Log("Target " + Target.gameObject);
        if (Target == null)
        {
            return;
        }

        if(path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            if (pathIsEnded)
            {
                return;
            }
            //Debug.Log("Reached end of path.");
            pathIsEnded = true;
            return;
        }
        pathIsEnded = false;

        /*
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Target.rotation, 1.0f * Time.deltaTime);
        dir *= speed * Time.fixedDeltaTime;
        */

        transform.position = Vector3.MoveTowards(transform.position, path.vectorPath[currentWaypoint], speed * Time.deltaTime);

        float dist = Vector3.Distance (transform.position, path.vectorPath[currentWaypoint]);

        if(dist == 0)
        {
            currentWaypoint++;
            return;
        }
    }

    public void UpdateTarget(Transform target)
    {
        this.Target = target;
    }
    public void UpdateLayer(int layer)
    {
        gameObject.layer = layer;
    }
    public void SeekTarget()    
    {
        seekingStarted = true;
        seeker.StartPath(transform.position, Target.position, OnPathComplete);
    }
    public Transform GetTransform()
    {
        return this.transform;
    }
}
