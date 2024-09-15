using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Unity.Burst.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UIElements;

public class Charatcater_I
{

    public Char_Stats Char_base_Stat { get; set; }
    public int Char_Level { get; set; }

    public Dictionary<string, Sprite> Char_Sprite;
    public Dictionary<string, SkeletonDataAsset> Char_Spine;
    public Dictionary<Char_Race, List<string>> Char_RaceDic;

    public List<BodyPS> Char_BPS;


    public Charatcater_I(Char_Stats _Stats, int _level)
    {

        Char_base_Stat = _Stats;
        Char_Level = _level;
        if (Char_base_Stat.IllustrationType == "Spine")
        {
            Char_Spine = SpineDict(Char_base_Stat.Char_SpineName, Char_base_Stat.Char_SpineImg);
        }
        else
        {
            Char_Sprite = SpriteDict(Char_base_Stat.Char_SpriteName, Char_base_Stat.Char_SpriteImg);
        }

        if (Char_base_Stat.Race_Detail.Count > 0)
        {
            Char_RaceDic = Sec_Race(Char_base_Stat.Race, Char_base_Stat.Race_Detail);
        }

        //Char_BPS=Creat_Body_ByRace(Char_base_Stat);
        

        //GetDisForBPL("Default", Char_BPS, Char_base_Stat);



    }






    public List<BodyPS> Creat_Body_ByRace(Char_Stats _Stats)
    {
        //������
        //ת��Ϊ��ά����
        //дһ����csv�ļ����ȡ�ģ���Ҫ�����б�
        List<BodyPS> result = new List<BodyPS>();
        //�������
        // Race_BPL ����ֱ�Ӹ�һ������str��Ȼ���Զ���
        //Ҳ���Ը�һ�� �����ָ�ĸ�ʽ�������ֶ�����
        if (_Stats.Race_BPL != string.Empty)
        {
            string[] bpln = _Stats.Race_BPL.Split(",");
            
            if (bpln.Length > 1)
            {
                result = GetBPLFromStr(bpln);
                return result;
            }
            else
            {
                result = GetBPLFromRace_Str(bpln[0]);
                return result;
            }

        }

        //�ڶ��������
        if (_Stats.Race_Detail.Count != 0 && _Stats.Race_Detail[0] != "#")
        {
            result = GetBPLFromRace_Str(_Stats.Race_Detail[0]);
            return result;
        }


        //��һ�������

         result = GetBPLFromRace_Str(_Stats.Race[0].ToString());
        if (result.Count == 0) {
            Debug.Log("Error"+ _Stats.Char_name+ "BPS is Empty");
        }
        
        return result;
    }


    public List<BodyPS> GetBPLFromStr(string [] bplste)
    {
        List<BodyPS> result = new List<BodyPS>();
        for (int j = 1; j < bplste.Length ; j++)
        {
            if (int.Parse(bplste[j]) != 0)
            {
                for (int k = 0; k < int.Parse(bplste[j]); k++)
                {
                    if (j == 1)
                    {
                        result.Add(new BodyPS_Head());
                    }
                    if (j == 2)
                    {
                        result.Add(new BodyPS_Body());
                    }
                    if (j == 3)
                    {
                        result.Add(new BodyPS_Arm());
                    }
                    if (j == 4)
                    {
                        result.Add(new BodyPS_Leg());
                    }
                }
            }
        }

        //result ����Ϊnull

        return result;
    }

    public List<BodyPS> GetBPLFromRace_Str(string race)
    {


        //ÿ�ζ�ȡ���嶼Ҫ���ɶ�ά���飬��Ҫ�Ż���
        //Debug.Log("Finding" + race);
        List<BodyPS> result = new List<BodyPS>();
        UnityEngine.TextAsset BPL_MainFile = Resources.Load<TextAsset>("BodyPartsList/BodyPartsListDetail");
        UnityEngine.TextAsset BPL_MainFile2 = Resources.Load<TextAsset>("BodyPartsList/BodyPartsList");
        //string[] rows = BPL_MainFile.text.Trim().Split("\n");
        List<string> rows = BPL_MainFile.text.Trim().Split("\n").ToList();
        rows.RemoveAt(0);
        //string[] rows2 = BPL_MainFile2.text.Trim().Split("\n");
        List<string> rows2 = BPL_MainFile2.text.Trim().Split("\n").ToList();
        rows2.RemoveAt(0);
        //����rows��ʼ����С
        string[,] d2a = new string[rows.Count+rows2.Count, rows[0].Split(",").Length];
        for (int i = 0; i < rows.Count + rows2.Count; i++)
        {
            if( i < rows.Count)
            {
                string[] lines = rows[i].Split(",");
                for (int j = 0; j < lines.Length; j++)
                    d2a[i, j] = lines[j];
            }
            else
            {
                string[] lines = rows2[i- rows.Count].Split(",");
                for (int j = 0; j < lines.Length; j++)
                    d2a[i, j] = lines[j];
            }

        }


        for (int i = 0; i < d2a.GetLength(0); i++)
        {
            if (d2a[i, 0] == race)
            {
                for (int j = 1; j < d2a.GetLength(1); j++)
                {
                    if (d2a[i, j] != null && int.Parse(d2a[i, j]) != 0)
                    {
                        for (int k = 0; k < int.Parse(d2a[i, j]); k++)
                        {
                            if (j == 1)
                            {
                                result.Add(new BodyPS_Head());
                            }
                            if (j == 2)
                            {
                                result.Add(new BodyPS_Body());
                            }
                            if (j == 3)
                            {
                                result.Add(new BodyPS_Arm());
                            }
                            if (j == 4)
                            {
                                result.Add(new BodyPS_Leg());
                            }


                        }
                    }
                }


            }
        }


        ////result ����Ϊnull

        return result;
    }


    public void GetDisForBPL(string dname, List<BodyPS> bps, Char_Stats stat)
    {
        /*
        Dictionary<string, string[,]> result = new Dictionary<string, string[,]>();
        UnityEngine.TextAsset BPL_MainFile = Resources.Load<TextAsset>("BodyPartsList/BodyPartsListDetail");
        string[] rows = BPL_MainFile.text.Split("\n");
        //��������Ҫ��Ҫ��̬ȷ����С��
        string name = "";
        string[,] a2 = new string  [6,7];
        for (int i=0; i<rows.Length; i++)
        {
            string[] lines = rows[i].Split(",");
            //����ÿ����a2�Ĵ�Сȷ��
            if ( i%7  == 0) {
                if (name != "") { 
                    result.Add(name,a2);
                }
            
                name = lines[i];
            }
            else
            {
                for (int j = 0; j < lines.Length; j++)
                    a2[i%7-1,j] = lines[j];
            }
            
        }*/

        //��������ֱ��д�ױȷ�
        //������ôʵ�ַ��䣬�ڿ��ǡ�����������������������������

        // ÿ������0��0 ��dname ����
        Dictionary<string, string[]> result = new Dictionary<string, string[]>();
        UnityEngine.TextAsset BPL_MainFile = Resources.Load<TextAsset>("BodyPartsList/BPDistributeRule");
        string[] rows = BPL_MainFile.text.Split("\n");
        //��������Ҫ��Ҫ��̬ȷ����С��
        for (int i = 0; i < rows.Length; i++)
        {
            string[] lines = rows[i].Split(",");
            //����ÿ����a2�Ĵ�Сȷ��
            if (i % 7 == 0&& lines[0] == dname)
            {
                for (int j=1; j < 7; j++)
                {
                    string bplname = rows[i + j].Split(",")[0];
                    string [] bpldis = new string [6];
                    Array.Copy(rows[i + j].Split(","), 1, bpldis,0 , 6);
                    result.Add(bplname, bpldis);
                }
                break;
            }

        }

        for (int i = 0; i < bps.Count; i++)
        {
            if (result.ContainsKey(bps[i].BPType.ToString()))
            {
                bps[i].Strength = ((1- GetDisForBPL_Rnd())*float.Parse(result[bps[i].BPType.ToString()][1]) / 100) * stat.Strength;
                bps[i].Dexterity = ((1 - GetDisForBPL_Rnd()) * float.Parse(result[bps[i].BPType.ToString()][1]) / 100) * stat.Dexterity;
                bps[i].Stamina = ((1 - GetDisForBPL_Rnd()) * float.Parse(result[bps[i].BPType.ToString()][1]) / 100) * stat.Stamina;
                bps[i].Speed = ((1 - GetDisForBPL_Rnd()) * float.Parse(result[bps[i].BPType.ToString()][1]) / 100) * stat.Speed;
                bps[i].Intellgence = ((1 - GetDisForBPL_Rnd()) * float.Parse(result[bps[i].BPType.ToString()][1]) / 100) * stat.Intellgence;
                bps[i].Spirit = ((1 - GetDisForBPL_Rnd()) * float.Parse(result[bps[i].BPType.ToString()][1]) / 100) * stat.Spirit;


            }
        }

    }


    public float GetDisForBPL_Rnd(float mean = 0, float stdDev = 0.1715f)
    {
        //mean ������������
        //stdDev�Ǳ�׼��

        float u1 = 1 - (float)new System.Random().NextDouble();
        float u2 = 1 - (float)new System.Random().NextDouble();
        float randStdNormal = (float)(Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2));

        //����outliars
        if (mean + stdDev * randStdNormal > 0.4 || mean +stdDev * randStdNormal < -0.4)
        {
            randStdNormal = GetDisForBPL_Rnd();
        }

        return mean + stdDev * randStdNormal;



    }


    public int C_Strength
    {
        get { return Char_Level * (int)(Char_base_Stat.Strength + 1) / 30; }
    }
    public int C_Stamina
    {
        get { return Char_Level * (int)(Char_base_Stat.Stamina + 1) / 30; }
    }
    public int C_Dexterity
    {
        get { return Char_Level * (int)(Char_base_Stat.Dexterity + 1) / 30; }
    }

    public int C_Speed
    {
        get { return Char_Level * (int)(Char_base_Stat.Speed + 1) / 30; }
    }

    public int C_Spirit
    {
        get { return Char_Level * (int)(Char_base_Stat.Spirit + 1) / 30; }
    }


    public int C_Intellgence
    {
        get { return Char_Level * (int)(Char_base_Stat.Intellgence + 1) / 30; }
    }

    public int Max_HP
    {
        get { return (int)(Cacu_Curve(C_Strength, Char_base_Stat.Strength) + Cacu_Curve(C_Stamina, Char_base_Stat.Stamina, 2) + Cacu_Curve(C_Spirit, Char_base_Stat.Spirit, (float)1.5)); }
    }

    public int Max_SP
    {
        get { return (int)(Cacu_Curve(C_Strength, Char_base_Stat.Strength) + Cacu_Curve(C_Stamina, Char_base_Stat.Stamina) + Cacu_Curve(C_Speed, Char_base_Stat.Speed) + Cacu_Curve(C_Dexterity, Char_base_Stat.Dexterity)); }
    }

    public int Max_MP
    {
        get { return (int)(Cacu_Curve(C_Intellgence, Char_base_Stat.Intellgence, (float)2.5) + Cacu_Curve(C_Spirit, Char_base_Stat.Spirit, (float)2)); }
    }

    //x is stat current,  D_x is the limit of the stat, k is caculate coefficient
    // (D_x+1) needs to add 1 as the min vaule
    // Tail_1 is a contst
    //���赱ǰ����ΪS Prefab�е������޶�ΪSetS ���õ�������СֵΪ1 ʵ�������޶�ΪDS=Sets+1
    //�ȼ�ΪL ������30����ʱ��ǰ�����ﵽ�����޶ȡ�S=L*DS/30
    //HP�����������仯���յ�����Ϊ x*ln��-x��-x  ���������x=DS Ҳ������������ʱ��ﵽ���ֵ�������Ҫ���������Ҳ�ƽ�ƣ�Ҳ����x=x-DS��
    //��ʱ�õ�������-HP�����������£���S-DS��*ln��-S+DS��-S+DS
    //�����������F��0��Ϊ������Ϊ���õ͵ȼ�HP��Ϊ������Ҫ�����ʽ��+F��0����ͬʱ��һ������Tail_1Ϊ����-HP����С�ӳɣ���������Ϊ50��Ҳ����F��0��+50=Tail_1��Ϊ����������
    //����F��0���ܿ���Ϊ����������ҪȡF��0���ľ���ֵ��
    //���յõ���ʽ��Ϊ��S-DS��*ln��-S+DS��-S+DS+��-DS*ln(DS)+DS)+50 S���ڣ�0��DS����ǰ���ʽ����Ҫ����k�����������ӳ�HP�Ĳ���
    //�õ�F��S��=k*����S-DS��*ln��-S+DS��-S+DS��+ ��-DS*ln(DS)+DS)+50
    //�����������������ޣ�S>DS ����-HP�仯��Ϊ F��X�������ֵ+ln��Ŀǰ��������Prefab�������ĵ���+1��*4*k  ln��+1��Ϊ�˷�ֹln1=0
    //�õ�ʽ�� F��max��+ln��S-Sets+1��*4*k S���ڣ�DS��+���
    public float Cacu_Curve(int x, float D_x, float k = 1)
    {
        if (D_x > 100)
        {
            D_x =10 * (float)Math.Sqrt(D_x);
        }
        float iD_x = D_x + 1;
        //+1������С����Ϊ1��Ĳ���

        float Tail_1 = 50;
        float f0 = Mathf.Abs(k * ((-iD_x) * Mathf.Log(iD_x) + iD_x));

        float fmax = D_x + Tail_1 + f0;

        if (x > (int)D_x)
        {
            return (fmax + k * 4 * Mathf.Log(x - (int)D_x + 1));
        }
        else
        {
            float j = 1;
            //����j<1ʹ�ó˷����е�б�ʱ仯��û���������ʱ������ֵ��X��ı䣬��Ҫ�ı�+1����С�޶Ȳ���
            float Product_1 = (x - iD_x) * Mathf.Log(iD_x - x) * j;
            float Sum_1 = iD_x - x;

            return k * (Product_1 + Sum_1) + Tail_1 + f0;
        }
    }



    public  Dictionary<string, Sprite> SpriteDict(List<string> strs, List<Sprite> sprites)
    {
        Dictionary<string, Sprite> result = new Dictionary<string, Sprite>();
        for (int i = 0; i < strs.Count && i < sprites.Count; i++)
        {
            result.Add(strs[i], sprites[i]);
        }
        return result;


    }
    public  Dictionary<string, SkeletonDataAsset> SpineDict(List<string> strs, List<SkeletonDataAsset> spines)
    {
        Dictionary<string, SkeletonDataAsset> result = new Dictionary<string, SkeletonDataAsset>();
        for (int i = 0; i < strs.Count && i < spines.Count; i++)
        {
            result.Add(strs[i], spines[i]);
        }
        return result;

    }

    public  Dictionary<Char_Race, List<string>> Sec_Race(List<Char_Race> races, List<string> strs)
    {
        Dictionary<Char_Race, List<string>> result = new Dictionary<Char_Race, List<string>>();

        List<List<string>> result1 = new List<List<string>>();

        result1.Add(new List<string>());
        int start = 0;
        for (int i = 0; i < strs.Count; i++)
        {
            if (strs[i] == "#")
            {
                result1.Add(new List<string>());
                result1[start].Add(strs[i]);
                start++;
                continue;
            }
            result1[start].Add(strs[i]);
        }
       // result1.Add(new List<string>(strs.GetRange(start, strs.Count - start)));


        for (int i = 0; i < races.Count && i < result1.Count; i++)
        {
            result.Add(races[i], result1[i]);
        }

        return result;
    }

}

