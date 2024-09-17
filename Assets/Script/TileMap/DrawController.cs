using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class DrawController : MonoBehaviour
{
    public SpriteRenderer STest;
    public TileDes_base TD;

    //[SerializeField] GameObject DesLevel;



    // Start is called before the first frame update
    void Start()
    {
        List<DesIns>Walls = new List<DesIns>();
        DesIns [] Walls_a = GetComponentsInChildren<DesIns>(true);
        Walls = Walls_a.ToList();
        
        GetBuilding(Walls);
    }

    // Update is called once per frame


    // take all walls in the map , return a building 
     public List<Building> GetBuilding (List<DesIns> Walls)
     {
        List<Building> build_result = new List<Building>();
        List<DesIns> wall_list = Walls;
        List<Vector3> poslist = GetWallPositionList(Walls);


        // step1 loop contllor
        int step1 = 0;
        int infiloop1 = 0;
         //Debug.Log(wall_list.Count);

        while (wall_list.Count != step1) {

            step1 = wall_list.Count;
 

        for (int i =0 ; i < wall_list.Count ; i++)
        {
            
            if(GetAjacentWallsNum(wall_list[i], wall_list) < 2) 
            {

                //test 
                wall_list[i].transform.GetComponent<SpriteRenderer>().color = Color.black;



                wall_list.Remove(wall_list[i]);
               

                continue;
            }

            if(GetAjacentWallsNum(wall_list[i], wall_list) == 2 && GetCornerWallsNum(wall_list[i], wall_list) == 1) 
            {
                if (IsWallatCorner(wall_list[i],wall_list))

                /* remove the wall  has just 3 adjacents in 8driction , 
                and 3 adjacents like : 
                ^   ^
                |  /
                | / __>

                ^   ^
                 \ |                 
                <__\|

                |\__>
                | \
                ,  ,

                
                <__|\
                   | \
                   ,   ,

                */

                {

                //test 
                wall_list[i].transform.GetComponent<SpriteRenderer>().color = Color.gray;
                wall_list.Remove(wall_list[i]);

                }
                
            }
        }

        //test infi loop
        infiloop1++;
        if(infiloop1 > 1000){
            Debug.Log("step1 infi loop");
            break;
        }


        }

        

        //step2
        //get min position
        // == 0
        if (wall_list.Count == 0) {
            goto END; 

            }


        DesIns min = wall_list[0];
        for (int i =0 ; i < wall_list.Count ; i++)
        {
            if(wall_list[i].transform.position.x < min.transform.position.x){
                min = wall_list[i];
            }
            if(wall_list[i].transform.position.y < min.transform.position.y){
                min = wall_list[i];
            }
        }

        




        // step2 loop contllor
        int infiloop2in = 0;


        //step2 loop 
        int newbuildindex = 0;
        DesIns start =min;
        DesIns end = null;
        Vector3 driction = Vector3.up;

        Building building = new Building();

         List<DesIns> res_wl = new List<DesIns>();
         // 1. noemal wall near the true duplicatepoint can be in this incoorrcetly
         
        List<DesIns> duplicatepoint = new List<DesIns>();


        while(wall_list.Count != 0)
        {

        

        
        while (end != start)
        {
            //frist time 
            if (end ==null){
                end = start;
            }

            if (!res_wl.Contains(end)){
                res_wl.Add(end);

            //test with color
            end.GetComponent<SpriteRenderer>().color = Color.red;


             }
             else{
                // duplicate more than one time
                 if (!duplicatepoint.Contains(end))
                duplicatepoint.Add(end);

             }


            if(IsExistWallsFormPos(end.transform.position+RotateVectorAroundZAxis(driction,-90f),wall_list))
            {
                driction = RotateVectorAroundZAxis(driction,-90f);
            }
            if(IsExistWallsFormPos(end.transform.position+driction,wall_list)){
                end = GetWallsFormPos(end.transform.position+driction,wall_list);
            }
            else{
                driction = RotateVectorAroundZAxis(driction,90f);
                if(IsExistWallsFormPos(end.transform.position+driction,wall_list))
                end = GetWallsFormPos(end.transform.position+driction,wall_list);
                
            }
            


            //test infi loop
            infiloop2in++;
            if(infiloop2in > 1000){
            Debug.Log("step2 in infi loop");
            break;
            }



        }

        //remove res_wl form wall_list
        wall_list.RemoveAll(item => res_wl.Contains(item));

        // find true duplicatepoint
        // split it
        
        build_result.Add(new Building());
        build_result[newbuildindex].Walls = res_wl;
        build_result[newbuildindex].Duplicate_Walls = duplicatepoint;
        newbuildindex++;


             //test infi loop
            newbuildindex++;
            if(newbuildindex > 1000){
            Debug.Log("step2 out infi loop");
            break;
            }


        }


        return build_result;



        END:

        return null;


     }
    public List<Vector3> GetWallPositionList (List<DesIns> Walls){
        List<Vector3> result = new List<Vector3>();
        for (int i =0 ; i < Walls.Count ; i++)
        {

            result.Add(Walls[i].transform.position);
        }
        return result;
    }

    public int GetAjacentWallsNum (DesIns mainDes, List<DesIns> Walls)
    {
        int result = 0;
        if(IsExistWallsFormPos(mainDes.transform.position+Vector3.up ,Walls))
        result++;
        if(IsExistWallsFormPos(mainDes.transform.position+Vector3.down ,Walls))
        result++;
        if(IsExistWallsFormPos(mainDes.transform.position+Vector3.left ,Walls))
        result++;
        if(IsExistWallsFormPos(mainDes.transform.position+Vector3.right ,Walls))
        result++;

        return result;

    }

    public int GetCornerWallsNum (DesIns mainDes, List<DesIns> Walls)
    {
        int result = 0;
        if(IsExistWallsFormPos(mainDes.transform.position+Vector3.up+Vector3.left ,Walls))
        result++;
        if(IsExistWallsFormPos(mainDes.transform.position+Vector3.down+Vector3.left ,Walls))
        result++;
        if(IsExistWallsFormPos(mainDes.transform.position+Vector3.up+Vector3.right ,Walls))
        result++;
        if(IsExistWallsFormPos(mainDes.transform.position+Vector3.up+Vector3.right ,Walls))
        result++;

        return result;

    }

    public bool IsWallatCorner (DesIns mainDes, List<DesIns> Walls)
    {
        if (IsExistWallsFormPos(mainDes.transform.position+Vector3.up,Walls) && IsExistWallsFormPos(mainDes.transform.position+Vector3.right,Walls) && IsExistWallsFormPos(mainDes.transform.position+Vector3.up+Vector3.right,Walls))
        return true;
        if (IsExistWallsFormPos(mainDes.transform.position+Vector3.up,Walls) && IsExistWallsFormPos(mainDes.transform.position+Vector3.left,Walls) && IsExistWallsFormPos(mainDes.transform.position+Vector3.up+Vector3.left,Walls))
        return true;
        if (IsExistWallsFormPos(mainDes.transform.position+Vector3.down,Walls) && IsExistWallsFormPos(mainDes.transform.position+Vector3.right,Walls) && IsExistWallsFormPos(mainDes.transform.position+Vector3.down+Vector3.right,Walls))
        return true;
        if (IsExistWallsFormPos(mainDes.transform.position+Vector3.down,Walls) && IsExistWallsFormPos(mainDes.transform.position+Vector3.left,Walls) && IsExistWallsFormPos(mainDes.transform.position+Vector3.down+Vector3.left,Walls))
        return true;


        return false;
    }   

    public bool IsExistWallsFormPos (Vector3 pos, List<DesIns> Walls)
    {
        for (int i =0 ; i < Walls.Count ; i++){
            if (Walls[i].transform.position == pos)
            {
                return true;
            }
        }
        return false;
    }
    public DesIns GetWallsFormPos (Vector3 pos, List<DesIns> Walls )
    {
            {
        for (int i =0 ; i < Walls.Count ; i++){
            if (Walls[i].transform.position == pos)
            {
                return Walls[i];
            }
        }
        Debug.Log("None Des in"+ pos );
        return null;
    }
    }


    public Vector3 RotateVectorAroundZAxis(Vector3 inputVector, float angle)
    {
        //Rotate at clock
        Quaternion rotation = Quaternion.Euler(0, 0, -angle); 
        return rotation * inputVector;
    }

}
