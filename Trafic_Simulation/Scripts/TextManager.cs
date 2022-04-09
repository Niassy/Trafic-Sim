using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    public Text m_timer_text;
    public Text m_num_vehicle_text;
    public Text m_veh_per_min_text;

    int m_timer = 0;
    int m_number_loop = 0;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTimer", 0f, 1f);

        InvokeRepeating("UpdateAvgVehiclePerMin", 10f, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateTimer()
    {
        m_timer++;
        m_timer_text.text = m_timer.ToString();
    }

    public void UpdateAvgVehiclePerMin()
    {
        m_number_loop++;

        //int count_vehicle = SpawnManager.GetNumVehicle();

        //int avg_veh = (int)count_vehicle / m_number_loop;

        SpawnManager.SetCumulatedVehiclePerMinute();

        int cumul_vehicle = SpawnManager.GetCumulatedVehiclePerMinute();

        int avg = cumul_vehicle / m_number_loop;
        // loop 1 20 v
        // avg = 20 /loop

        // loop 2 12 v
        // loop 3 14 v


        m_veh_per_min_text.text = avg.ToString();
    }

    public void SetNumVehicle(int number)
    {
        m_num_vehicle_text.text = number.ToString();
    }
}
