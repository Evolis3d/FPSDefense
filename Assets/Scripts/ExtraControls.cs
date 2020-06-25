using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public enum ItemSelectionType
{
    Abs,
    Cycle,
}

public class ExtraControls : MonoBehaviour
{
    private Camera _mainCam;
    private ListaSlots _lista;
    private SelectionChecker _selectionChecker;

    [Header("Seleccion de Trampas")] 
    public ItemSelectionType itemSelectionMode;
    
    
    [Header("Prefabs")]
    public List<GameObject> prefabTrampa;
    private int _currentTrampa;
    
    // Start is called before the first frame update
    void Start()
    {
        _mainCam = Camera.main;
        _lista = GetComponent<ListaSlots>();
        _selectionChecker = GetComponent<SelectionChecker>();
        _currentTrampa = 0;
    }

    // Update is called once per frame
    void Update()
    {

        var ray = _mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;
            if (selection && selection.CompareTag("slot"))
            {
               _selectionChecker.SetCurrentState(ControlState.CanConstruct);
               if (Input.GetMouseButton(0))
               {
                   var col = selection.GetComponent<Collider>();
                   Build(selection);
                   col.enabled = false;
               }
            }
            else if (selection && selection.CompareTag("trampa"))
            {
                _selectionChecker.SetCurrentState(ControlState.Occupied);    
            }
            else
            {
                _selectionChecker.SetCurrentState(ControlState.None);
            }
        }

        
        //para debug...
        if (Input.GetKeyDown(KeyCode.Escape)) Debug.Break();
        
        //select trampa, 1,2,3
        if (Input.GetKeyDown(KeyCode.Alpha1)) _currentTrampa = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2)) _currentTrampa = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3)) _currentTrampa = 2;
        
        
        //selector de trampas con la rueda del mouse
        if(Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            if(Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                _currentTrampa++;                    
            }
            if(Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                _currentTrampa--;
            }
        }

        if (itemSelectionMode == ItemSelectionType.Cycle)        //ciclico
        {
            if (_currentTrampa > 2) _currentTrampa = 0;
            if (_currentTrampa < 0) _currentTrampa = 2;       
        }
        else                                                     //abosluto 
        {
            _currentTrampa = Mathf.Clamp(_currentTrampa,0,2);          
        }
        //print(_currentTrampa);
        
    }


    private void Build(Transform parentSlot)
    {
        if (parentSlot)
        {
            GameObject clone = Instantiate(prefabTrampa[_currentTrampa], parentSlot);
            clone.transform.position = parentSlot.position;
            clone.transform.SetParent(null);
        }
    }

}
