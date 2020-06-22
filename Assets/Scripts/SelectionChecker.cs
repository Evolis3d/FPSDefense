using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//esto no va aqui
public enum ControlState
{
   None,
   CanConstruct,
   Occupied,
}
// --esto no va aqui


public class SelectionChecker : MonoBehaviour
{
    public Image cursor;
    public Color defaultColor;
    public Color canConstruct;
    public Color occupied;

    [SerializeField] private ControlState currentState;

    void Start()
    {
        currentState = ControlState.None;
    }

 
    public void SetCurrentState(ControlState newState)
    {
        currentState = newState;
        switch (currentState)
        {
            case ControlState.None:
                SetCursorColor(defaultColor);
                break;
            case ControlState.CanConstruct:
                SetCursorColor(canConstruct);
                break;
            case ControlState.Occupied:
                SetCursorColor(occupied);
                break;
            default:
                SetCursorColor(defaultColor);
                break;
        }
    }

    private void SetCursorColor(Color newColor)
    {
        cursor.color = newColor;
    }

    public bool IsOccupied()
    {
        return (currentState == ControlState.Occupied);
    }

    public bool CanConstruct()
    {
        return (currentState == ControlState.CanConstruct);
    }
    
    

}
