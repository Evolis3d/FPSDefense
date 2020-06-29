using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(OleadaGenerica))]
[CanEditMultipleObjects]
public class OleadaGenerica_Editor : Editor
{
    SerializedProperty m_tipo;
    private SerializedProperty m_points;
    SerializedProperty m_lista;
    SerializedProperty m_cantidad;
    SerializedProperty m_freq;

    private string textoHelpbox;
    private bool usarCantidad = true;
    
    void OnEnable()
    {
        m_tipo = serializedObject.FindProperty("tipo");
        m_points = serializedObject.FindProperty("spawnPoints");
        m_lista = serializedObject.FindProperty("prefabEnemies");
        m_cantidad = serializedObject.FindProperty("cantidad");
        m_freq = serializedObject.FindProperty("frecuencia");
    }
    
    public override void OnInspectorGUI()
    {
        string helpbox1 = "Se lanza una cantidad fija de enemigos de un mismo tipo. Si se especifican varios prefabs, se hará un Random de ellos.";
        string helpbox2 = "Se lanza por orden secuencial la lista de enemigos indicados por su prefab.";

        EditorGUILayout.PropertyField(m_tipo);
        
        if (m_tipo.enumValueIndex == 0)
        {
            textoHelpbox = helpbox1;
            usarCantidad = true;
        }
        else if (m_tipo.enumValueIndex == 1)
        {
            textoHelpbox = helpbox2;
            usarCantidad = false;
        }
        
        EditorGUILayout.HelpBox(textoHelpbox, MessageType.None);
        EditorGUILayout.Separator();

        if (usarCantidad) EditorGUILayout.PropertyField(m_cantidad);
        EditorGUILayout.Separator();
        
        EditorGUILayout.PropertyField(m_lista,true);
        
        EditorGUILayout.Separator();
        EditorGUILayout.PropertyField(m_freq);
        
        EditorGUILayout.Separator();
        EditorGUILayout.PropertyField(m_points, true);

        serializedObject.ApplyModifiedProperties();
    }
}
