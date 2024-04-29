using UnityEngine;
using UnityEngine.Serialization;

public class EnemyMovement : MonoBehaviour
{
    [FormerlySerializedAs("Speed")] [SerializeField] private float speed;
    private Rigidbody2D _myRigidbody2D;
    [SerializeField] private int EnemyHealt;
    
    void Start()
    {
        _myRigidbody2D = GetComponent<Rigidbody2D>();
    }

   
    void Update()
    {
        _myRigidbody2D.velocity = new Vector2(speed, 0);
        //.Instance.PlaySFX("EnemyWalk");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        speed = -speed;
        FlipEnemyFace();
    }

    void FlipEnemyFace()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(_myRigidbody2D.velocity.x)),1f);
    }

    public int GetEnemyHealthCount()
    {
        return EnemyHealt;
    }

    public void GetEnemyDamage(int damage)
    {
        Debug.Log("taching " + gameObject.name);
        EnemyHealt -= damage;
        if (EnemyHealt <= 0)
        {
            gameObject.SetActive(false);
            AudioManager.Instance.PlaySFX("EnemyDeath");
        }
    }
}
