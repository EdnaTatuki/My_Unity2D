using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DesIns : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TileDes_base TileDes_Base;


    public DesIns() {
       // show the sprite
    }
}
public class DesIns_Wall : DesIns
{
 
}

public class Building {
    public List<DesIns_Wall> Walls { get; set; }

    //need to split
    public List<DesIns_Wall> Duplicate_Walls { get; set; }
    public List<Vector3> Space { get; set; }
}