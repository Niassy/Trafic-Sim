using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_Controller : MonoBehaviour
{
    [SerializeField]
    Sprite m_red_sprite;

    [SerializeField]
    Sprite m_green_sprite;

    [SerializeField]
    Sprite m_yellow_sprite;

    [SerializeField]
    SpriteRenderer m_sp_renderer;

    //public enum Color
    //{
    //    Red,
    //    Green,
    //    Yellow
    //}

    //Color m_current_color;


    // Start is called before the first frame update
    void Start()
    {
        m_sp_renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchToColor(Color color)
    {
    }


    public void SetRed()
    {
        m_sp_renderer.sprite = m_red_sprite;
    }

    public void SetGreeen()
    {
        m_sp_renderer.sprite = m_green_sprite;
    }

    public void SetYellow()
    {
        m_sp_renderer.sprite = m_yellow_sprite;
    }
}
