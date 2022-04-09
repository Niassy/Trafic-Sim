using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{

    // Must go on vehicle
    #region Movement

    //Transform destination;
    [SerializeField]
    bool is_moving;

    [SerializeField]
    Vector3 dest;

    //[HideInInspector]
    public bool has_path;

    public bool path_end;

    // The path to use
    [HideInInspector]
    public Path_Road path_road;

    // The current path to use
    //[HideInInspector]
    public int number_path;

    #endregion

    Rigidbody2D rb;

    Vector2 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        is_moving = false;
        path_end = false;
        //dest = Vector3.zero;

    }

    // Update is called once per frame
    void Update()
    {
        #region PathPlanning
        // Means the vehicle has a path
        // It is configured by the spawnmanager::ConfigurePath
       
        if (has_path)
        {
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            is_moving = true;
        }

        if (is_moving)
        {
            //Debug.Log("Moving to "+dest);
            MoveTo(dest);

            if (Vector3.Distance(transform.position, dest) < 0.01f)
            {
                if (has_path)
                {
                    this.path_road.SetNextDestination(this);
                }
                else
                    is_moving = false;

                if (path_end)
                {
                    is_moving = false;
                    Destroy(this.gameObject,2f);

                    SpawnManager.DecrementNumVehicle();

                    FindObjectOfType<TextManager>().SetNumVehicle(SpawnManager.GetNumVehicle());

                }

                //is_moving = false;
            }


        }

        #endregion
    }

    void MoveTo(Vector3 destination)
    {

        float speed = 1f;
        float t = Time.deltaTime;


        // The distance per frame
        float dist_per_frame = speed * t;
        

        transform.position = Vector3.MoveTowards(transform.position,
        destination, Time.deltaTime);
    }

    Vector3 Seek(Vector3 target,float max_speed,Vector3 velocity)
    {

        Vector3 desired_vel = target - transform.position;
        desired_vel.Normalize();
        desired_vel*= max_speed;

        return desired_vel - velocity;

    }

    void FollowLeader(Vector3 leader_vel,Vector3 leader_pos,float speed,float max_speed)
    {

        // Get the previous velocity
        

        float BEHIND_DIST = 2f;
        Vector3 tv = new Vector3(leader_vel.x, leader_vel.y, leader_vel.z);
        tv.Normalize();
        tv *= -1;

        // destination to arrive
        Vector3 behind_pos = leader_pos + tv;

        // Now we have to compute the speed at each call
        // Since Vector.MoveToward does not give us information
        // about the velocity

        speed+=Time.deltaTime;

        //if ( speed >max_speed )

        //float t = Time.deltaTime;

        // The distance per frame
       // float dist_per_frame = speed * t;


        //float dist_behind = Mathf.Abs( behind_pos.x - transform.position.x) ;
        //float expected_speed = dist_behind / t;


       // transform.position = Vector3.MoveTowards(transform.position,
       //behind_pos, dist_per_frame);
        

    }

    void FollowLeader(float leader_speed,Vector3 leader_pos)
    {

        // Determine behind point

        float BEHIND_DIST = 2f;
        //Vector3 tv = new Vector3(leader_pos.x, leader_pos.y, leader_pos.z);
        //tv *= -1;
        //tv.Normalize();


        // Assume it move only on a direcrion(x or y)
        // Speed can be one vector

        float tv = leader_speed * -1;
        leader_speed *= BEHIND_DIST;


        // destination to seek
        Vector3 behind_pos = leader_pos + new Vector3(leader_speed, 0, 0);


        //float speed = 1f;
        //float t = Time.deltaTime;

        //float dist_behind = Mathf.Abs( behind_pos.x - transform.position.x) ;
        //float expected_speed = dist_behind / t;


        //float max_speed = 3;

        //float speed = ;



       // transform.position = Vector3.MoveTowards(transform.position,
       //behind_pos, s);
    }

    void FollowPath(Path_Road path)
    {

    }

    private void FixedUpdate()
    {
        if (is_moving == true)
        {
            //Debug.Log("Mouse Pos" + mousePos);
            //Vector2 lookDir = mousePos - rb.position;

            Vector2 lookDir = new Vector2( dest.x,dest.y) - rb.position;


            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 180;
            //Debug.Log("Look Dir " + lookDir);
            rb.rotation = angle;
        }
    }

    #region Setters_Getters

    public void SetDestination(Vector3 dest)
    {
        this.dest = new Vector3(dest.x, dest.y, 0);

        //Debug.Log("Destination "+this.dest);
    }

    public void SetMoving(bool move)
    {
        this.is_moving = move;
    }

    public bool IsMoving() { return this.is_moving; }

    #endregion

    public Vector3 GetDestination() { return this.dest; }

    public int GetNumberPath() { return number_path; }

}
