using UnityEngine;

public class KingAttackController : MonoBehaviour, IAttackController
{
    private Tower _tower;
    private GameObject _target;

    public Tower Tower
    {
        get => _tower;
        set => _tower = value;
    }

    public GameObject Target
    {
        get => _target;
        set => _target = value;
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
