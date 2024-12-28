using UnityEngine;

public class BuildManager : MonoBehaviour
{
    private GridManager gridManager; //reference to grid manager
    private PlaceHolderController placeHolderController;
    
    private Vector3 mousePosition;

    private GameObject placeHolder;
    public GameObject objectToPlace;

    private void Start()
    {
        if (gridManager == null)
        {
            gridManager = GetComponent<GridManager>();
            if (gridManager == null)
            {
                Debug.LogError("GridManager not found!");
            }
        }

        if (placeHolder == null)
        {
            Debug.LogError("PlaceHolder not found!");
        }
        
        placeHolderController = placeHolder.GetComponent<PlaceHolderController>();
        if (placeHolderController == null)
        {
            Debug.LogError("PlaceHolderController not found!");
        }
    }
    private void Update()
    {
        UpdateMousePosition();
        if (Input.GetMouseButtonDown(0)) //LMB clicked
        {
            PlaceObject();
        }
    }
    
    void UpdateMousePosition() //Actualiza la posicion del mouse
    {
        if (placeHolder == null || gridManager == null) return;
        
        Ray ray = (Camera.main.ScreenPointToRay(Input.mousePosition));
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 point = hit.point;
            int x = Mathf.FloorToInt(point.x / gridManager.cellSize);
            int z = Mathf.FloorToInt(point.z / gridManager.cellSize);
            mousePosition = new Vector3(x * gridManager.cellSize, 0, z * gridManager.cellSize);
            placeHolder.transform.position = mousePosition;

            if (x >= 0 && z >= 0 && x < gridManager.gridWidthX && z < gridManager.gridHeightZ)
            {
                if (gridManager.IsCellAvailable(x, z))
                {
                    placeHolderController.SetPlaceHolderColor(true);
                }
                else
                {
                    placeHolderController.SetPlaceHolderColor(false);
                }
            }
            else //si esta fuera de los limites, marca invalido.
            {
                placeHolderController.SetPlaceHolderColor(false);
            }
        }
    }
    void PlaceObject()
    {
        if (gridManager == null || objectToPlace == null) return;
        
        int x = Mathf.FloorToInt(Input.mousePosition.x / gridManager.cellSize);
        int z = Mathf.FloorToInt(Input.mousePosition.y / gridManager.cellSize);
        if (gridManager.IsCellAvailable(x, z))
        {
            Instantiate(objectToPlace, mousePosition, Quaternion.identity);
            gridManager.OccupyCell(x, z);
        }
        else
        {
            Debug.Log("Cell is already occupied!");
        }
    }
}