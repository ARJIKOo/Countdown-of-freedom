using UnityEngine;
using UnityEngine.Serialization;

public class CoinSystem : MonoBehaviour
{
    [SerializeField] private int coinPoint = 100;
    [SerializeField] private AudioClip coinPickupSfx;
    [FormerlySerializedAs("wasPickUP")] [SerializeField] private bool wasPickUp=false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")&& !wasPickUp)
        {
            //if (Camera.main != null) AudioSource.PlayClipAtPoint(coinPickupSfx, Camera.main.transform.position);
            AudioManager.Instance.PlaySFX("Coin");
            FindObjectOfType<GameSessions>().AddCoinPoint(coinPoint);
            wasPickUp = true;
            Destroy(gameObject);
        }
    }
}
