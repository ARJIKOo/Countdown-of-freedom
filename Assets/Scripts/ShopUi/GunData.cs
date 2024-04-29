using UnityEngine;

[CreateAssetMenu(fileName = "NewGunData",menuName ="ScriptableObjects/GunData", order = 1)]
public class GunData : ScriptableObject
{
   public string GunName;
   public int damage;
   public int cost;
   public Sprite gunSprite;
   public GameObject BulletsPrefab;
   public int PoolAmount;
}
