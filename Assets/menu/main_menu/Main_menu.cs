using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_menu : MonoBehaviour
{


    private void Start()
    {
        SoundManager.instance.PlayBGM();
    }


    public void New_game()
    {
        //SceneManager.LoadScene("Story");
        //SceneManager.LoadScene("WorldMap");
        SceneManager.LoadScene("SMap");

    }
}
