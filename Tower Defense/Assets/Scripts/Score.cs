using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    public string selectedTower;

    public int lives = 10;
    public float money;
    public float cost;
    public int totalpaths;
    public int placedpaths;

    public GameObject UIlives;
    public GameObject UImoney;
    public GameObject UIpaths;
    public GameObject UIbuildmode;

    [SerializeField] private GameObject spawnenemybutton;
    [SerializeField] private GameObject spawnwavebutton;
    [SerializeField] private GameObject buypathbutton;
    [SerializeField] private GameObject shop;
    [SerializeField] private GameObject selectedtowerUI;
        [SerializeField] private GameObject Costtext;
        [SerializeField] private GameObject Damagetext;
        [SerializeField] private GameObject Speedtext;
        [SerializeField] private GameObject Rangetext;
        [SerializeField] private GameObject Notestext;

    [SerializeField] private GameObject towerprefab;
    [SerializeField] private GameObject pathspotprefab;
    [SerializeField] private GameObject waypointprefab;
    
    [SerializeField] private GameObject waypoints;
    private GameObject waypoint;

    public bool placementMode;
    public int deadenemies;
    private Ray MouseP;
    private void Awake()
    {
        totalpaths = 30;
    }
    private void Start()
    {
        placementMode = true;
        selectedTower = null;
        selecttower();
    }
    private void Update()
    {
        Buildmode(placementMode);

        textUI();

        if (Input.GetMouseButtonDown(0) & placementMode == true)
        {
            placement();
        }

        if (lives < 1)
        {
            SceneManager.LoadScene("You lose");
        }
    }

    private void Placepathspot(Vector3 orgloc, Vector3 Pathloc, float newpos, bool positive, bool x)
    {
        if (positive == true)
        {
            newpos += pathspotprefab.transform.localScale.x;
        }
        else
        {
            newpos -= pathspotprefab.transform.localScale.x;
        }

        if (x == true)
        {
            Pathloc.x = newpos;
        }
        else
        {
            Pathloc.y = newpos;
        }

        Collider[] colliders = Physics.OverlapBox(Pathloc, pathspotprefab.transform.localScale / 2, pathspotprefab.transform.rotation);
        if (colliders.Length == 0)
        {
            Instantiate(pathspotprefab, Pathloc, pathspotprefab.transform.rotation);
        }
    }

    private void Buildmode(bool buildmode)
    {
        shop.SetActive(buildmode);
        buypathbutton.SetActive(buildmode);
        if (buildmode == true)
        {
            UIbuildmode.GetComponent<TextMeshProUGUI>().text = "Build mode on";
        }
        else
        {
            UIbuildmode.GetComponent<TextMeshProUGUI>().text = "Build mode off";
        }
    }

    private void textUI()
    {
        UIlives.GetComponent<TextMeshProUGUI>().text = lives + " lives";
        UImoney.GetComponent<TextMeshProUGUI>().text = "$" + money;
        UIpaths.GetComponent<TextMeshProUGUI>().text = "paths left: " + totalpaths;
    }

    private void placement()
    {
        MouseP = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(MouseP, out RaycastHit hit);
        if (hit.collider != null)
        {
            if (hit.collider.tag == "towerspot" & selectedTower != null & money >= cost)
            {
                Instantiate(towerprefab, hit.collider.transform.position, hit.collider.transform.rotation);
                hit.collider.tag = "towerspotfilled";
                money -= cost;
            }

            else if (hit.collider.tag == "towerspot" & selectedTower == null)
            {
                Debug.Log("No tower selected");
            }

            else if (hit.collider.tag == "towerspot" & selectedTower != null & money < cost)
            {
                Debug.Log("not enough money!");
            }

            else if (hit.collider.tag == "pathspot" & totalpaths > 0)
            {
                totalpaths -= 1;
                placedpaths += 1;
                waypoint = Instantiate(waypointprefab, hit.collider.transform.position, hit.collider.transform.rotation);
                waypoint.transform.SetParent(waypoints.transform);
                Destroy(hit.collider.gameObject);
                Placepathspot(hit.collider.transform.position, hit.collider.transform.position, hit.collider.transform.position.x, true, true);
                Placepathspot(hit.collider.transform.position, hit.collider.transform.position, hit.collider.transform.position.x, false, true);
                Placepathspot(hit.collider.transform.position, hit.collider.transform.position, hit.collider.transform.position.y, true, false);
                Placepathspot(hit.collider.transform.position, hit.collider.transform.position, hit.collider.transform.position.y, false, false);
            }

            else
            {
                Debug.Log(hit.collider.name);
            }
        }
    }

    public void selecttower()
    {
        if (selectedTower == null)
        {
            Costtext.GetComponent<TextMeshProUGUI>().text = "";
            Damagetext.GetComponent<TextMeshProUGUI>().text = "";
            Speedtext.GetComponent<TextMeshProUGUI>().text = "";
            Rangetext.GetComponent<TextMeshProUGUI>().text = "";
            Notestext.GetComponent<TextMeshProUGUI>().text = "";
        }
        if (selectedTower == "Wizard Tower")
        {
            cost = 5;
            Costtext.GetComponent<TextMeshProUGUI>().text = "5$";
            Damagetext.GetComponent<TextMeshProUGUI>().text = "10 damage";
            Speedtext.GetComponent<TextMeshProUGUI>().text = "1.2 fire rate";
            Rangetext.GetComponent<TextMeshProUGUI>().text = "10 range";
            Notestext.GetComponent<TextMeshProUGUI>().text = "Shoot magic orbs that always hits its target";
        }
        if (selectedTower == "Bomber Tower")
        {
            cost = 10;
            Costtext.GetComponent<TextMeshProUGUI>().text = "10$";
            Damagetext.GetComponent<TextMeshProUGUI>().text = "";
            Speedtext.GetComponent<TextMeshProUGUI>().text = "3.5";
            Rangetext.GetComponent<TextMeshProUGUI>().text = "";
            Notestext.GetComponent<TextMeshProUGUI>().text = "";
        }
        if (selectedTower == "Sniper Tower")
        {
            cost = 7;
            Costtext.GetComponent<TextMeshProUGUI>().text = "7$";
            Damagetext.GetComponent<TextMeshProUGUI>().text = "";
            Speedtext.GetComponent<TextMeshProUGUI>().text = "";
            Rangetext.GetComponent<TextMeshProUGUI>().text = "";
            Notestext.GetComponent<TextMeshProUGUI>().text = "";
        }

    }


}
