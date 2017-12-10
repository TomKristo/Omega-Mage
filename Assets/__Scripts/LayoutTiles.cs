using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class TileTex
{
    public string str;
    public Texture2D tex;
}

public class LayoutTiles : MonoBehaviour {

    static public LayoutTiles S;

    public TextAsset roomsText;
    public string roomNumber = "0";

    public GameObject tilePrefab;
    public TileTex[] tileTextures;

    public bool ________________;

    public PT_XMLReader roomsXMLR;
    public PT_XMLHashList roomsXML;
    public Tile[,] tiles;
    public Transform tileAnchor;

    private void Awake()
    {
        S = this;

        GameObject tAnc = new GameObject("TileAnchor");
        tileAnchor = tAnc.transform;

        roomsXMLR = new PT_XMLReader();
        roomsXMLR.Parse(roomsText.text);
        roomsXML = roomsXMLR.xml["xml"][0]["room"];

        BuildRoom(roomNumber);
    }

    public void BuildRoom(string rNumStr)
    {
        PT_XMLHashtable roomHT = null;
        for (int i = 0; i < roomsXML.Count; i++)
        {
            PT_XMLHashtable ht = roomsXML[i];

            if (ht.att("num") == rNumStr)
            {
                roomHT = ht;
                break;
            }
        }

        if (roomHT == null)
        {
            Utils.tr("ERROR", "LayoutTiles.BuildRoom()", "Room not found: " + rNumStr);
            return;
        }

        BuildRoom(roomHT);
    }

    public Texture2D GetTileTex(string tStr)
    {
        foreach (TileTex tTex in tileTextures)
        {
            if (tTex.str == tStr)
            {
                return (tTex.tex);
            }
        }
        return (null);
    }

    public void BuildRoom(PT_XMLHashtable room)
    {
        string floorTexStr = room.att("floor");
        string wallTexStr = room.att("wall");

        string[] roomRows = room.text.Split('\n');

        for (int i = 0; i < roomRows.Length; i++)
        {
            roomRows[i] = roomRows[i].Trim('\t');
        }

        tiles = new Tile[100, 100];

        Tile ti;
        string type, rawType, tileTexStr;
        GameObject go;
        int height;
        float maxY = roomRows.Length - 1;

        for (int y = 0; y < roomRows.Length; y++)
        {
            for (int x = 0; x < roomRows[y].Length; x++)
            {
                height = 0;
                tileTexStr = floorTexStr;

                type = rawType = roomRows[y][x].ToString();
                switch (rawType)
                {
                    case " ":
                    case "_":
                        continue;
                    case ".":
                        break;
                    case "|":
                        height = 1;
                        break;
                    default:
                        type = ".";
                        break;
                }

                if (type == ".")
                {
                    tileTexStr = floorTexStr;
                }
                else if (type == "|")
                {
                    tileTexStr = wallTexStr;
                }

                go = Instantiate(tilePrefab) as GameObject;
                ti = go.GetComponent<Tile>();

                ti.transform.parent = tileAnchor;

                ti.pos = new Vector3(x, maxY - y, 0);
                tiles[x, y] = ti;

                ti.type = type;
                ti.height = height;
                ti.tex = tileTexStr;
            }
        }
    }

}
