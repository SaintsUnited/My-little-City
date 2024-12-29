using UnityEngine;

public class BuildManager : MonoBehaviour
{
    private GridManager gridManager; //reference to grid manager
    private PlaceHolderController placeHolderController; //reference to placeholdercontroller
    
    private Vector3 mousePosition;

    public GameObject placeHolderPrefab;
    private GameObject placeHolderInstance;
    
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
        objectToPlace = null;
    }
    private void Update()
    {
        UpdateMousePosition();
        if (objectToPlace&& Input.GetMouseButtonDown(0)) //LMB clicked
        {
            PlaceObject();
        }
    }
    void UpdateMousePosition() //Updates mouse position
    {
        if (placeHolderInstance == null || gridManager == null) return;
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 point = hit.point;
            int x = Mathf.FloorToInt(point.x / gridManager.cellSize);
            int z = Mathf.FloorToInt(point.z / gridManager.cellSize);
            
            mousePosition = new Vector3(x * gridManager.cellSize, 0, z * gridManager.cellSize);
            placeHolderInstance.transform.position = mousePosition;

            if (gridManager.GetCellStatus(x, z))
            {
                placeHolderController.SetPlaceHolderColor(false); //Occupied
            }
            else
            {
                placeHolderController.SetPlaceHolderColor(true); //Available
            }
        }
    }
    public void SetObjectToPlace(GameObject newObject)
    {
        objectToPlace = newObject;
        placeHolderPrefab = newObject;

        if (placeHolderInstance != null)
        {
            Destroy(placeHolderInstance);
        }

        if (newObject != null)
        {
            placeHolderInstance = Instantiate(placeHolderPrefab, Vector3.zero, Quaternion.identity);
            placeHolderController = placeHolderInstance.GetComponent<PlaceHolderController>();
            
            if (placeHolderController == null)
            {
                Debug.LogError("PlaceHolderController not found!");
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
            ClearObjectToPlace();
            Debug.Log("Object placed!");
        }
        else
        {
            objectToPlace = null;
            Debug.Log("Cell is already occupied!");
        }
    }
    private void ClearObjectToPlace()
    {
        objectToPlace = null;

        if (placeHolderInstance != null)
        {
            Destroy(placeHolderInstance);
            placeHolderInstance = null;
        }
    }
}