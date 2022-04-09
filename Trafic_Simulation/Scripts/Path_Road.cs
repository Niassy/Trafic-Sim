using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class manager path that a vehicle may
// take to reach his destination
public class Path_Road : MonoBehaviour
{
    
    public Transform[] path_positions;

    // Start is called before the first frame update
    void Start()
    {    
    }

    // Update is called once per frame
    void Update()
    {   
    }

    // We  assume that the vehicle is configured in the path
    public void SetNextDestination(Vehicle vehicle)
    {
        // The path start at 0
        // Consider it like the current node position
        int vehicle_path =vehicle.GetNumberPath();

        if (vehicle.path_road== null)
            vehicle.path_road = this;

        int next_path = vehicle_path + 1;


        // Last Path
        if (next_path >= path_positions.Length)
        {
            vehicle.path_end = true;
            return;
        }

        vehicle.number_path = next_path;
        //Debug.Log("Next path " +next_path);
        //Debug.Log("Next path Pos" + this.path_positions[next_path].position);
        vehicle.SetDestination(this.path_positions[next_path].position);
    }


    // Set the next destination of the vehicle
    // we path number
    public void SetNextDestination(Vehicle vehicle,int number_path)
    {
        vehicle.number_path = number_path;
        vehicle.SetDestination(this.path_positions[number_path].position);
        vehicle.path_road = this;
        vehicle.has_path = true;
        vehicle.path_end = false;
    }

    bool IsFiniShed()
    {
        return false;
    }

}






