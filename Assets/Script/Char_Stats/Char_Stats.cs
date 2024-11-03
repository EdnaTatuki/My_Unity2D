using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEditor.U2D;
using UnityEngine;




[CreateAssetMenu (fileName = "Charater" ,menuName = "New Charater Stat")]
public class Char_Stats : ScriptableObject
{
    
    [SerializeField] string Char_name;
    public string Char_Name {
        get {return Char_name;} 
    }

    [SerializeField] string Char_Fullname;
     public string Char_FullName {
        get {return Char_Fullname;} 
    }

    [SerializeField] Char_Rarity Rarity;
    public Char_Rarity RARITY {
        get {return Rarity;} 
    }
    [SerializeField] Char_sex Sex;
    public Char_sex SEX {
        get {return Sex;} 
    }

    [SerializeField] float Strength;
    public float STRENGTH {
        get {return Strength;} 
    }
    [SerializeField] float Stamina;
    public float STAMINA {
        get {return Stamina;} 
    }
    [SerializeField] float Dexterity;
    public float DEXTERITY {
        get {return Dexterity;} 
    }
    [SerializeField] float Speed;
    public float SPEED {
        get {return Speed;} 
    }
    [SerializeField] float Intellgence;
    public float INTELLGENCE {
        get {return Intellgence;} 
    }
    [SerializeField] float Spirit;
    public float SPIRIT {
        get {return Spirit;} 
    }


    
    [SerializeField] List<BodyPS> Stat_DBPlan1;
    public  List<BodyPS> STAT_DBPlan1{
        get {return Stat_DBPlan1;} 
    }
    [SerializeField] List<int> Stat_DBPlan2;
    public  List<int> STAT_DBPlan2{
        get {return Stat_DBPlan2;} 
    }


    [SerializeField] int Sight;
    public int SIGHT{
        get {return Sight;} 
    }
    
    [SerializeField] string Serises;
    public string SERISES{
        get {return Serises;} 
    }
 

    [SerializeField] string IllustrationType;
    public string ILLUSTRATIONTYPE{
        get {return IllustrationType;} 
    }

    [SerializeField] List<string> Char_SpriteName;
    public List<string> Char_SPRITEName{
        get {return Char_SpriteName;} 
    }
    [SerializeField] List<Sprite> Char_SpriteImg; 
    public List<Sprite> Char_SpriteIMG{
        get {return Char_SpriteImg;} 
    }
    [SerializeField] List<string> Char_SpineName;  
    public  List<string> Char_SPINEName{
        get {return Char_SpineName;} 
    }
    [SerializeField] List<SkeletonDataAsset> Char_SpineImg;
    public List<SkeletonDataAsset> Char_SpineSKEL{
        get {return Char_SpineImg;} 
    }

    [SerializeField] List<Char_Race> Race;
    public List<Char_Race> RACES{
        get {return Race;} 
    }
    [SerializeField] List<string> Race_Detail;
    public List<string> RACE_Detail{
        get {return Race_Detail;} 
    }

    //race or array for body parts list. can be null 
    //or [head,0 ......] 
    [SerializeField] string Race_BPL;   
    public string RACE_BPL{
        get {return Race_BPL;} 
    }

    //name of dis��. can be null 
    [SerializeField] string BPL_DisName;
    public string BPL_DisNAME{
        get {return BPL_DisName;} 
    }


    [SerializeField] Sprite Char_icon;
    public Sprite Char_ICON{
        get {return Char_icon;} 
    }


    [SerializeField] Char_SO SO;
    public Char_SO so{
        get {return SO;} 
    }


    [SerializeField] int Logic;
    public int LOGIC{
        get {return Logic;} 
    }

    [SerializeField] int Feeling;
    public int FEELING{
        get {return Feeling;} 
    }






}


public enum Char_sex
{
    None=0,
    Male,
    Female,
    Intersex

}

public enum Char_Rarity
{
    Normal = 0,
    Rare,
    Epic,
    Legend,
    Rainbow

}


// 
public enum Char_SO
{
    A = 0,
    Hetero,
    Homo,
    Bi,
    Andro,
    Gyno,
    Ambi,
    Pan

}

public enum Char_Race
{
    Human,
    Elf,
    Vampire,
    Demon,
    Angel,
    Dragon,
    Dwarf,
    Mech,
    Furry,
    Insect,
    Centaur,
    Undead,
    Filthiness,
    Naga,
    Lizardman,
    Forg,
    MarineO,
    Unknow


}



