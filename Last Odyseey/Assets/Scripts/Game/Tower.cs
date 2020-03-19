using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    private SpriteRenderer mySpriteRenderer;


    private Monster target;

    private Queue<Monster> monsters = new Queue<Monster>();

    // Start is called before the first frame update
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        Debug.Log(target);
    }

    public void Select() {
        mySpriteRenderer.enabled = !mySpriteRenderer.enabled;
    }

    public void Attack() {

        if(target == null && monsters.Count > 0)
        {
            target = monsters.Dequeue();
            
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Monster") {
           monsters.Enqueue(other.GetComponent<Monster>());
           
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Monster")
        {

            target = null;
        }

    }
}
