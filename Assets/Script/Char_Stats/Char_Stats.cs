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
    
    public string Char_name;
    public string Char_Fullname;
    public Char_Rarity Rarity;
    public Char_sex Sex;

    public float Strength;
    public float Stamina;
    public float Dexterity;
    public float Speed;
    public float Intellgence;
    public float Spirit;


    //public Dictionary<BodyPS, int> Stat_DBPlan;
    public List<BodyPS> Stat_DBPlan1;
    public List<int> Stat_DBPlan2;


    public int Sight;

    
    public string Serises;

    public string IllustrationType;
    public List<string> Char_SpriteName;
    public List<Sprite> Char_SpriteImg;
    public List<string> Char_SpineName;
    public List<SkeletonDataAsset> Char_SpineImg;

    public List<Char_Race> Race;
    public List<string> Race_Detail;

    //race or array for body parts list. can be null 
    //or [head,0 ......] 
    public string Race_BPL;
    //name of dis��. can be null 
    public string BPL_DisName;

    public Sprite Char_icon;

    public Char_SO SO;

    public int Logic;
    public int Feeling;





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



