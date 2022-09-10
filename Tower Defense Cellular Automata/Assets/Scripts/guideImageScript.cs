using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guideImageScript : MonoBehaviour
{
    public Sprite Goblin;
    public Sprite Skeleton;
    public Sprite Golem;

    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Awake()
    {
        if(this.gameObject.tag == "Mage"){
            spriteRenderer.sprite = Skeleton;
        }
        else if(this.gameObject.tag == "Goblin"){
            spriteRenderer.sprite = Goblin;
        }
        else if(this.gameObject.tag == "Golem"){
            spriteRenderer.sprite = Golem;
        }
    }
}
