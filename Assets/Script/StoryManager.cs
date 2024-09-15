using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;

public class StoryManager:MonoBehaviour 
{

    public Global_V global_V;
    
    // Start is called before the first frame update


    public static StoryManager instance;


    public bool isStoryMode;


    bool prelog;
    bool tur;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public StoryManager()
    {
        global_V = new Global_V();
    }

    void Start()
    {

    }

    void Update()
    {

        if (SceneManager.GetActiveScene().name == "Story"&&!prelog)
        {
            Prelog();
        }
        if (DialogManager.instance.isOver && prelog&&!tur)
        {
            if (SceneManager.GetActiveScene().name != "SMap")
            {
                SceneManager.LoadScene("SMap");
            }
            else
            {
                Tutorial();

            }
        }
        if (isStoryMode)
        {
            DialogManager.instance.DialogUpdate();
        }

    }

    public  void Prelog()
    {
        prelog = true;
        global_V.AddToAllChar("System");
        global_V.AddToAllChar("YJY");
        global_V.AddToAllChar("Volupte");
        global_V.AddToAllChar("Alliycia");
        global_V.AddToAllChar("Magie");
        global_V.AddToAllChar("Noirceur");
        global_V.AddToAllChar("Orika");
        global_V.AddToAllChar("Rin");
       
        global_V.AddToAllChar("拉尼娅");
        global_V.AddToAllChar("伊薇特");
        global_V.AddToAllChar("伊薇特-觉醒");
        global_V.AddToAllChar("阿莱美达");
        global_V.AddToAllChar("リラ・ファクトリナ");
        global_V.AddToAllChar("貽耶摩津巳");
        global_V.AddToAllChar("ムト・エーミル");
        global_V.AddToAllChar("アルカ");
        global_V.AddToAllChar("スーリ・シャムシート");
        global_V.AddToAllChar("導守梢");
        
        DialogManager.instance.StartIndex();
        StoryText preolg = new StoryText(global_V,"ad36");
        DialogManager.instance.StoryToTlak = preolg;
        isStoryMode = true;
       


    }
    public void Tutorial()
    {

       
        this.tur = true;
        //CameraManager.instance.AddUItoCamera(DialogManager.instance.DialogUi);


        DialogManager.instance.StartIndex();
        StoryText tur = new StoryText(global_V, "tur");
        DialogManager.instance.StoryToTlak = tur;
        isStoryMode = true;

    }


}
