using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public  class Global_V 

{

    public  List<Charatcater_I> All_Char;
    public static Charatcater_I Sys;
    
    public Global_V()
    {
        All_Char = new List<Charatcater_I>();
        
    }
   public void AddToAllChar(string basename,int lv=1)
    {
        string basename1 = "Data/Char/" + basename;      
        Char_Stats base_ob = Resources.Load<Char_Stats>(basename1);
        Charatcater_I CC = new Charatcater_I(base_ob, lv);
        All_Char.Add(CC);
        if (FindCharByName(basename)==Sys)
        {
            Debug.Log("Error in adding " + basename);
        }
    }


    public Charatcater_I FindCharByName(string _name)
    {
        for (int i = 0; i <+ All_Char.Count; i++)
        {
            if (string.Compare(All_Char[i].Char_base_Stat.Char_Name, _name)==0)
            {
                
                return All_Char[i];
            }
        }
        Debug.Log("Error in "+_name+"Not Found " + _name);
        return Sys;
    }

    public List<Charatcater_I> FindCharByNameList(List<string> _name)
    {
        List<Charatcater_I> result = new List<Charatcater_I>();
            for (int i = 0; i < _name.Count; i++)
            {
                if (FindCharByName(_name[i]) != null)
            {
                result.Add(FindCharByName(_name[i]));
            }
            }
        return result;
    }
       
    
}



