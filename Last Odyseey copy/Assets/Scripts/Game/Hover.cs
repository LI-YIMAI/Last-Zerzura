using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : Singleton<Hover>
{
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update



    // Areference to teh rangedcheck on the tower.
    private SpriteRenderer rangeSpriteRenderer;
    void Start()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.rangeSpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        FollowMouse(); 
    }

    private void FollowMouse()
    {
        if (spriteRenderer.enabled)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }

    }

    public void Activate(Sprite sprite)
    {
        this.spriteRenderer.sprite = sprite;
        spriteRenderer.enabled = true;

        rangeSpriteRenderer.enabled = true;
    }

    public void Deactivate()
    {
        spriteRenderer.enabled = false;
        rangeSpriteRenderer.enabled = false;

        GameManager.Instance.ClickedBtn = null;

        
    }
}
