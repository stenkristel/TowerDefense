using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathspot : MonoBehaviour
{
    [SerializeField] private GameObject nopathprefab;
    [SerializeField] private GameObject player;
    private Score playerscript;
    private int paths;
    private void Start()
    {
        player = GameObject.Find("Player");
        playerscript = player.GetComponent<Score>();
        paths = playerscript.placedpaths;
    }

    private void Update()
    {
        if (paths != playerscript.placedpaths)
        {
            nopathplace();
        }

        if (playerscript.placementMode == true & playerscript.totalpaths > 0)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    private void nopathplace()
    {
        Instantiate(nopathprefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }


}
