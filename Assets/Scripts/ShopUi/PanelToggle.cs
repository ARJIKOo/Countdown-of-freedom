using UnityEngine;
using UnityEngine.UI;

public class PanelToggle : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject shopInventoryUI;
    [SerializeField] private GameObject musicPanel;
    [SerializeField] private GameObject ControllerPanel;
    
    [Header("Buttons")]
    [Header("Inventory Buttons")]
    [SerializeField] private Button inventoryButton;
    [SerializeField] private Button inventoryExitButton;
    [SerializeField] private GameObject InventoryButtonObject;
    [Header("Shop Buttons")]
    [SerializeField] private Button shopButton;
    [SerializeField] private Button shopExitButton;
    [SerializeField] private GameObject shopButtonObject;
    [Header("Music Buttons")]
    [SerializeField] private Button MusicButton;
    [SerializeField] private Button MusicExitButton;
    [Header("Controller Buttons")]
    [SerializeField] private Button ControllerExitButton;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        // set up button liseners
        inventoryButton.onClick.AddListener(ActivateInventoryPanel);
        inventoryExitButton.onClick.AddListener(ExitInventoryPanel);
        shopButton.onClick.AddListener(ActivateShopPanel);
        shopExitButton.onClick.AddListener(ExitShopPanel);
        MusicButton.onClick.AddListener(ActivateMusicPanel);
        MusicExitButton.onClick.AddListener(ExitMusicPanel);
        ControllerExitButton.onClick.AddListener(ExitControllerPanel);
        
        inventoryPanel.SetActive(false);
        shopPanel.SetActive(false);
        shopInventoryUI.SetActive(false);
        musicPanel.SetActive(false);
        ControllerPanel.SetActive(true);
    }

    
    // Methods to activate and deactivate panles
    void ActivateInventoryPanel()
    {
        inventoryPanel.SetActive(true);
        shopPanel.SetActive(false);
        InventoryButtonObject.SetActive(true);
        shopButtonObject.SetActive(false);
        shopInventoryUI.SetActive(true);
    }
    
    void ActivateShopPanel()
    {
        inventoryPanel.SetActive(false);
        shopPanel.SetActive(true);
        InventoryButtonObject.SetActive(false);
        shopButtonObject.SetActive(true);
        shopInventoryUI.SetActive(true);
    }
    
    void ActivateMusicPanel()
    {
        musicPanel.SetActive(true);
        
    }

    void ExitMusicPanel()
    {
        musicPanel.SetActive(false);
    }

    void ExitControllerPanel()
    {
        ControllerPanel.SetActive(false);
    }

    void ExitInventoryPanel()
    {
        inventoryPanel.SetActive(false);
        shopPanel.SetActive(false);
        InventoryButtonObject.SetActive(false);
        shopButtonObject.SetActive(false);
        shopInventoryUI.SetActive(false);
    }

    void ExitShopPanel()
    {
        inventoryPanel.SetActive(false);
        shopPanel.SetActive(false);
        InventoryButtonObject.SetActive(false);
        shopButtonObject.SetActive(false);
        shopInventoryUI.SetActive(false);
    }
   
}
