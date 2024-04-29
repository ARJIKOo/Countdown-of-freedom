using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
   [SerializeField] private GameObject shopItemPrefab;
   [SerializeField] private Transform contentParent;
   [SerializeField] private Sprite defaultItemImage;
   private GameSessions _gameSessions;

   private void Start()
   {
      UpdateShopPanel();
      _gameSessions = FindObjectOfType<GameSessions>();
   }

   public void UpdateShopPanel()
   {
      // clear existing items
      foreach (Transform child in contentParent)
      {
         Destroy(child.gameObject);
      }
      
      //instantiate ui elemets for each item in shop
      foreach (GunData item in ShopManager.Instance.shopItem)
      {
         GameObject newItem = Instantiate(shopItemPrefab, contentParent);
         TextMeshProUGUI[] itemTexts = newItem.GetComponentsInChildren<TextMeshProUGUI>();

         foreach (TextMeshProUGUI text in itemTexts)
         {
            if (text.CompareTag("GunNameText"))
            {
               text.text = item.GunName;
            }else if (text.CompareTag("GunPriceText"))
            {
               text.text = item.cost.ToString();
            }
         }

         Image[] itemImage = newItem.GetComponentsInChildren<Image>();
         foreach (Image image in itemImage)
         {
            if (image != null && image.CompareTag("GunImage"))
            {
               image.sprite = item.gunSprite != null ? item.gunSprite : defaultItemImage;
               
            }
         }
         
         Button buyButton = newItem.GetComponentInChildren<Button>();
         if (buyButton != null)
         {
            // Add a listener to the button click event
            buyButton.onClick.AddListener(() => BuyItem(item,buyButton));
         }
         else
         {
            Debug.LogWarning("Buy button not found in shop item prefab: " + item.GunName);
         }
      }
   }

   public void BuyItem(GunData item,Button button)
   {
      // Disable button click function
      button.interactable = false;

      // Change button color to green (you may need to adjust color settings as needed)
      ColorBlock colors = button.colors;
      colors.normalColor = Color.green;
      button.colors = colors;

      // Call the BuyItem method of the ShopManager for the selected item
      if (_gameSessions.GetCoinCount() >= item.cost)
      {
         ShopManager.Instance.BuyItem(item);
         _gameSessions.DiscounCoin(item.cost);
      }
      else
      {
         Debug.Log("Not enough coins to purchase the gun.");
      }
      //ShopManager.Instance.BuyItem(item);
        
      UpdateShopPanel();
   }
}
