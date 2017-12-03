using System.Collections;
using UnityEngine;

public class MenueButtons : MonoBehaviour {

    // Use this for initialization

    public GameObject loadingImage;


    public void LoadScene()
    {
        loadingImage.SetActive(true);
        Application.LoadLevel(1);
    }
}


