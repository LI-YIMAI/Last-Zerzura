using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    // This is the projectiles type
    [SerializeField]
    private string projectileType;

    [SerializeField]
    private float projectileSpeed;

    public float ProjectileSpeed { get { return projectileSpeed; } }

    private SpriteRenderer mySpriteRenderer;


    private Animator myAnimator;
    [SerializeField]
    private int damage;
    //the price set from tile script when palcing tower 
    public int Price { get; set; }
    //The upgrade count set from tile script when placing tower ; default number 2
    //each time for upgrading, the Count number will + 1;
    public int Count { get; set; }
    private Monster target;

    public Monster Target { get { return target; } }



    private Queue<Monster> monsters = new Queue<Monster>();

    private bool canAttack = true;


    private float attackTimer;

    [SerializeField]
    private float attackCooldown;
    // Start is called before the first frame update
    void Awake()
    {

        myAnimator = transform.parent.GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        //        Debug.Log(target);
    }

    public void Select() {
        mySpriteRenderer.enabled = !mySpriteRenderer.enabled;
    }
    public int Damage
    {
        get
        {
            return damage;
        }
        set
        {
            this.damage = value;
        }
    }
    public void Attack() {

        if (!canAttack)//If we can't attack
        {
            //Count how much time has passed since last attack
            attackTimer += Time.deltaTime;

            //If the time passed is higher than the cooldown, then we need to reset
            //and be able to attack again
            if (attackTimer >= attackCooldown)
            {
                canAttack = true;
                attackTimer = 0;
            }
        }
        if (target == null && monsters.Count > 0)
        {
            target = monsters.Dequeue();
            
        }

        if (target != null && target.IsActive)
        {
            if (target != null && target.IsActive)//If we have a target that is active
            {
                if (canAttack)//If we can attack then we shoot at the target
                {
                    Shoot();
                    myAnimator.SetTrigger("Attack");
                    canAttack = false;
                }

            }
        }
        else if (monsters.Count>0)
        {
            target = monsters.Dequeue();
        }
        if(target!=null && !target.Alive || target!=null&&!target.IsActive)
        {
            target = null; 
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
    private void Shoot() {

        Projectile projectile = GameManager.Instance.Pool.GetObject(projectileType).GetComponent<Projectile>();
        projectile.transform.position = transform.position;

        projectile.Initialize(this);
    }
}
