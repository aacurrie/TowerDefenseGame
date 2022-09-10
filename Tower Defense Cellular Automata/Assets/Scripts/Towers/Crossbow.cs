using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : MonoBehaviour
{
    
    [SerializeField, Range(1.5f, 10.5f)]
    public float targetingRange = 1.5f;
    public GameObject currentTarget;
    private Queue<GameObject> targetPriority = new Queue<GameObject>();
    private bool firing = false;
    public GameObject projectile;
    public float fireInterval = 1.5f;
    public float projectileSpeed = 5.0f;
    public Transform towerBase;
    private float age;
    private float initializationTime;
    public bool trackingEnabled = true;
    
    void Start()
    {
        initializationTime = Time.timeSinceLevelLoad;
    }
    
    void Update()
    {
        if(currentTarget == null)
        {
            if (targetPriority.Count == 0)                
            {                    
                AcquireTarget();
            }
        }

        age = Time.timeSinceLevelLoad - initializationTime;
        age = Mathf.Round(age);

        //Debug.Log(age);

        if(trackingEnabled)
        {
            TrackTarget();
        }

        if(age % 2 != 0)
        {
            firing = false;
        }
        if(!firing)
        {
            //StartCoroutine(Fire());
            Fire();
        }
        
        OnDrawGizmos();
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector2 position = towerBase.position;
        Gizmos.DrawWireSphere(position, targetingRange);
    }
    public bool AcquireTarget()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(towerBase.position, targetingRange, 1 << LayerMask.NameToLayer("Enemies"));
        foreach(Collider2D i in hitColliders)
        {
            targetPriority.Enqueue(i.transform.gameObject);
        }
        if(targetPriority.Count > 0)
        {
            currentTarget = targetPriority.Peek();
            return true;
        }
        return false;
    }
    public bool TrackTarget() 
    {
		if (currentTarget == null) {
			return false;
		}

        Vector2 difference = currentTarget.transform.position - transform.position;
        float rotationAngle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationAngle);

		Vector2 towerPostion = transform.position;
		Vector2 targetPostion = currentTarget.transform.position;
		if (Vector2.Distance(towerPostion, targetPostion) > targetingRange) {
			currentTarget = null;
            targetPriority.Dequeue();
            if(targetPriority.Count > 0)
            {
                currentTarget = targetPriority.Peek();
            }
            else
            {
                transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            }
			return false;
		}
		return true;
	}

    /*IEnumerator*/ void Fire()
    {
        //yield return new WaitForSeconds(fireInterval);
        if(/*currentTarget != null*/ age % 2 == 0)
        {
            Vector2 direction = (currentTarget.transform.position - transform.position).normalized;
            GameObject currentProjectile = Instantiate(projectile, transform.position, transform.rotation);
            currentProjectile.GetComponent<CrossbowProjectile>().Target = currentTarget;
            firing = true;
        }
        /*
        else if(currentTarget == null)
        {
            StopCoroutine(Fire());
            firing = false;
        }
        */
        
    }
}
