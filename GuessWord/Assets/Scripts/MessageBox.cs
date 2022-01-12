using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using UnityEditor;
using UnityEngine.Accessibility;
using UnityEngine.UI;

internal class MessageBox : Object
{
    internal GameObject m_GameGrid;
    internal Canvas m_CanvasObject;
    internal Button m_ButtonPrefab;

    // Actual game objects instantiated for message box
    internal Button m_YesButton;
    internal Button m_NoButton;
    internal GameObject m_Quad;
    internal GameObject m_QuadText;

    internal MessageBox(GameObject gameGrid, Canvas canvasObject, Button buttonPrefab)
    {
        m_GameGrid = gameGrid;
        m_CanvasObject = canvasObject;
        m_ButtonPrefab = buttonPrefab;
    }

    internal void Show(string message,
        float width = 3.0f, float height = 2.5f,
        float yOffset = 2.5f, float zOffset = -4.0f,
        int fontSize = 2)
    {
        SetUpQuad(width, height, yOffset,  zOffset);
        SetUpQuadText(message, width,  height, fontSize);
        SetYesButton(width, height);
        // SetNoButton(width, height);
    }

    internal void Destroy()
    {
        GameObject.Destroy(m_YesButton.gameObject);
        // GameObject.Destroy(m_NoButton.gameObject);
        GameObject.Destroy(m_Quad.gameObject);
        GameObject.Destroy(m_QuadText.gameObject);
    }

    internal void SetUpQuadText(string message, float width, float height, int fontSize)
    {
        // Set the text message
        m_QuadText = new GameObject("Message");
        TextMeshPro text = m_QuadText.AddComponent<TextMeshPro>();
        text.text = message;
        text.color = Color.black;
        text.fontSize = fontSize;
        text.transform.position = m_Quad.transform.position;
        text.rectTransform.sizeDelta = new Vector2(width * .9f, height * .9f);
    }
    internal void SetUpQuad(float width, float height, float yOffset, float zOffset)
    {
        // Set up the modal/dialog box
        m_Quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        m_Quad.transform.position = m_GameGrid.transform.position + new Vector3(0, yOffset, zOffset);
        m_Quad.transform.localScale = new Vector3(width, height, 1.0f);
    }
    internal void SetYesButton(float width, float height)
    {
        m_YesButton = GetButton(width, height);
    }

    internal void SetNoButton(float width, float height)
    {
        m_NoButton = GetButton(width, height, -140.0f, "No");
    }

    internal Button GetButton(float width, float height, float xPos=140.0f, string message="Yes")
    {
        Button buttonGameObject = Instantiate(m_ButtonPrefab, new Vector3(xPos, 340.0f, 5.5f), Quaternion.identity);
        buttonGameObject.transform.localScale = new Vector3(3.5f, 3.5f, 3.5f);

        var rectTransform = buttonGameObject.GetComponent<RectTransform>();
        rectTransform.SetParent(m_CanvasObject.transform, false);
        rectTransform.sizeDelta = new Vector2(60.0f, 35.0f);

        Button button = buttonGameObject.GetComponent<Button>();
        // m_Button.onClick.AddListener(Callback);
        TextMeshProUGUI textMesh = buttonGameObject.GetComponentInChildren<TextMeshProUGUI>();
        textMesh.fontSize = 15;
        textMesh.text = message;
        textMesh.rectTransform.sizeDelta = new Vector2(width * .9f, height * .9f);
        return buttonGameObject;
    }
}
