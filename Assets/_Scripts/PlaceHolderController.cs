using System;
using UnityEngine;

public class PlaceHolderController : MonoBehaviour
{
    public Renderer placeHolderRenderer; //Objecto que cambia de color

    private void Awake()
    {
        placeHolderRenderer = GetComponent<Renderer>();
    }
    public void SetPlaceHolderColor(bool isValid)
    {
        if (!placeHolderRenderer)
        {
            Debug.LogError("PlaceHolder is not set");
            return;
        }
        //Cambia el color del objeto
        Color placeHolderColor = isValid ? Color.green : Color.red;
        placeHolderRenderer.sharedMaterial.color = placeHolderColor;
    }
}