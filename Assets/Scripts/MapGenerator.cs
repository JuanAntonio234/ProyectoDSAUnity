using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{

    public GameObject floorPrefab;
    public GameObject wallPrefab;
    public Tilemap floorTilemap;
    public Tilemap wallTilemap;

    public int mapWidth = 50;
    public int mapHeight = 50;
    public int maxRoomSize;


    // Start is called before the first frame update
    void Start()
    {
        GenerateMap();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GenerateMap()
    {
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                InstantiateTile(wallPrefab, x, y,wallTilemap);
            }
        }
        BSP(new Rect(0, 0, mapWidth, mapHeight));
    }

    void InstantiateTile(GameObject prefab, int x, int y, Tilemap tilemap)
    {
        Vector3Int cellPosition=new Vector3Int(x,y,0);
        GameObject tile=Instantiate(prefab,tilemap.GetCellCenterWorld(cellPosition),Quaternion.identity);

    }

    void BSP(Rect area)
    {
        //verificar si el area es suficientemente grande para dividir
        //si
        if (area.width > maxRoomSize || area.height>maxRoomSize)
        {
            maxRoomSize = Random.Range(5, 21);

            Rect leftArea, rightArea;
            //dividir el area en dos
            //horizontal
            if (Random.Range(0, 2) == 0)
            {
               
            }
            //vertical
            else
            {
               
            }

            BSP(leftArea);
            BSP(rightArea);
            
            ConnectAreas(leftArea, rightArea);
        }
        //no
        //creamos otra sala
        else
        {
            CreateRoom(area);
        }
    }

    //funcion para conectar la salas con un pasillo
    void CreateRoom(Rect area)
    {
        for(int x = Mathf.RoundToInt(area.x); x < Mathf.RoundToInt(area.xMax); x++)
        {
            for(int y = Mathf.RoundToInt(area.y);y < Mathf.RoundToInt(area.yMax); y++)
            {
                InstantiateTile(floorPrefab, x, y,floorTilemap);
                
                if(x == Mathf.RoundToInt(area.x) || x == Mathf.RoundToInt(area.xMax) - 1 ||
                y == Mathf.RoundToInt(area.y) || y == Mathf.RoundToInt(area.yMax) - 1)
                {
                    InstantiateTile(wallPrefab, x, y, wallTilemap);
                }
            }
        }
    }

    void ConnectAreas(Rect leftArea,Rect rightArea)
    {
        int pasilloX = Mathf.RoundToInt(leftArea.xMax);
        int pasilloY = Mathf.RoundToInt(leftArea.y + leftArea.height / 2);

        while (pasilloX < rightArea.x)
        {
            InstantiateTile(floorPrefab, pasilloX, pasilloY, floorTilemap);
            InstantiateTile(wallPrefab, pasilloX - 1, pasilloY - 1, wallTilemap);
            InstantiateTile(wallPrefab, pasilloX + 1, pasilloY + 1, wallTilemap);

            if (Random.Range(0, 2) == 0)
            {
                pasilloX++;
            }
            else
            {
                pasilloY += (pasilloY < Mathf.RoundToInt(rightArea.y + rightArea.height / 2)) ? 1 : -1;
            }
        }
    }
}
