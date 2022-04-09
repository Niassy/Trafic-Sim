using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Vehicle : MonoBehaviour
{
    public Transform destination;


    #region QLearning
    // ------------- QLEARNING ---------------//

    // Learning to drive section
    // Here we use QLearning to teach agent how to drive


    int[] states;
    int[] actions;


    // Possible action
    List<int[]> possible_actions;

    int state_space;
    int action_space;

    int distance_space;

    // Getting the max distance from target
    float max_distance;


    //----------------END Q LEARNING---------------------//


    #endregion


    // Must go on vehicle
    #region Movement

    bool is_moving;

    Vector3 dest;
    #endregion


    Rigidbody2D rb;

    Vector2 mousePos;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        is_moving = false;
        dest = Vector3.zero;

    }
    
    // Given the repective positions of agent and target,
    // the heading vector of the agent and it speed,
    // This function convert the parameter belowed into space
    // of state
    // The state is a combinaison of the heading vector(velocity)
    // and the ratio distance/max_distance splitted into numbers 
    void FeaturiseToState(Vector3 posAgent,Vector3 posTarget,
        int agentSpeed,Vector3 headingAgent)
    {
        //this.max_distance

        //float dist ) 
        Vector3 dirMove = (posTarget - posAgent);

        // Normalise the vector
        dirMove.Normalize();



    }

    // Update is called once per frame
    void Update()
    {
        //transform.position =  Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y),
        //   new Vector2(destination.position.x, destination.position.y), 1f * Time.deltaTime);


        //transform.Rotate(new Vector3(0, 0, 90) * Time.deltaTime);

        //transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime);

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        //Vector3dest = Vector3.zero;
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Mouse Hit");
            Vector3 world_point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dest = world_point;
            dest.z = 0;

            Debug.Log("Destination "+dest);
            is_moving = true;

            Vector2 TargetDir = (dest - transform.position);
            TargetDir.Normalize();

           // Debug.Log("TargetDir " + TargetDir);

            //Debug.Log(transform.forward);
            //float angleDir = Vector2.Angle(TargetDir, );

            //Debug.Log("Angle " + angleDir);
        }


        if (is_moving)
        {
            //Debug.Log(dest);
            MoveTo(dest);

            if (Vector3.Distance( transform.position,dest ) < 0.01f )
            {
                is_moving = false;
            }


        }

    }

    void MoveTo(Vector3 destination)
    {

        //transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y),
          // new Vector2(destination.x, destination.y), 1f * Time.deltaTime);

        transform.position = Vector3.MoveTowards(transform.position,
        destination,   Time.deltaTime);


        // See Project
        // UVS ENSIEE

        //Vector3 world_point = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //destination = world_point;
    }

    //Vector3 LeaderFollow(Vehicle leader)
    //{

    //    Vector3 tv =leader.GetDestination 

    //    var tv      :Vector3D = leader.velocity.clone();
    //    var force   :Vector3D = new Vector3D();

    //    // Calculate the behind point
    //    tv.scaleBy(-1);
    //    tv.normalize();
    //    tv.scaleBy(LEADER_BEHIND_DIST);
    //    behind = leader.position.clone().add(tv);

    //    // Creates a force to arrive at the behind point
    //    force = force.add(arrive(behind));

    //    return force;

    //}

    private void FixedUpdate()
    {
        if (is_moving == true)
        {
            //Debug.Log("Mouse Pos" + mousePos);
            Vector2 lookDir = mousePos - rb.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 180;
            //Debug.Log("Look Dir " + lookDir);
            rb.rotation = angle;
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision");
    }

    // Learning to drive

    // Learning to avoid obstacle

    // Path Following

    // Leader Following



}
