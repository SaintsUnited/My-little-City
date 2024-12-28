using UnityEngine;

public class GridVisualizer : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    [SerializeField] private Color gridColor = Color.white;

    private void OnDrawGizmos()
    {
        if (gridManager == null) return;
        
        Gizmos.color = gridColor;
        for (int x = 0; x <= gridManager.gridWidthX; x++)
        {
            Gizmos.DrawLine(
                new Vector3(x * gridManager.cellSize, 0, 0), 
                new Vector3(x * gridManager.cellSize, 0, gridManager.gridHeightZ * gridManager.cellSize));
        }
        for (int z = 0; z <= gridManager.gridHeightZ; z++)
        {
            Gizmos.DrawLine(
                new Vector3(0, 0, z * gridManager.cellSize), 
                new Vector3(gridManager.gridWidthX * gridManager.cellSize, 0, z * gridManager.cellSize));
        }
    }
}
