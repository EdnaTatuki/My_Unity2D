using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryLine
{
    // Start is called before the first frame update

    public string Line;
    public string L_Type;
    public int ID;

    public int Next;
    public Charatcater_I s_char;
    public string s_char_name;
    public string Font;
    public int Location;
    public string Img_Name;

    public StoryLine(int id, string l_Type,string line,int next,string scn="system", Charatcater_I chare=null,int location=0,string iname="normal",string font="sc")
    {
        Line = line;
        ID= id;
        L_Type = l_Type;
        Next = next;
        if (chare == null || chare == Global_V.Sys || l_Type=="Show")
        {
            s_char = Global_V.Sys;
            s_char_name = "System";
            if (l_Type == "Show")
            {
                s_char_name = scn;
            }
        }
        else
        {
            s_char = chare;
            s_char_name = chare.Char_base_Stat.Char_Name;
        
    }
        Location = location;
        Font = font;
        Img_Name = iname;
    }

}
