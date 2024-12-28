using UnityEngine;

public class PlaceHolderController : MonoBehaviour
{
    public GameObject placeHolderPrefab; //Objecto que cambia de color
    [SerializeField] private Color validColor = Color.green;
    [SerializeField] private Color invalidColor = Color.red;
    
    public void SetPlaceHolderColor(bool isValid) //Cambia el color del objeto
    {
        if (!placeHolderPrefab)
        {
            Debug.LogError("PlaceHolder is not set");
            return;
        }
        
        Renderer rend = placeHolderPrefab.GetComponent<Renderer>();
        if (rend != null)
        {
            Material instanceMaterial = rend.material;
            rend.material.color = isValid ? validColor : invalidColor;
        }
        else
        {
            Debug.LogWarning("Renderer is not set");
        }
    }
}