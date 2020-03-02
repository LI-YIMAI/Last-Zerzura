using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Stack<Node> path;

    public Point GridPosition { get; set; }

    private Vector3 destination;

    public bool IsActive { get; set; }

    private void Update()
    {
        // been called per frame
        Move();
    }
    public void spawn()
    {
        // set the start spqwn position
        transform.position = LevelManager.Instance.BluePortal.transform.position;
        // starts to scale the monster 
        StartCoroutine(Scale(new Vector3(0.1f, 0.1f), new Vector3(1, 1)));
        // once finished scaling the monster, we set the monster path which will be passed from LevelManager
        SetPath(LevelManager.Instance.Path);
    }

    public IEnumerator Scale(Vector3 from, Vector3 to)
    {
        IsActive = false;
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
            //used to retrieve or fetch the first element of the Stack or the element present at the top of the Stack.
            //The element retrieved does not get deleted or removed from the Stack.
            GridPosition = path.Peek().Gridposition;
            //The element is popped from the top of the stack and is removed from the same.
            destination = path.Pop().WorldPosition;
        }
    }
}
