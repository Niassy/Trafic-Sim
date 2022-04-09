using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{

    public GameObject m_prefab_vehicle;
    public Transform m_spawnPoint;

    [SerializeField]
    Path_Road[] array_path;

    public float spawn_time = 4f;

    public static int s_Num_Vehicle = 0;

    // Max Vehicle spawned in each Loop
    public static int s_max_vehicle;

    // Cumulated vehicle per minite
    public static int s_cumul_vehicle_min;



    // Logic
    // When an agent is spawn at a position,it is
    // directly configured with a path to follow

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnVehicle", 2f, spawn_time);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Instatiate()
    {

    }

    void ConfigurePath(Vehicle vehicle,Path_Road path,bool loop_path)
    {

    }

    void SpawnVehicle()
    {
        GameObject vehicle = Instantiate(m_prefab_vehicle, m_spawnPoint.position,
            Quaternion.identity);

        Vehicle vehicle_script = vehicle.GetComponent<Vehicle>();

        Path_Road path_road = this.array_path[0];

        // Configure the vehicle
        path_road.SetNextDestination(vehicle_script, 0);

        StartCoroutine(IE_ActivateVehicle(vehicle_script));

        SpawnManager.IncrementNumVehicle();

        FindObjectOfType<TextManager>().SetNumVehicle(SpawnManager.GetNumVehicle());


    }


    public static void SetCumulatedVehiclePerMinute ()
    {
        int num = SpawnManager.GetNumVehicle();
        SpawnManager.s_cumul_vehicle_min += num;

    }

    IEnumerator IE_ActivateVehicle(Vehicle vehicle)
    {
        yield return new WaitForSeconds(1f);

        vehicle.SetMoving(true);
    }


    public static void IncrementNumVehicle()
    {
        SpawnManager.s_Num_Vehicle++;
    }

    public static void DecrementNumVehicle()
    {
        SpawnManager.s_Num_Vehicle--;
    }


    public static int GetNumVehicle()
    {
        return SpawnManager.s_Num_Vehicle;
    }

    public static int GetCumulatedVehiclePerMinute()
    {
        return SpawnManager.s_cumul_vehicle_min;
    }
}
