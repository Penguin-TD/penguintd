using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class TowerPlacement : MonoBehaviour
{
    [SerializeField] public SpriteRenderer rangeSprite;
    [SerializeField] private CircleCollider2D rangeCollider;
    [SerializeField] private Color gray;
    [SerializeField] private Color red;
    [NonSerialized] public bool isPlacing = true;
    [NonSerialized] public int isRestricted = 0;

    private Tower tower;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        tower = GetComponent<Tower>();
        rangeCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlacing)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePosition;
        }

        bool towerOverUI = (Player.main.gameStart && EventSystem.current.IsPointerOverGameObject());

        if (Input.GetMouseButtonDown(0) && !towerOverUI && isRestricted == 0 && tower.cost <= Player.main.money)
        {
            rangeCollider.enabled = true;
            isPlacing = false;
            GetComponent<TowerPlacement>().enabled = false;
            rangeSprite.enabled = false;
            Player.main.money -= tower.cost;
        }

        if (isRestricted > 0)
        {
            rangeSprite.color = red;
        }
        else
        {
            rangeSprite.color = gray;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Tower over something");
        if ((collision.gameObject.tag == "Restricted" || collision.gameObject.tag == "Tower") && isPlacing)
        {
            Debug.Log("Tower over restricted area");
            ++isRestricted;
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Restricted" || collision.gameObject.tag == "Tower") && isPlacing)
        {
            --isRestricted;
        }
    }
}
