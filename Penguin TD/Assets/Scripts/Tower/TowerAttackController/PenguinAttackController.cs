using UnityEngine;

public class PenguinAttackController : MonoBehaviour, IAttackController
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
    private float cooldown = 0f;
    void Start()
    {
        
    }

    void Update()
    {
        if(_target) {
            transform.right = _target.transform.position - transform.position;
            if(cooldown >= _tower.fireRate) {
                _target.GetComponent<Enemy>().Damage(_tower.damage);
                cooldown = 0.0f;
            }
            else {
                cooldown += Time.deltaTime;
            }
            
        }
    }
}
