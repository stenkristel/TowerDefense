using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButtons : MonoBehaviour
{
    private Score scorescript;
    private GameObject player;

    private GameObject selectedtowerimage;
    [SerializeField] private GameObject towerimage;
    private Texture selectedtoweriamagetexture;

    private void Start()
    {
        player = GameObject.Find("Player");
        scorescript = player.GetComponent<Score>();

        towerimage = GameObject.Find("SelectedTowerImage");

    }
    public void selectTower()
    {
        selectedtowerimage = transform.parent.gameObject;
        scorescript.selectedTower = selectedtowerimage.name;
        scorescript.selecttower();
        selectedtoweriamagetexture = selectedtowerimage.GetComponent<RawImage>().texture;
        towerimage.GetComponent<RawImage>().texture = selectedtoweriamagetexture;
    }
}
