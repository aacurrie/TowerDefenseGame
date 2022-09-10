using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMusic : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        if (SceneManager.GetActiveScene().name == "Chunk Generation")
        {
            Destroy(this.gameObject);
        }
    }

}
