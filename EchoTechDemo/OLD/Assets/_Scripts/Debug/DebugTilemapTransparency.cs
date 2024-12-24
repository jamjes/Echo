using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DebugTilemapTransparency : MonoBehaviour
{
    public bool TransparencyOn;
    public Tilemap[] maps;
    public SpriteRenderer[] platforms;


    private void Update()
    {
        if (maps.Length > 0)
        {
            if (TransparencyOn && maps[0].color.a != 0)
            {
                foreach (Tilemap m in maps)
                {
                    m.color = new Color(m.color.r, m.color.g, m.color.b, 0);
                }

                foreach (SpriteRenderer s in platforms)
                {
                    s.color = new Color(s.color.r, s.color.g, s.color.b, 0);
                }
            }
            else if (!TransparencyOn && maps[0].color.a == 0)
            {
                foreach (Tilemap m in maps)
                {
                    m.color = new Color(m.color.r, m.color.g, m.color.b, 100);
                }

                foreach (SpriteRenderer s in platforms)
                {
                    s.color = new Color(s.color.r, s.color.g, s.color.b, 100);
                }
            }
        }
    }
}
