using UnityEngine;
using System;

public class TowerUpgrades : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [System.Serializable]
    class Level
    {
        public float range = 4f;
        public float damage = 20f;
        public float fireRate = 1f;
		public int cost = 100;
    }
    
    [SerializeField] private Level[] levels = new Level[3];
    [NonSerialized] public int currentLevel = 0;
	[NonSerialized] public string currentCost;
    
    private Tower tower;
    [SerializeField] private TowerRange towerRange;	
    void Awake()
    {
        tower = GetComponent<Tower>();
		currentCost = levels[0].cost.ToString();
    }

    public void Upgrade()
    {
        if (currentLevel < levels.Length && levels[currentLevel].cost <= Player.main.money)
        {
	        Debug.Log("have enough money to upgrade tower");
            tower.range = levels[currentLevel].range;
            tower.damage = levels[currentLevel].damage;
            tower.fireRate = levels[currentLevel].fireRate;
            towerRange.UpdateRange();

			Player.main.money -= levels[currentLevel].cost;

			if(currentLevel >= levels.Length - 1) 
			{
				currentCost = "MAX";
			}
			else 
			{
				currentCost = levels[currentLevel].cost.ToString();
			}

            ++currentLevel;
        }
    }
    void Update()
    {
        
    }
}
