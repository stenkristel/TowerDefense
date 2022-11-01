using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public string towertype;
    [SerializeField] float radius;
    public float projSpd;
    public float projDmg;
    public float expdmg;

    public float timeout = 1f;
    [SerializeField] private float currentTimeout;

    public GameObject target;
    [SerializeField] private GameObject Sphereprefab;

    private Score scorescript;
    private GameObject player;

    public float explosionrange;

    void Start()
    {
        player = GameObject.Find("Player");
        scorescript = player.GetComponent<Score>();
        currentTimeout = 0;
        explosionrange = 3.5f;
        expdmg = 5;

        //dmg, spd, range, prjsd
        if (scorescript.selectedTower == "Wizard Tower")
        {
            towertype = "wizard";
            stats(10, 1.2f, 10, 5);
        }
        if (scorescript.selectedTower == "Bomber Tower")
        {
            towertype = "bomber";
            stats(0, 2.7f, 5, 15);
        }
        if (scorescript.selectedTower == "Sniper Tower")
        {
            towertype = "sniper";
            stats(15, 3.5f, 20, 50);
        }
    }

    void Update()
    {
        if (scorescript.placementMode == false)
        {

            if (currentTimeout >= 0)
                currentTimeout -= Time.deltaTime;

            if (target)
            {
                if (currentTimeout <= 0)
                {
                    Shoot();
                    checkenemyoutofrange();
                }
            }

            else
            {
                GetClosestEnemy();
            }
        }
        
        
    }
    private void Shoot()
    {
        //Vector3 prjloc = transform.position;
        //prjloc.z -= 2f;
        Instantiate(Sphereprefab, transform.position, transform.rotation, transform);
        currentTimeout = timeout;
    }

    private void checkenemyoutofrange()
    {
        if (radius < Vector2.Distance(target.transform.position, transform.position))
        {
            target = null;
        }
    }

    private void GetClosestEnemy()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        if (colliders.Length == 0)
            return;
        Collider closestCollider = colliders[0];
        float currentClosest = float.MaxValue;
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("enemy"))
            {
                float distance = Vector3.Distance(collider.transform.position, transform.position);
                if(distance < currentClosest)
                {
                    closestCollider = collider;
                    target = closestCollider.gameObject;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("grass"))
        {
            //Debug.Log("Tower placed in grass: X2 range!");
            //radius *= 2f;
        }

        if (other.gameObject.tag.Equals("mud"))
        {
            //Debug.Log("Tower placed in mud: X0.5 damage!");
            //projDmg /= 2;
            
        }
    }

    public void stats(float dmg, float spd, float rad, float prjspd)
    {
        projDmg = dmg;
        timeout = spd;
        radius = rad;
        projSpd = prjspd;
    }
}
