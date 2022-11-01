using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public string projectiletype;

    Tower towerscript;
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private GameObject Target;
    private Vector3 targetloc;

    private Score scorescript;
    private GameObject player;
    [SerializeField] private GameObject explosionprefab;
    float damagemodifer;
    private void Awake()
    {
        towerscript = GetComponentInParent<Tower>();
        speed = towerscript.projSpd;
        damage = towerscript.projDmg;
        Target = towerscript.target;
        projectiletype = towerscript.towertype;
    }
    private void Start()
    {
        targetloc = Target.transform.position;
        player = GameObject.Find("Player");
        scorescript = player.GetComponent<Score>();
    }

    private void Update()
    {
        movetotarget();
    }

    private void movetotarget()
    {
        if (scorescript.placementMode == false)
        {
            if (projectiletype != "wizard")
            {
                transform.position = Vector3.MoveTowards(transform.position, targetloc, speed * Time.deltaTime);
                if (targetloc == transform.position)
                {
                    Destroy(gameObject);
                    if (projectiletype == "bomber")
                    {
                        boom();
                    }
                }
            }
            else if (projectiletype == "wizard" & Target)
            {
                transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, speed * Time.deltaTime);
            }
            else
            {
                Destroy(gameObject);
            }
            
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        if (projectiletype != "bomber")
        {
            GameObject hitObj = collision.gameObject;
            if (hitObj.gameObject.tag == "enemy")
            {
                enemys enemyScript = hitObj.GetComponent<enemys>();
                enemyScript.takeDamage(damage);
            }
        }
        else
        {
            boom();
        }


    }
    private void boom()
    {
        Debug.Log("boom");
        Vector3 boomloc = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 1);
        Instantiate(explosionprefab, boomloc, gameObject.transform.rotation);

        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, towerscript.explosionrange);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].tag == "enemy")
            {
                colliders[i].GetComponent<enemys>().takeDamage(towerscript.expdmg);
            }
                
        }
    }
}
