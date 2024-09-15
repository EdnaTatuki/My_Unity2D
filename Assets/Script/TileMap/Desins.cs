using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Desins : MonoBehaviour
{
    // Start is called before the first frame update
    public TileDes td;

    public void DrawSprite(SpriteRenderer sr, Vector3 location)
    {
        sr.sprite = td.Sprites;
        sr.transform.position = location;
        //Ïû³ýÅö×²
        if (td.passable)
        {
            Destroy(sr.GetComponent<BoxCollider2D>());
            Destroy(sr.GetComponent<Rigidbody2D>());
            Destroy(sr.GetComponent<CompositeCollider2D>());
        }
    }


}
