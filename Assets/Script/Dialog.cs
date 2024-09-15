using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using Spine.Unity;
using static Unity.VisualScripting.Icons;
using UnityEngine.TextCore.Text;
using Unity.UI;
using static UnityEditor.FilePathAttribute;
using UnityEngine.Assertions.Must;
using Spine;
using UnityEngine.UIElements;
using System;
using UnityEngine.UI;
using System.Diagnostics.Tracing;
using UnityEngine.SceneManagement;

public class DialogManager : MonoBehaviour
{

    //DialogUi
    public GameObject DialogUiCa;

    //Camera Main
    public Camera CameraMain;

    //Charater Img

    public List<SpriteRenderer> CharSpritel;
    public List<SkeletonAnimation> SkeletonAnimation;


    //charater name
    public TMP_Text CharName;
    //dialog content
    public TMP_Text CharContent;

    public RectTransform TextPanle;

    //Type letter by letter
    public int lettersPersecond;
    int lettersPersecondtmp;


    //isTpying
    bool isTying;
    //isStoppong
    bool isStopping;
    //isStoryOver
    public bool isOver;




    //Story
    public StoryText StoryToTlak;
    public int Index;

    //BackGround
    public SpriteRenderer BackGround;
    //CG
    public RectTransform CG;

    //Dialoging Char
    public List<Charatcater_I> Dialoging_Char;


    //EffeectMusic
    public AudioClip EffectM;

    
    public static DialogManager instance;

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

    public void StartIndex()
    {
        Index = 0;
        Dialoging_Char = new List<Charatcater_I>();
        isOver = false;
        GameObject _duio = Resources.Load<GameObject>("UI/DUICamera");
        DialogUiCa = Instantiate(_duio);
        CameraMain = Camera.main;
        
        //DialogUiCa.transform.Find("DialogUI").gameObject.GetComponent<Canvas>().worldCamera = CameraMain;
        DialogUiCa.transform.Find("CG").SetParent(CameraMain.transform, false);
        Transform DialogUI = DialogUiCa.transform.Find("DialogUI");
        DialogUI.SetParent(CameraMain.transform, false);
        Transform Dialogback = DialogUiCa.transform.Find("back");
        Dialogback.SetParent(CameraMain.transform, false);
        FillToCamera(Dialogback);
        DialogUI _dui = DialogUiCa.GetComponent<DialogUI>();
        CharSpritel = _dui.CharSpritel;
        SkeletonAnimation= _dui.SkeletonAnimation;
        CharName=_dui.CharName;
        CharContent=_dui.CharContent;
        TextPanle=_dui.TextPanle;
        BackGround=_dui.BackGround;
        CG=_dui.CG;
        Destroy(DialogUiCa);

    }

    //根据Index 和 StoryToTlak来Update，Index是StoryTlak中Line的索引

    public void DialogUpdate()
    {
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter)) && isTying)
            {
                if (lettersPersecond != 999)
                {
                    lettersPersecondtmp = lettersPersecond;
                }
                lettersPersecond = 999;
            }
            if (!isTying && !isStopping)
            {

                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter)||Index==0)
                {
                    if (lettersPersecond == 999)
                    {
                        lettersPersecond = lettersPersecondtmp;
                    }


                    if (StoryToTlak.StoryLine_list[Index].L_Type != "CG" && Index!=0)
                    {
                        SoundManager.instance.PlaySound(EffectM);
                    }
                    
                    TextPanle.gameObject.SetActive(true);
                    CG.gameObject.SetActive(false);



                //这样写的话文本里要先切换场景再切换BGM
                    if (StoryToTlak.StoryLine_list[Index].L_Type == "ChangeBg")
                    {
                    if (StoryToTlak.StoryLine_list[Index].Line == "None")
                    {
                        BackGround.sprite = null;
                    }
                    else
                    {
                        ChangeBg(StoryToTlak.StoryLine_list[Index].Line);
                    }
                        Index++;
                    }

                    if (StoryToTlak.StoryLine_list[Index].L_Type == "ChangeBGM")
                    {
                    SoundManager.instance.ChangeBGM(StoryToTlak.StoryLine_list[Index].Line);
                    Index++;
                    }

                   if (StoryToTlak.StoryLine_list[Index].L_Type == "Stop")
                    {
                        StartCoroutine(StopWait()); 

                    }

                    if (StoryToTlak.StoryLine_list[Index].L_Type == "CG")
                    {
                        ChangeCG(StoryToTlak.StoryLine_list[Index].Line);
                        StartCoroutine(CGWait());

                    }

                    if (StoryToTlak.StoryLine_list[Index].L_Type == "Dialog")
                    {
                        CharSpeak(StoryToTlak.StoryLine_list[Index].s_char, StoryToTlak.StoryLine_list[Index].Line, StoryToTlak.StoryLine_list[Index].Font,
                            StoryToTlak.StoryLine_list[Index].Location, StoryToTlak.StoryLine_list[Index].Img_Name);
                        
                    }

                    if (StoryToTlak.StoryLine_list[Index].L_Type == "Show")
                    {
                        ShowMsg(StoryToTlak.StoryLine_list[Index].s_char_name, StoryToTlak.StoryLine_list[Index].Line, StoryToTlak.StoryLine_list[Index].Font);
                    }

                    if (StoryToTlak.StoryLine_list[Index].L_Type == "End")
                    {
                        Dialoging_Char.Clear();
                   

                        isOver = true;
                        StoryManager.instance.isStoryMode = false;               
                }

                if (StoryToTlak.StoryLine_list[Index].L_Type != "End"){ 
                    Index++;
                    }


                    //next
                }
            }  
    }


    public  void CharSpeak(Charatcater_I _charater,string content, string _Font = "sc", int location = 0,string _imgname="normal")
    {
        AddCharDialog(_charater, location, _imgname);
        ShowDiglog(_charater.Char_base_Stat.Char_name, content, _Font);

    }


    public  void ShowDiglog(string _name,string _content, string _Font = "sc")
    {

        CharName.text= _name;
        StartCoroutine(TypeDiglog(_content));
        
        
    }

    public void ShowMsg(string _name,string _content, string _Font = "sc")
    {
        CharName.text = _name;
        //CharName.gameObject.SetActive(false);
        StartCoroutine(TypeDiglog(_content));

    }



    public  IEnumerator TypeDiglog(string _content)
    {
                isTying = true;
                CharContent.text = "";
                foreach (var letter in _content.ToCharArray())
                {
                    CharContent.text += letter;
                    yield return new WaitForSeconds(1f / lettersPersecond);
                }
                isTying = false;
               // lettersPersecond = 30;

    }

    public IEnumerator StopWait(float time=1)
    {
        isStopping = true;
        TextPanle.gameObject.SetActive(false);
        yield return new WaitForSeconds(time);
        isStopping = false;
    }


    public IEnumerator CGWait(float time = 1)
    {
        isStopping = true;
        TextPanle.gameObject.SetActive(false);

        CG.gameObject.SetActive(true);
        yield return new WaitForSeconds(time);
        isStopping = false;
    }


    //location 位置，_Font使用的字体
    public void ShowImg(Sprite _char, int location = 0)
    {
        CharSpritel[location].color = Color.white;
        CharSpritel[location].gameObject.SetActive(true);
        CharSpritel[location].sortingOrder = 6;
        CharSpritel[location].sprite = _char; ;
        for (int i = 0; i < 6; i++)
        {
            if (i != location)
            {
                CharSpritel[i].color = Color.gray;
                CharSpritel[i].sortingOrder = i;

                

            }

            //grey all spine
            SkeletonAnimation[i].Skeleton.SetColor(Color.gray);
            SkeletonAnimation[i].GetComponent<MeshRenderer>().sortingOrder = i;


            //disable match spine
            if (i == location)
            {
                SkeletonAnimation[location].gameObject.SetActive(false);
            }

        }
    }




    public  void ShowSpine(SkeletonDataAsset _skelData,int location=0,int _isnew=0)
    {

        if (_isnew == 0) {
            SkeletonAnimation[location].gameObject.SetActive(true);
            SkeletonAnimation[location].skeletonDataAsset = _skelData;
            SkeletonAnimation[location].Initialize(true);
            SkeletonAnimation[location].AnimationState.SetAnimation(0, "idle", true);
            SkeletonAnimation[location].Skeleton.SetColor(Color.white);
            SkeletonAnimation[location].GetComponent<MeshRenderer>().sortingOrder = 6;
        }
        else
        {
            SkeletonAnimation[location].Skeleton.SetColor(Color.white);
            SkeletonAnimation[location].GetComponent<MeshRenderer>().sortingOrder = 6;
        }
        for (int i = 0; i < 6; i++)
        {
            if (i != location)
            {
                SkeletonAnimation[i].Skeleton.SetColor(Color.gray);
                SkeletonAnimation[i].GetComponent<MeshRenderer>().sortingOrder = i;
            }
            //grey all sprite
            CharSpritel[i].color = Color.gray;
            CharSpritel[i].sortingOrder = i;


            //disable match sprite
            if (i == location)
            {
                CharSpritel[location].gameObject.SetActive(false);
            }

        }

    }


    public void AddCharDialog(Charatcater_I _charater, int location = 0, string _imgname = "normal")
    {
        int _new= 0;
        if (_charater.Char_base_Stat.IllustrationType == "Spine")
        {
            //var new is to set Spine Animation show smoothly
            if(Dialoging_Char.Contains(_charater)) {
                _new = 1;
            }
            else
            {
                Dialoging_Char.Add(_charater);
            }
            
            ShowSpine(_charater.Char_Spine[_imgname], location,_new);
        }
        else
        {
            ShowImg(_charater.Char_Sprite[_imgname], location);
        }
    }


    public void ChangeBg(string _bg)
    {
        _bg = "Bg/" + _bg;
        Sprite BgFile = Resources.Load<Sprite>(_bg);
        //BackGround.GetComponent<UnityEngine.UI.Image>().sprite= BgFile;
        BackGround.sprite = BgFile;


    }

    public void ChangeCG(string _cg)
    {
        _cg = "CG/" + _cg;
        Sprite CGFile = Resources.Load<Sprite>(_cg);
        CG.GetComponent<UnityEngine.UI.Image>().sprite = CGFile;


    }


    /// <summary>
    /// 使sprite铺满整个屏幕
    /// </summary>
    public void FillToCamera(Transform _objanimator)
    {


         SpriteRenderer m_AnimatorSprite = _objanimator.GetComponent<SpriteRenderer>();
        

        Vector3 scale = _objanimator.transform.localScale;
        float cameraheight = Camera.main.orthographicSize * 2;
        float camerawidth = cameraheight * Camera.main.aspect;

        if (cameraheight >= camerawidth)
        {
            scale *= cameraheight / m_AnimatorSprite.bounds.size.y;
        }
        else
        {
            scale *= camerawidth / m_AnimatorSprite.bounds.size.x;
        }
        _objanimator.transform.localScale = scale;
        _objanimator.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, _objanimator.transform.position.z);
    }


}
