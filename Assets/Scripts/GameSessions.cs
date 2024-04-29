using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class GameSessions : MonoBehaviour
{
    public static GameSessions Instance { get; private set; }
    [SerializeField] private int playerLives = 3;
    [SerializeField] private int coin ;
    [SerializeField] private float resetTime;

    [FormerlySerializedAs("CoinText")]
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private Image livesImage;
    [FormerlySerializedAs("_musicSlider")] [Header("Music")]
    

    [Header("Music")]
    public Scrollbar sfxSlider;
    public Scrollbar musicSlider;

    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSessions>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
    }

    private void Start()
    {
        coinText.text = coin.ToString();
        musicSlider.value = 0.5F;
        sfxSlider.value = 0.5F;
    }

    public void AddCoinPoint(int points)
    {
        coin += points;
        coinText.text = coin.ToString();
    }

    public void DiscounCoin(int price)
    {
        coin -= price;
        coinText.text = coin.ToString();
    }

    public void DeathSession()
    {
        if (playerLives > 1)
        {
           TakeLife();
           StartCoroutine(WaitLOadScene());
        }
        else
        {
            ResetGameSession();
            AudioManager.Instance.musicSource.Play();
        }
    }
    

    // ReSharper disable Unity.PerformanceAnalysis
    void ResetGameSession()
    {
        Destroy(gameObject);
        FindObjectOfType<ScenePersist>().ResetScenepersist();
        SceneManager.LoadScene(0);
    }

    void TakeLife()
    {
        playerLives = playerLives - 1;
        livesImage.fillAmount -= 0.1f;

    }
    
    IEnumerator  WaitLOadScene()
    {
        yield return new WaitForSecondsRealtime(resetTime);
        int correctSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(correctSceneIndex);
    }

    public int GetCoinCount()
    {
        return coin;
    }

    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(musicSlider.value);
        
    }

    public void SfxVolume()
    {
        AudioManager.Instance.SFXVolume(sfxSlider.value);
    }
    

  
}
