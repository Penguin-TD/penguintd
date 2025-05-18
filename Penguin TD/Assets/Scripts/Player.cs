using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public static Player main;

    public int money = 0;
    [SerializeField] private TextMeshProUGUI moneyGUI;
    void Start()
    {
        main = this;
    }

    // Update is called once per frame
    void Update()
    {
        moneyGUI.text = "Fish: " + money.ToString();
    }
}
