using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject SlotFillBase;
    public List<BoardSlot> Slots;
    public BoardGroups GroupManager;

    public int gridHeight = 7;
    public int gridWidth  = 6;

    public int[,] grid;
    void Start()
    {
        GroupManager = gameObject.GetComponent<BoardGroups>();
        grid = new int[gridWidth, gridHeight];
        GetChildren();
        PopulateGrid();
    }

    private void GetChildren()
    {
        for(int i=0; i<transform.childCount; i++)
        {
            Slots.Add(transform.GetChild(i).gameObject.GetComponent<BoardSlot>());
        }
    }

    private void PopulateGrid()
    {
        int x = 0;
        int y = 0;
        for(int i=0; i<transform.childCount; i++)
        {
            grid[x,y] = i;
            Slots[i].GetComponent<BoardSlot>().GridCoords = new Vector2(x,y);
            x++;
            if (x % gridWidth == 0)
            {
                x = 0;
                y++;
            }
        }
    }
}
