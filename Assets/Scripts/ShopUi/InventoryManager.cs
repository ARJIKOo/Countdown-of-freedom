using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
  public static InventoryManager Instance;
  //  List of store inventort items
  public List<GunData> inventory = new List<GunData>();
   
  //Event for update the UI when inventory change
  public delegate void OnInventoryChange();
  public OnInventoryChange OnInventoryChangeCallback;
   

  
  private void Awake()
  {
    if (Instance == null)
    {
      Instance = this;
    }
    else
    {
      Destroy(gameObject);
    }
  }
  
  //Method to add an item to the inventory
  public void AddItem(GunData gun)
  {
    inventory.Add(gun);
      
    // Trigger inventory change event
    if (OnInventoryChangeCallback != null)
    {
      OnInventoryChangeCallback.Invoke();
    }
  }
   

   
 
   
  // Method to display the inventory (for debugging purposes)
  public void DisplayInventory()
  {
    foreach (GunData gun in inventory)
    {
      Debug.Log("Item: " + gun.GunName);
    }
  }

}
