using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healtbar : MonoBehaviour
{

    enemys parentscirpt;
    private float maxhealth;
    private void Start()
    {
        parentscirpt = GetComponentInParent<enemys>();
        maxhealth = parentscirpt.health;
    }
    private void Update()
    {
        gameObject.transform.localScale = new Vector3(parentscirpt.health / maxhealth, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
    }

}
