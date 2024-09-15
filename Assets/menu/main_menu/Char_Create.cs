using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char_Create : MonoBehaviour
{
    [SerializeField] Char_Stats _base;
    [SerializeField] int level;

    public Charatcater_I N_Charater;

    [SerializeField] Char_Detail_1 _detail_1;

    public void Setup()
    {
        N_Charater = new Charatcater_I(_base, level);
       // _detail_1.SetData(N_Charater);
    }



    public void Start()
    {
       // Setup();
        
    }

}
