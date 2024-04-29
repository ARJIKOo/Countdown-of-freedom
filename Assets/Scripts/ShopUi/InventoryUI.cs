using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject inventoryItemPrefab;
    [SerializeField] private Transform contentParent;
    [SerializeField] private Sprite defaultItemImage;
    private int previousItemCount;
    
    void Start()
    {
        
        previousItemCount = InventoryManager.Instance.inventory.Count;
        UpdateInventoryPanel();
        InventoryManager.Instance.OnInventoryChangeCallback += HandleInventoryChange;
        
    }

    void HandleInventoryChange()
    {
        // check if the number of inventory items has change
        if (InventoryManager.Instance.inventory.Count != previousItemCount)
        {
            UpdateInventoryPanel();
            previousItemCount = InventoryManager.Instance.inventory.Count;
        }
    }

    void UpdateInventoryPanel()
    {
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }
        
        //instance UI elements
        foreach (GunData gun in InventoryManager.Instance.inventory)
        {
            GameObject newItem = Instantiate(inventoryItemPrefab, contentParent);

            TextMeshProUGUI[] itemTexts = newItem.GetComponentsInChildren<TextMeshProUGUI>();
            nventoryItemButton itemButton = newItem.GetComponent<nventoryItemButton>();
            if (itemButton != null)
            {
                itemButton.Initialize(gun);
            }
            else
            {
                Debug.LogWarning("InventoryItemButton component not found on prefab.");
            }
            
            foreach (TextMeshProUGUI text in itemTexts)
            {
                if (text.CompareTag("GunNameText"))
                {
                    text.text = gun.GunName;
                }

                if (text.CompareTag("GunPriceText"))
                {
                    text.text = gun.cost.ToString();
                }
            }

            Image[] itemImage = newItem.GetComponentsInChildren<Image>();

            foreach (Image image in itemImage)
            {
                if (image != null && image.CompareTag("GunImage"))
                {
                    image.sprite = gun.gunSprite != null ? gun.gunSprite : defaultItemImage;
                }
            }
        }
    }
}
