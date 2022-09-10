using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxHealth = 100;
    public float currentHealth = 100;
    public float healthBarLength;
    public Texture healthBackground;

    
    void Start()
    {
        healthBarLength = Screen.width / 6;
        GUI.Box(new Rect(0, 0, healthBarLength, Screen.height / 6), healthBackground);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
