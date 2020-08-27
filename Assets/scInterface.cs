using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scInterface : MonoBehaviour
{
    public GameObject yourButton;
    // Start is called before the first frame update
    void Start()
    {
        yourButton.SetActive(false);
        Button btn = yourButton.GetComponent<Button>(); 
        btn.onClick.AddListener(ResetGame); 
    }
    void ResetGame()
    {
        SceneManager.LoadScene("SampleScene");
        yourButton.SetActive(false);
        Time.timeScale = 1f;
    }
    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale == 0) yourButton.SetActive(true);
    }
}
