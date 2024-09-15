using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using static UnityEditor.Progress;
using static UnityEditor.FilePathAttribute;

public class StoryText 
{
    int Story_index;

    //DialogmanagerFile
    public UnityEngine.TextAsset dialogFile;

    public Dictionary<string,Charatcater_I> Char_L;
    public List<StoryLine> StoryLine_list;



    public Global_V global_;


    //test
    public List<List<string>> Story_Text;



    public List<string> Dialog_Type_list;

    public StoryText(Global_V g,string sname)
    {
        StoryLine_list = new List<StoryLine>();
        Char_L = new Dictionary<string, Charatcater_I>();
        global_ = g;
        ReadStory(sname);
    }





    public void ReadStory(string _story)
    {
        _story = "Story/" + _story;
        dialogFile = Resources.Load<TextAsset>(_story);
        string[] rows = dialogFile.text.Split("\n");
        string[] charlist = rows[0].Split(",", System.StringSplitOptions.None);
        List<string> charl = charlist.ToList<string>();
        charl.RemoveAt(0);
        for (int j=0; j<charl.Count-1; j++)
        {
            if (charl[j] == null || charl[j] == "")
            {
                break;
            }
            if (Char_L.ContainsKey(charl[j]) == false)
            {
                Char_L.Add(charl[j], global_.FindCharByName(charl[j]));
            }
        }
    
        for (int i =2;i<rows.Length;i++)
        {
            string [] con = rows[i].Split(",", System.StringSplitOptions.None);
            if (con[0] == null || con[0]=="")
            {
                break;
            }
            int id = int.Parse(con[0]);
            int location=0;
            if (con[5] != "")
            {
                location = int.Parse(con[5]);
            }
            int next_id = int.Parse(con[4]);
            Charatcater_I _sc = Global_V.Sys;
            if (Char_L.ContainsKey(con[2]))
            {
                 _sc = Char_L[con[2]];
            }
            
            StoryLine_list.Add(new StoryLine(id, con[1], con[3], next_id, con[2], _sc, location, con[6], con[7]));
        }

    }


    public void AllDialog()
    {
        for (int i = 0; i < Dialog_Type_list.Count; i++)
        {
            List<string> tmp = new List<string>();
            tmp.Add(Dialog_Type_list[i]);
            Story_Text[i] = tmp;
        }
        Story_index = 0;
    }

    public void SetCharByName(List<Charatcater_I> _char, List<StoryLine> _line)
    {
        for (int i = 0; i < _line.Count; i++)
        {
            for (int j = 0; j < _char.Count; j++)
                if (_char[j].Char_base_Stat.Char_name == _line[i].s_char_name)
                {
                    _line[i].s_char = _char[j];
                }
        }
    }




}
