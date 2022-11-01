using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    Ray mousep;
    Vector3 mouseP;
    [SerializeField] private GameObject sphereprefab;

    void Update()
    {
        mouseP = Input.mousePosition;
        //maakt een ray vanaf 0.0.0 naar de muis positie
        //Physics.Raycast(Vector3.zero, mouseP);
        //Debug.DrawRay(Vector3.zero, mouseP, Color.cyan);

        //maakt een ray vanaf een het object naar de muispositie
        //Physics.Raycast(transform.position, mouseP);
        //Debug.DrawRay(transform.position, mouseP, Color.red);






        if (Input.GetMouseButtonDown(0))
        {
            Physics.Raycast(mousep, out RaycastHit hit);
            Debug.Log(hit.collider.name + " is op locatie " + hit.collider.transform.position);
            Instantiate(sphereprefab, mouseP, hit.transform.rotation);
        }
        
        

        mousep = Camera.main.ScreenPointToRay(Input.mousePosition);
    }
}
