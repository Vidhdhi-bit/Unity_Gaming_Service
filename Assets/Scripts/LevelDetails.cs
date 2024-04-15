using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class LevelDetails : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject levelNumber = GameObject.FindGameObjectWithTag("LevelNo");

        levelNumber.GetComponent<TextMeshProUGUI>().text = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
