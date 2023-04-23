using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject enemyprefab;
    public GameObject startlocation;
    private Score scorescript;
    private GameObject player;
    [SerializeField] private GameObject selectedtoweriamage;
    [SerializeField] private GameObject towerimage;
    private Texture towerimagetexture;
    private Texture selectedtoweriamagetexture;

    [SerializeField] private int waveenemys;
    [SerializeField] private int currentwave;
    [SerializeField] private int wavesLeft;

    [SerializeField] private GameObject uiwave;
    [SerializeField] private GameObject uienemies;
    [SerializeField] private GameObject uitimeuntilwave;

    [SerializeField] private float timeuntiwave;

    private void Start()
    {
        player = GameObject.Find("Player");
        scorescript = player.GetComponent<Score>();
        timeuntiwave = 0;
        currentwave += 1;
        uiwave.GetComponent<TextMeshProUGUI>().text = "Wave: " + currentwave;
        uitimeuntilwave.GetComponent<TextMeshProUGUI>().text = "";
    }
    private void Update()
    {
        uienemies.GetComponent<TextMeshProUGUI>().text = "Enemies left: " + (waveenemys - scorescript.deadenemies);
        if (timeuntiwave > 0)
        {
            timeuntiwave -= Time.deltaTime;
            uitimeuntilwave.GetComponent<TextMeshProUGUI>().text = "Time until wave: " + timeuntiwave;
        }
        else if (timeuntiwave <= 0)
        {
            uitimeuntilwave.GetComponent<TextMeshProUGUI>().text = "";
        }
    }
    public void BuildMode()
    {
        scorescript.placementMode = !scorescript.placementMode;
    }
    public void SpawnOneEnemy()
    {
        Vector3 randomcord = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0);
        Instantiate(enemyprefab, startlocation.transform.position + randomcord, enemyprefab.transform.rotation);
    }

    public void startgame()
    {
        StartCoroutine(Wave());
    }
    IEnumerator Wave()
    {
        uiwave.GetComponent<TextMeshProUGUI>().text = "Wave: " + currentwave;
        for (int i = 0; i < waveenemys; i++)
        {
            SpawnOneEnemy();
            yield return new WaitForSeconds(Random.Range(0.2f, 1f));
        }
        currentwave += 1;
        
        if (wavesLeft > 0)
        {
            StartCoroutine(waitforwave());
        }
    }

    IEnumerator waitforwave()
    {
        timeuntiwave = 20;
        yield return new WaitForSeconds(20f);
        scorescript.deadenemies = 0;
        waveenemys += 1 * currentwave;
        StartCoroutine(Wave());
    }

    public void buypath()
    {
        if(scorescript.money >= 10)
        {
            scorescript.money -= 10;
            scorescript.totalpaths += 1;
        }
        
    }

    public void destroybutton()
    {
        GameObject button = GameObject.Find("Start wave");
        Destroy(button);
    }
}
