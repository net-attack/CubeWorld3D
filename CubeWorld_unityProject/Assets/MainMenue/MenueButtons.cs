using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenueButtons : MonoBehaviour {

    // Use this for initialization

    public GameObject loadingImage;


    public void LoadScene()
    {
        loadingImage.SetActive(true);
        SceneManager.LoadScene(1);
    }
}


