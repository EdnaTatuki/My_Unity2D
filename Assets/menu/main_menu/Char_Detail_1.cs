using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using TMPro;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using Unity.VisualScripting;

public class Char_Detail_1 : MonoBehaviour
{
    [SerializeField] TMP_Text Nmae;

    [SerializeField] TMP_Text Gender;

    [SerializeField] TMP_Text Max_HP;
    [SerializeField] TMP_Text Max_SP;
    [SerializeField] TMP_Text Max_MP;

    [SerializeField] VerticalLayoutGroup BPContent;


    public void SetData(Charatcater_I charater)
    {
        ShowText(Nmae, charater.Char_base_Stat.Char_Name);
        Gender.text = charater.Char_base_Stat.SEX.ToString();
        Max_HP.text = charater.Max_HP.ToString();
        Max_SP.text = charater.Max_SP.ToString();
        Max_MP.text = charater.Max_MP.ToString();

        SetBPS(BPContent.transform.GetChild(0),charater.Char_BPS[0]);
        for (int i = 1;i< charater.Char_BPS.Count; i++)
        {
            Transform bpc1 = Instantiate(BPContent.transform.GetChild(0), BPContent.transform);
            SetBPS(BPContent.transform.GetChild(i), charater.Char_BPS[i]);
        }

    }


    public void ShowText(TMP_Text _test,string _showtest,string language="sc")
    {
        //���汨������������
        /*
        if (language == "jp")
        {
            Font _jp = Resources.Load<Font>("MSYH SDF 1");
            TMP_FontAsset asset = TMP_FontAsset.CreateFontAsset(_jp);
            _test.font = asset;

        }*/
        _test.text = _showtest;
    }

    public void SetBPS(Transform trans , BodyPS bp)
    {
        TMP_Text bpshow = trans.Find("BPName").GetComponent<TMP_Text>();
        ShowText(bpshow, bp.BPType.ToString());
        bpshow = trans.Find("BPSTR").GetComponent<TMP_Text>();
        ShowText(bpshow, System.Math.Round(bp.Strength).ToString());
        bpshow = trans.Find("BPDEX").GetComponent<TMP_Text>();
        ShowText(bpshow, System.Math.Round(bp.Dexterity).ToString());
        bpshow = trans.Find("BPSTA").GetComponent<TMP_Text>();
        ShowText(bpshow, System.Math.Round(bp.Stamina).ToString());
        bpshow = trans.Find("BPINT").GetComponent<TMP_Text>();
        ShowText(bpshow, System.Math.Round(bp.Intellgence).ToString());
        bpshow = trans.Find("BPSPT").GetComponent<TMP_Text>();
        ShowText(bpshow, System.Math.Round(bp.Spirit).ToString());
        bpshow = trans.Find("BPSPD").GetComponent<TMP_Text>();
        ShowText(bpshow, System.Math.Round(bp.Speed).ToString());
    }



}
