using UnityEngine;

public class Bounce : MonoBehaviour
{
    [SerializeField] private Sprite firstSprite;

    [SerializeField] private Sprite seconSprite;

    private SpriteRenderer _mySpriteRenderer;

    private CapsuleCollider2D _myCapsuleCollider2D;
    // Start is called before the first frame update
    void Start()
    {
        _mySpriteRenderer = GetComponent<SpriteRenderer>();
        _myCapsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        BounceSpriteChange();
    }

    void BounceSpriteChange()
    {
        if (!_myCapsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            _mySpriteRenderer.sprite = firstSprite;
            return;
        }

        _mySpriteRenderer.sprite = seconSprite;
        AudioManager.Instance.PlaySFX("Bounce");
    }
}
