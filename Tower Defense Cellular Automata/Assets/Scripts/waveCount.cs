using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class waveCount : MonoBehaviour
{
    public Text number;

    // Start is called before the first frame update
    void Awake()
    {
        number.text = StatisticsTracker.wave.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
