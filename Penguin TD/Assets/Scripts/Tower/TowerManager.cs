using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TowerManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Towers")] 
    [SerializeField] private GameObject Penguin;
    [SerializeField] private GameObject sniperPenguin;
    [SerializeField] private LayerMask towerLayer;
	[SerializeField] private GameObject towerDetailsPanel;
	[SerializeField] private TextMeshProUGUI towerName;
	[SerializeField] private TextMeshProUGUI towerLevel;
	[SerializeField] private TextMeshProUGUI upgradeCost;
	[SerializeField] private TextMeshProUGUI towerTargetting;
    
    [Header("Game Start")]
    [SerializeField] private GameObject startPromptPanel;
    [SerializeField] private GameObject kingPenguinSelectPanel;
    [SerializeField] private GameObject playerStats;
    [SerializeField] private GameObject shop;
    
    private GameObject selectedTower;
    private GameObject placingTower;
    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClearSelected();
            towerDetailsPanel.SetActive(false);
            ClearPlaced();
        }

        if (placingTower)
        {
            if (!placingTower.GetComponent<TowerPlacement>().isPlacing)
            {
                placingTower = null;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject() && EnemyManager.main.gameStart)
            {
                Debug.Log("Pressing UI element");
                return;
            }
            
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 100f, towerLayer);
            
            if (hit.collider != null)
            {
                if (selectedTower)
                {
                    ClearSelected();
                }
                selectedTower = hit.collider.gameObject;

                if (selectedTower.GetComponent<TowerPlacement>().isPlacing)
                {
                    Debug.Log("Tower Placed");
                    if (selectedTower.name.Replace("(Clone)", "").Trim() == "King Penguin")
                    {
                        startPromptPanel.SetActive(false);
                        kingPenguinSelectPanel.SetActive(false);
                        playerStats.SetActive(true);
                        shop.SetActive(true);
                        EnemyManager.main.wavedone = true;
                        EnemyManager.main.gameStart = true;
                    }
                    return;
                }
                
                selectedTower.GetComponent<TowerPlacement>().rangeSprite.enabled = true;

                Debug.Log("Selecting tower");
                
                towerDetailsPanel.SetActive(true);
                towerName.text = selectedTower.name.Replace("(Clone)", "").Trim();
                towerLevel.text = "Tower LVL: " + (selectedTower.GetComponent<TowerUpgrades>().currentLevel + 1).ToString();
                upgradeCost.text = selectedTower.GetComponent<TowerUpgrades>().currentCost;

                Tower tower = selectedTower.GetComponent<Tower>();
                if (tower.first)
                {
                    towerTargetting.text = "First";
                }
                else if (tower.last)
                {
                    towerTargetting.text = "Last";
                }
                else if (tower.strong)
                {
                    towerTargetting.text = "Strong";
                }
                else
                {
                    towerTargetting.text = "Default";
                }

            }
            else
            {
                Debug.Log("Clicked non tower area");
                if (selectedTower)
                {
                    ClearSelected();
                    towerDetailsPanel.SetActive(false);
                }
            }
        }
    }
    private void ClearPlaced()
    {
        if (placingTower)
        {
            Destroy(placingTower);
            placingTower = null;
        }
    }

    private void ClearSelected()
    {
        if (selectedTower)
        {
            selectedTower.GetComponent<TowerPlacement>().rangeSprite.enabled = false;
            selectedTower = null;
        }
    }

    public void SetTower(GameObject tower)
    {
        ClearPlaced();
        ClearSelected();
        towerDetailsPanel.SetActive(false);
        placingTower = Instantiate(tower);
    }

    public void UpgradeTower()
    {
        if (selectedTower)
        {
            Debug.Log("Upgrading Tower");
            selectedTower.GetComponent<TowerUpgrades>().Upgrade();
            towerLevel.text = "Tower LVL: " + (selectedTower.GetComponent<TowerUpgrades>().currentLevel + 1).ToString();
            upgradeCost.text = selectedTower.GetComponent<TowerUpgrades>().currentCost;
        }
    }

    public void ChangeTargetting()
    {
        if (selectedTower)
        {
            Tower tower = selectedTower.GetComponent<Tower>();

            if (tower.first)
            {
                tower.first = false;
                tower.last = true;
                tower.strong = false;
                towerTargetting.text = "Last";
            }
            else if (tower.last)
            {
                tower.first = false;
                tower.last = false;
                tower.strong = true;
                towerTargetting.text = "Strong";
            }
            else if (tower.strong)
            {
                tower.first = true;
                tower.last = false;
                tower.strong = false;
                towerTargetting.text = "First";
            }
        }
    }
}
