using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private bool debuff_status = false;
    private Stack<Node> path;
    private float inital_speed;
    public Point GridPosition { get; set; }

    private Vector3 destination;
    private SpriteRenderer spriteRenderer;
    public bool IsActive { get; set; }
    public bool Alive
    {
        get
        {
            return health.CurrentValue > 0;
        }
    }
    private Animator myAnimator;
    [SerializeField]
    private Stat health;
    private void Awake()
    {
        //sets up references to the components
        myAnimator = GetComponent<Animator>();
        health.Initialize();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        if (speed == 2)
        {
            inital_speed = 2;
        }
        if(speed == 1)
        {
            inital_speed = 1;
        }
    }
    private void Update()
    {
        // been called per frame
        Move();
    }
    public void spawn(int health)
    {
        // set the start spqwn position
        transform.position = LevelManager.Instance.BluePortal.transform.position;
        this.health.Bar.Reset();
        this.health.MaxVal = health;
        this.health.CurrentValue = this.health.MaxVal;
        // get the animator component
        //myAnimator = GetComponent<Animator>();
        // starts to scale the monster 
        StartCoroutine(Scale(new Vector3(0.1f, 0.1f), new Vector3(1, 1), false));
        // once finished scaling the monster, we set the monster path which will be passed from LevelManager
        SetPath(LevelManager.Instance.Path);
    }

    public IEnumerator Scale(Vector3 from, Vector3 to, bool remove)
    {
        //IsActive = false;
        float progress = 0;

        while (progress<=1)
        {
            //lerp is chaging the size over timel
            transform.localScale = Vector3.Lerp(from, to, progress);

            progress += Time.deltaTime;

            yield return null;
        }
        transform.localScale = to;
        IsActive = true;
        if (remove)
        {
            Release();
            
        }
    }

    private void Move()
    {
        // isactive to check if the moster has been slaced succesfully 
        if (IsActive)
        {
            // set the position of monster from start point to destination point with specific speed 
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);

            if (transform.position == destination)
            {
                if (path != null && path.Count > 0)
                {
                    Animate(GridPosition, path.Peek().Gridposition);
                    // we just wannna use it's node and get the Gridposition

                    //used to retrieve or fetch the first element of the Stack or the element present at the top of the Stack.
                    //The element retrieved does not get deleted or removed from the Stack.
                    GridPosition = path.Peek().Gridposition;
                    //The element is popped from the top of the stack and is removed from the same.
                    destination = path.Pop().WorldPosition;
                }
            }
        }
       
    }

    private void SetPath(Stack<Node> newPath)
    {
        if (newPath != null)
        {
            this.path = newPath;
            Animate(GridPosition, path.Peek().Gridposition);
            //used to retrieve or fetch the first element of the Stack or the element present at the top of the Stack.
            //The element retrieved does not get deleted or removed from the Stack.
            GridPosition = path.Peek().Gridposition;
            //The element is popped from the top of the stack and is removed from the same.
            destination = path.Pop().WorldPosition;
        }
    }

    private void Animate(Point currentPos, Point newPos)
    {
        if (currentPos.Y > newPos.Y)
        {
            // moving down
            myAnimator.SetInteger("Horizontal", 0);
            myAnimator.SetInteger("Vertical", 1);
        }
        else if (currentPos.Y < newPos.Y)
        {
            //moving up
            myAnimator.SetInteger("Horizontal", 0);
            myAnimator.SetInteger("Vertical", -1);
        }
        if (currentPos.Y == newPos.Y)
        {
            if (currentPos.X > newPos.X)
            {
                //moving left
                myAnimator.SetInteger("Horizontal", -1);
                myAnimator.SetInteger("Vertical", 0);
            }
            else if (currentPos.X < newPos.X)
            {
                //moving right
                myAnimator.SetInteger("Horizontal", 1);
                myAnimator.SetInteger("Vertical", 0);
            }
        }
    }

    // if the mosnter hit the protal with tag "RedPortal"
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "RedPortal")
        {
            // pass true and release the monster 
            StartCoroutine(Scale(new Vector3(1, 1), new Vector3(0.1f, 0.1f),true));
        }
        if (other.tag == "Tile")
        {
            spriteRenderer.sortingOrder = other.GetComponent<TileScript>().GridPosition.Y;
        }
    }

    public void Release()
    {
        
        IsActive = false;
        //set correct start position
        GridPosition = LevelManager.Instance.BlueSpawn;
        //remove monster from the game
        GameManager.Instance.Pool.ReleaseObject(gameObject);
        //Releases the object in the object pool
        GameManager.Instance.Removemonster(this);
    }
    //taking damage
    public void TakeDamage(int damage,int count)
    {
        if (IsActive)
        {
            health.CurrentValue -= damage;
            if (inital_speed == 2)
            {
                if (speed > 1.4)
                {
                    if (!debuff_status)
                    {
                        float temp = speed;
                        Debug.Log(count);
                        speed = speed - 0.3f * count;
                        debuff_status = true;
                        Debug.Log(speed);
                        StartCoroutine(StartCountdown());
                        //wait(temp);
                        //speed = temp;
                        //debuff_status = false;
                    }
                }
            }
            if (inital_speed == 1)
            {
                if (speed > 0.6)
                {
                    if (!debuff_status)
                    {
                        float temp = speed;
                        Debug.Log(count);
                        speed = speed - 0.2f * count;
                        debuff_status = true;
                        Debug.Log(speed);
                        StartCoroutine(StartCountdown());
                        //wait(temp);
                        //speed = temp;
                        //debuff_status = false;
                    }
                }
            }
            
            

            if (health.CurrentValue <= 0)
            {
                GameManager.Instance.Gold += 2;
                // go to death state
                myAnimator.SetTrigger("Die");
                IsActive = false;
                GetComponent<SpriteRenderer>().sortingOrder--;
            }
        }
        
    }
    float currCountdownValue;
    public IEnumerator StartCountdown(float countdownValue = 3)
    {
        currCountdownValue = countdownValue;
        while (currCountdownValue > 0)
        {
            Debug.Log("Countdown: " + currCountdownValue);
            
            currCountdownValue--;
            if (currCountdownValue == 0)
            {
                debuff_status = false;
                speed = inital_speed;
            }
            yield return new WaitForSeconds(1.0f);
        }
    }
    //public void wait(float temp)
    //{
    //    float timeLeft = 3.0f;
    //    timeLeft -= Time.deltaTime;
    //    if (timeLeft < 0)
    //    {
    //        speed = temp;
    //        debuff_status = false;
    //    }


    //}
}
