using UnityEngine;

public class GridManager : MonoBehaviour
{
    private bool[,] grid;
    public int gridWidthX = 10;
    public int gridHeightZ = 10;
    public float cellSize = 1f;
    private void Start()
    {
        grid = new bool[gridWidthX, gridHeightZ];
    }
    public bool IsCellAvailable(int x, int z)
    {
        return x >= 0 && z >= 0 && x < gridWidthX && z < gridHeightZ && !grid[x, z];
    }
    public void OccupyCell(int x, int z)
    {
        if (x >= 0 && x < gridWidthX && z >= 0 && z < gridHeightZ)
        {
            grid[x, z] = true;
        }
    }

    public bool GetCellStatus(int x, int z)
    {
        if (x >= 0 && x < gridWidthX && z >= 0 && z < gridHeightZ)
        {
            return grid[x, z];
        }
        return false; //False if outside bounds
    }
}