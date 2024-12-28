using UnityEngine;

public class BuildMenuController : MonoBehaviour
{
    [SerializeField] private BuildManager buildManager;
    [SerializeField] private GameObject[] buildableObjects;
    
    public void SelectObject(int index)
    {
        if (index >= 0 && index < buildableObjects.Length)
        {
            buildManager.SetObjectToPlace(buildableObjects[index]);
            Debug.Log("Selected object: " + buildableObjects[index].name);
        }
        else
        {
            Debug.Log("Invalid object index selected!");
        }
    }
}