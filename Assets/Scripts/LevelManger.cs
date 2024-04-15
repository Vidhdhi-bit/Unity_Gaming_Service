using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class LevelManger : MonoBehaviour
{
    public GameObject LevelPrefab;
    public GameObject LevelParent;
    public GameObject ShopPanel;
    public GameObject CloseButton;
    public TextMeshProUGUI coinsText;

    private int coins;
    private int LevelPrice=100;
  
    // Start is called before the first frame update
    void Start()
    {
        CloseButton.GetComponent<Button>().onClick.AddListener(() => CloseShopPanel());

        Loadlevels();
        if (PlayerPrefs.HasKey("Coins"))
        {
            coins = PlayerPrefs.GetInt("Coins"); UpdateCoinUI();
        }
        else 
        {
            PlayerPrefs.SetInt("Coins", 1000);
            coins = 1000; 
            UpdateCoinUI();

        }
    }
    void UpdateCoinUI() 
    {
        coinsText.text = "Coins: " + coins;
    }
    private void Loadlevels() 
    {
        int sceneCount = Directory.GetFiles("Assets\\Scenes\\Levels", "*.unity").Length;

        for (int i = 0; i < sceneCount; i++) 
        {
            GameObject Level = Instantiate(LevelPrefab, LevelParent.transform);
            Level.name = Level.GetComponentInChildren<TextMeshProUGUI>().text = "Level " + (i + 1);

            Transform[] children = Level.GetComponentsInChildren<Transform>(true);
            int isLevelPurchased = PlayerPrefs.GetInt(Level.name + "_Purchased");

            foreach (Transform child in children)
            {
                if (child.CompareTag("PurchaseBtn"))
                {
                    if (isLevelPurchased == 0)
                    {
                        child.GetComponent<Button>().interactable = true;
                        child.GetComponent<Button>().onClick.AddListener(() => PurchaseLevel(Level.name));
                    }

                    else
                        child.GetComponent<Button>().interactable = false;

                }
                if (child.CompareTag("LevelName"))
                {
                    if (isLevelPurchased == 0)
                        child.GetComponent<Button>().interactable = false;
                    else
                    {
                        child.GetComponent<Button>().interactable = true;
                        child.GetComponent<Button>().onClick.AddListener(() => LoadLevel(Level.name));
                    }
                }
            }
        }
    }
    void UpdateUI(string levelName)
    {
        int isLevelPurchased = PlayerPrefs.GetInt(levelName + "_Purchased");

        GameObject Level = LevelParent.transform.Find(levelName).gameObject;
        foreach (Transform child in Level.transform)
        {
            if (child.CompareTag("PurchaseBtn"))
            {
                if (isLevelPurchased == 0)
                {
                    child.GetComponent<Button>().interactable = true;
                    child.GetComponent<Button>().onClick.AddListener(() => PurchaseLevel(Level.name));
                }

                else
                    child.GetComponent<Button>().interactable = false;

            }
            if (child.CompareTag("LevelName"))
            {

                if (isLevelPurchased == 0)
                    child.GetComponent<Button>().interactable = false;
                else
                {
                    child.GetComponent<Button>().interactable = true;
                    child.GetComponent<Button>().onClick.AddListener(() => LoadLevel(Level.name));

                }
            }
        }

    }
    void PurchaseLevel(string LevelName) 
    {
        print(coins + " level price=" + LevelPrice);
        if (coins >= LevelPrice)
        {
            coins -= LevelPrice;
            PlayerPrefs.SetInt("Coins", coins);
            PlayerPrefs.SetInt(LevelName + "_Purchased", 1);
            coinsText.text = "Coins: " + coins;
            print(LevelName);
            UpdateUI(LevelName);

        }
        else
        {
            Debug.Log("No enough coins to purchase");
            OpenShopPanel();
        }
    }
    void LoadLevel(string LevelName) 
    {
        SceneManager.LoadScene(LevelName);
    }
    void OpenShopPanel()
    {
        ShopPanel.SetActive(true);
    }
    void CloseShopPanel() 
    {
        ShopPanel.SetActive(false);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
