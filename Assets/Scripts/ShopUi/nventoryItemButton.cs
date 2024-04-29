using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class nventoryItemButton : MonoBehaviour
{
   [SerializeField] private Button itemButton;
   [SerializeField] private TMP_Text buttonText;
   private GunData gunData;
   private bool isSelected;
   
   
   
   //method to initialize the InventoryItemButton with GunData

   public void Initialize(GunData data)
   {
      gunData = data;
      UpdateButtonUI();
   }

   private void Start()
   {
      itemButton.onClick.AddListener(OnItemClick);
   }

   void OnItemClick()
   {
      if (gunData != null  )
      {
         // Change the player's gun sprite
         PlayerMovement.instance.ChangeGunSprite(gunData.gunSprite);
         ChangeObjectsPoolSettings();
      }
      if (isSelected)
         return;

      // Deselect any previously chosen item
      DeselectAllItem();

      // Toggle the isSelected flag
      isSelected = true;
      

      // Update the button UI
      UpdateButtonUI();
   }

   // Method to update the button UI based on the isSelected flag
   void UpdateButtonUI()
   {
      buttonText.text = isSelected ? "Chosened" : "Chosen";
      //if isselected is true
      ColorBlock colors = itemButton.colors;
      colors.normalColor = isSelected ? Color.green : Color.white;
      itemButton.colors = colors;
   }

   void DeselectAllItem()
   {
      foreach (Transform child in transform.parent)
      {
         nventoryItemButton itemButton = child.GetComponent<nventoryItemButton>();

         if (itemButton == this || !itemButton.isSelected)
         {
            continue;
         }

         itemButton.isSelected = false;
         itemButton.UpdateButtonUI();
      }
   }

   public void ChangeObjectsPoolSettings()
   {
      ObjectPool.FindObjectOfType<ObjectPool>().ResetObjectPool(gunData.BulletsPrefab,gunData.PoolAmount);
   }
}
