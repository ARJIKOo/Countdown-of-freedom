using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    // singleton instance
    public static ShopManager Instance;
    
    //list to store shop item
    public List<GunData> shopItem = new List<GunData>();
   
    //event for update UI when shop items change
    public delegate void OnShopItemChanged();
    public OnShopItemChanged OnShopItemChangedCallback;


    private void Awake()
    {
        //singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
   
  
   
    //Method to remove an item form the shop
    public void RemoveItemToShop(GunData item)
    {
        shopItem.Remove(item);
      
        //trigger shop item change event
        if (OnShopItemChangedCallback != null)
        {
            OnShopItemChangedCallback.Invoke();
        }
    }
   
    //Method to but an item from the shop
    public void BuyItem(GunData item)
    {
        //Add the item to the player inventory
        InventoryManager.Instance.AddItem(item);
      
        //Remove the item from the shop
        RemoveItemToShop(item);
    }
    
    //Method to display the items (for debugging purpose)
    public void DisplayShopItems()
    {
        foreach (GunData item in shopItem)
        {
            Debug.Log("Shop Item" + item.GunName);
        }
    }

}
