using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BodyPS 
{

    public Char_BodyParts_Type BPType;
    public int BPLevel;
    public int BPExp;
    public int BPOld;

    public float Strength;
    public float Stamina;
    public float Dexterity;
    public float Speed;
    public float Intellgence;
    public float Spirit;


    public Char_BPD_RL BPLR; 
    public List<BodyPSD> BodyPSD;

    public Char_Race Race;


}


public class BodyPS_Head: BodyPS
{

    public BodyPS_Head()
    {
        BPType = Char_BodyParts_Type.Head;
    }


}
    public class BodyPS_Body : BodyPS
{
    public BodyPS_Body()
    {
        BPType = Char_BodyParts_Type.Body;
    }

}

public class BodyPS_Arm : BodyPS
{
    public BodyPS_Arm()
    {
        BPType = Char_BodyParts_Type.Arm;
    }

}

public class BodyPS_Leg : BodyPS
{
    public BodyPS_Leg()
    {
        BPType = Char_BodyParts_Type.Leg;
    }
}



public class BodyPSD
{
    public Char_BPD_Type BPDType;
    public Char_BPD_RL BPLR;
    public BodyPS Parent;
    public List<BodyPSDD> BodyPSDD;
    public Char_Race Race;

    public BodyPSD (BodyPS bps)
    {
        Parent = bps;
        Race = bps.Race;
        BPLR = bps.BPLR;

      
    }

}


public class BodyPSDD
{
    public Char_BPD_RL BPLR;
    public BodyPSD Parent;
    public Char_Race Race;

}


public enum Char_BodyParts_Type
{
    Head=0,
    Body=1,
    Arm=2,
    Leg=3,
    Tail=4,
    Wing=5,

}

public enum Char_BPD_RL
{
    Null = 0,
    Left = 1,
    Right = 2,

}

public enum Char_BPD_Type
{
    Hand = 0,
    Foot = 1,

}