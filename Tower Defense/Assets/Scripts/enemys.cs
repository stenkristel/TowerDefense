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

    private Vector3 grassterainP;
    private Vector3 grassterainS;
    private Vector3 mudterainP;
    private Vector3 mudterainS;

    void Start()
    {
        {
            Waypoints = GameObject.Find("Waypoints" + path).transform;
            for (int i = 0; i < Waypoints.childCount; i++)
            {
                cords.Add(Waypoints.GetChild(i).transform);
            }

            grassterainP = GameObject.Find("TerainGrass").transform.position;
            grassterainS = GameObject.Find("TerainGrass").transform.localScale;
            mudterainP = GameObject.Find("TerainMud").transform.position;
            mudterainS = GameObject.Find("TerainMud").transform.localScale;


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


        if (transform.position.x >= grassterainP.x - grassterainS.x / 2  & transform.position.x <= grassterainP.x + grassterainS.x / 2 & transform.position.y >= grassterainP.y - grassterainS.y / 2 & transform.position.y <= grassterainP.y + grassterainS.y / 2)
        {
            speed = 12;
        }
        else if (transform.position.x >= mudterainP.x - mudterainS.x / 2 & transform.position.x <= mudterainP.x + mudterainS.x / 2 & transform.position.y >= mudterainP.y - mudterainS.y / 2 & transform.position.y <= mudterainP.y + mudterainS.y / 2)
        {
            speed = 3f;
        }
        else
        {
            speed = 6;
        }
    }
}
