using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelUIHandler : MonoBehaviour
{
    public GameObject BackButton;
    // Start is called before the first frame update
    void Start()
    {
        BackButton.GetComponent<Button>().onClick.AddListener(() => BackToLevelManagerScene());
    }

    void BackToLevelManagerScene()
    {
        SceneManager.LoadScene("Level Manager");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
