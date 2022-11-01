using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(destroyself());
    }
    void Update()
    {
        Vector3 scalechange = new Vector3(1f, 1f, 0) * Time.deltaTime;
        gameObject.transform.localScale += scalechange;
    }

    IEnumerator destroyself()
    {
        yield return new WaitForSeconds(0.35f);
        Destroy(gameObject);
    }
}
