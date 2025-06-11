using UnityEngine;

public class SniperAttackController : MonoBehaviour, IAttackController
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

    // Update is called once per frame
    void Update()
    {
        if(_target) {
            transform.right = _target.transform.position - transform.position;
            if(cooldown >= Tower.fireRate) {
                _target.GetComponent<Enemy>().Damage(_tower.damage);
                cooldown = 0.0f;
            }
            else {
                cooldown += Time.deltaTime;
            }
            
        }
    }
}
