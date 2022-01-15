using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

class KeyboardObject : Object
{
    // Quad defining region of keyboard on screen
    internal GameObject m_KeyboardQuad; // set in editor
    // Prefab for individual keys
    internal GameObject m_KeyPrefab; // set in editor

    // Parent empty GameObject for grouping the keys
    internal GameObject m_KeyBoxParent;

    // Order/arrangement of the keys
    internal KeyboardArrangement m_KeyboardArrangement;

    // Dictionary of KeyboardCell objects for the keyboard
    internal Dictionary<char, KeyboardCell> m_Keyboard;


    internal KeyboardObject(GameObject keyboardQuad,
                            GameObject keyBoxParent,
                            GameObject keyPrefab,
                            KeyboardArrangement arrangement)
    {
        m_KeyboardQuad = keyboardQuad;
        m_KeyBoxParent = keyBoxParent;
        m_KeyPrefab = keyPrefab;
        m_KeyboardArrangement = arrangement;
        SetUpKeyboard();
    }

    internal void SetUpKeyboard()
    {
        float cellSize = 0.250f;
        float xValue = cellSize;
        m_Keyboard = new Dictionary<char, KeyboardCell>();
        for (char letter = 'A'; letter <= 'Z'; letter++)
        {
            Vector3 centre = new Vector3(xValue, 0, 0);
            m_Keyboard.Add(letter, new KeyboardCell(letter, m_KeyBoxParent));
            m_Keyboard[letter].ConfigureCell(centre, 1, m_KeyPrefab);
            xValue += cellSize;
        }
    }

    internal void Destroy()
    {
        foreach (var gameObject in m_KeyBoxParent.GetComponentsInChildren<Transform>())
        {
            GameObject.Destroy(gameObject.gameObject);
        }
    }
}
