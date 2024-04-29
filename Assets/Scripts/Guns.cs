using System;
using Unity.VisualScripting;
using UnityEngine;


public class Guns : MonoBehaviour
{
    private Rigidbody2D _myRigidbody2D;
    private PlayerMovement _playerMovement;
    [SerializeField] public int bulettsDamage;
    [SerializeField] private float speed;
    private EnemyMovement _enemyMovement;

    private float _xspeed;
    

    void Start()
    {
        _myRigidbody2D = GetComponent<Rigidbody2D>();
        _playerMovement = FindObjectOfType<PlayerMovement>();
       _xspeed = _playerMovement ? _playerMovement.transform.localScale.x * speed : 0;
       _enemyMovement = FindObjectOfType<EnemyMovement>();

    }
    private void OnEnable()
    {
        
        _xspeed = _playerMovement ? _playerMovement.transform.localScale.x * speed : 0;
    }

    private void OnDisable()
    {
        _xspeed = 0;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (_playerMovement)
        {
            transform.Translate(new Vector2(_xspeed * Time.deltaTime, 0));
            Debug.Log(_xspeed);
        }
    }


    


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            //Destroy(other.gameObject);
            EnemyMovement enemyMovement = other.gameObject.GetComponent<EnemyMovement>();
            enemyMovement.GetEnemyDamage(bulettsDamage);
        }

        
        BulletPools();
    }
    
    

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyMovement enemyMovement = other.gameObject.GetComponent<EnemyMovement>();
            enemyMovement.GetEnemyDamage(bulettsDamage);
           // Destroy(other.gameObject);
        }
        
        BulletPools();
    }

    void BulletPools()
    {
        
        gameObject.SetActive(false);
        
    }

   
}
