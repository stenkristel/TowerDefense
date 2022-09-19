using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemys : MonoBehaviour
{
    List<Transform> cords = new List<Transform> ();
    public Transform Waypoints;
    private int at = 0;
    public int path;

    public float speed;
    public Vector3 cubeloc;


    void Start()
    {
        speed = 4;
        {
            Waypoints = GameObject.Find("Waypoints" + path).transform;
            for (int i = 0; i < Waypoints.childCount; i++)
            {
                cords.Add(Waypoints.GetChild(i).transform);
            }
        }
    }

    void Update()
    {



        if (transform.position != cords[at].position)
        {
            
            transform.position = Vector3.MoveTowards(transform.position, cords[at].position, speed * Time.deltaTime);
        }
        else if (cords.Count > at + 1)
        {
            at++;
        }
        else
        {
            Destroy(gameObject);
        }






    }
}
