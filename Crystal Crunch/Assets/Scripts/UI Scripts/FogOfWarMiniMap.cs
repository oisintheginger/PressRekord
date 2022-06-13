using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class FogOfWarMiniMap : MonoBehaviour
{   
    [SerializeField] float Width = 50;
    [SerializeField] float Height = 50;
    Tilemap FOWMap;
    void Start()
    {
        FOWMap = this.gameObject.GetComponent<Tilemap>();
        InvokeRepeating("UnveilMap", 1f, 1f);
    }
    
    void UnveilMap()
    {
        Vector3 PlayerToGridPos = PlayerStats._CurrentPlayerStats.transform.position;
        FOWMap.SetTile(FOWMap.WorldToCell(PlayerToGridPos), null);
        
        Vector3 Gap = FOWMap.cellSize/2;
        for(int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                Vector2 UnveilTilePos = (PlayerToGridPos - new Vector3(Width/2 * Gap.x, Height/2 * Gap.y, 0)) + new Vector3(Gap.x*x, Gap.y*y,0);
                FOWMap.SetTile(FOWMap.WorldToCell(UnveilTilePos), null);
            }
        }
        
    }
    
}
