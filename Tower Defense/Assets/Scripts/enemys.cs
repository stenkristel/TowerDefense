using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemys : MonoBehaviour
{
    List<Transform> cords = new List<Transform> ();
    public Transform Waypoints;
    private int at = 0;
    public int path;
    private Vector3 randomcord;

    public float speed;
    public float health;
    public Vector3 cubeloc;

    private GameObject player;
    private Score scorescript;
    private GameObject panel;
    private SpawnEnemy uiscript;
    [SerializeField] GameObject healtbar;


    private void Awake()
    {
        health = 15;
    }
    void Start()
    {
        {

            player = GameObject.Find("Player");
            scorescript = player.GetComponent<Score>();
            panel = GameObject.Find("Panel");
            uiscript = panel.GetComponent<SpawnEnemy>();
            speed = 6f;
            findwaypoints();

            randomcord = gameObject.transform.position - uiscript.startlocation.transform.position;

            Quaternion hprot = Quaternion.identity;
            hprot.eulerAngles = new Vector3(-90, 0, 0);
            Instantiate(healtbar, transform.position, hprot, transform);
        }
    }

    void Update()
    {
        if (health <= 0)
        {
            scorescript.money += 2;
            die();
        }

        if (scorescript.placementMode == false)
        {
            movetowaypoint();
        }
    }
    private void die()
    {
        scorescript.deadenemies += 1;
        Destroy(gameObject);
    }

    private void findwaypoints()
    {
        Waypoints = GameObject.Find("Waypoints" + path).transform;
        for (int i = 0; i < Waypoints.childCount; i++)
        {
            cords.Add(Waypoints.GetChild(i).transform);
        }
    }

    private void movetowaypoint()
    {
        
        
        if (transform.position != cords[at].position + randomcord)
        {
            transform.position = Vector3.MoveTowards(transform.position, cords[at].position + randomcord, speed * Time.deltaTime);
        }
        else if (cords.Count > at + 1)
        {
            at++;
        }
        else
        {
            scorescript.lives -= 1;
            die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("grass"))
        {
            speed = 12;
        }
        else if (other.gameObject.tag.Equals("mud"))
        {
            speed = 3;
        }
    }

    public void takeDamage(float dmg)
    {
        Debug.Log("damage = " + dmg);
        health -= dmg;
    }
}
