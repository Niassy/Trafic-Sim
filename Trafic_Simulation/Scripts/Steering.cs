using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering : MonoBehaviour
{

    public Transform destination;
    public float max_speed;
    Vector3 velocity;

    bool start;


    public Vector3 offset;

    public enum Steer
    {
        None,
        Arrive,
        OffsetPursuit
    }

    public Steer steer;

    public enum Deceleration
    {
        slow = 3,
        normal = 2,
        fast = 1
    }


    #region OffsetPursuit

    public Vector3 m_otherPos;
    public Vector3 m_offsetPos;
    public Vector3 m_other_velocity;
    public float m_look_ahead_time;
    public float m_to_offsetLength;

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        //max_speed = 2f;
        start = false;
        velocity = Vector3.zero;

        StartCoroutine(IE_Start());
    }


    IEnumerator IE_Start()
    {

        yield return new WaitForSeconds(3f);
        start = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (start == true)
        {
            Vector3 seek = Vector3.zero;

            if (steer == Steer.Arrive)
                seek = Arrive(this.destination.position, Deceleration.normal);

            else if (steer == Steer.OffsetPursuit)
                seek = OffsetPursuit(this.offset, destination.gameObject.GetComponent<Steering>());


            //Vector3 seek = Arrive(this.destination.position,Deceleration.normal);

            this.velocity += seek;

            if (this.velocity.magnitude > max_speed)
            {
                this.velocity.Normalize();
                this.velocity *= max_speed;

            }

             transform.position += velocity * Time.deltaTime;

            
            /*float speed = Mathf.Abs(velocity.x);

            transform.position = Vector3.MoveTowards(transform.position,
            destination.position,  speed *Time.deltaTime);
            */
        }
    }

    Vector3 Seek(Vector3 target)
    {

        Vector3 desired_vel = target - transform.position;
        desired_vel.Normalize();
        desired_vel *= this.max_speed;

        return desired_vel - velocity;

    }

    Vector3 Arrive(Vector3 target,Deceleration deceleration)
    {
        Vector3 toTarget = target - transform.position;
        float dist = toTarget.magnitude;

        if (dist > 0)
        {
            float decel_tweaker = 0.3f;
            float speed = dist / ((float)deceleration * decel_tweaker);

            if (speed >= max_speed)
                speed = max_speed;


            Vector3 desired_vel = toTarget * speed / dist;

            return desired_vel - this.velocity;

        }

        return Vector3.zero;
    }

    Vector3 OffsetPursuit(Vector3 offset,Steering other)
    {

        Vector3 OffsetPos = offset + other.transform.position;
        Vector3 toOffset = OffsetPos - transform.position;

        float lookAheadTime = toOffset.magnitude / (max_speed + other.velocity.magnitude);

        this.m_otherPos = other.transform.position;
        this.m_look_ahead_time = lookAheadTime;
        this.m_offsetPos = OffsetPos;
        this.m_other_velocity = new Vector3(other.velocity.x, 0, 0);
        this.m_to_offsetLength = toOffset.magnitude;


        return Arrive(OffsetPos  +new Vector3(  other.velocity.x,0,0) * lookAheadTime, Deceleration.fast);

    }
}
