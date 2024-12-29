using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Movement speed")]
    public float moveSpeed = 10f;
    public float zoomSpeed = 5f;

    [Header("Zoom")]
    public float minZoom = 5f;
    public float maxZoom = 50f;

    [Header("Outline Config")]
    public float edgeThickness = 10f;
    public bool enableEdgeMovement = true;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("No camera found!");
        }
    }
    private void Update()
    {
        if (enableEdgeMovement)
        {
            HandleEdgeMovement(); // Mover cámara según bordes
        }

        HandleZoom(); // Controlar el zoom
    }
    private void HandleEdgeMovement()
    {
        Vector3 move = Vector3.zero;
        Vector2 mousePosition = Input.mousePosition; //Get mouse position
        
        if (mousePosition.x >= Screen.width - edgeThickness)
        {
            move += Vector3.right; //Move to the right
        }
        else if (mousePosition.x <= edgeThickness)
        {
            move += Vector3.left; //Move to the left
        }

        if (mousePosition.y >= Screen.height - edgeThickness)
        {
            move += Vector3.forward; //Move up
        }
        else if (mousePosition.y <= edgeThickness)
        {
            move += Vector3.back; //Move down
        }
        
        transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);
    }
    private void HandleZoom()
    {
        //Zoom
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            Vector3 position = mainCamera.transform.localPosition;
            position.y -= scroll * zoomSpeed;
            position.y = Mathf.Clamp(position.y, minZoom, maxZoom);
            mainCamera.transform.localPosition = position;
        }
    }
}
