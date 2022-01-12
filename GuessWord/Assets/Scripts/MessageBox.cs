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
    internal GameObject m_Modal;

    internal MessageBox(GameObject gameGrid, Canvas canvasObject, Button buttonPrefab)
    {
        m_GameGrid = gameGrid;
        m_CanvasObject = canvasObject;
        m_ButtonPrefab = buttonPrefab;
    }

    internal void Show(string message)
    {
        Color modalColor = Color.grey;
        Color textColor = Color.black;

        // hack
        float yOffset = 125.0f;

        float width = 600.0f;
        float height = 300.0f + yOffset;
        float padding = 50.0f;

        Vector3 pos = new Vector3(540.0f, 620.0f, 0);

        SetUpModalBackground(pos, width, height, modalColor);
        SetUpModalText(message, pos, width, height, padding, textColor);

        float buttonWidth = 220.0f;
        float buttonHeight = 120.0f;
        float buttonXPos = 140.0f;
        float buttonYPos = -400.0f - yOffset/2.0f;
        Vector3 yesButtonPos = new Vector3(buttonXPos, buttonYPos, 0);
        Vector3 noButtonPos = new Vector3(-1.0f * buttonXPos, buttonYPos, 0);
        int fontSize = 36;

        SetYesButton(buttonWidth, buttonHeight, yesButtonPos, padding/2.0f, fontSize);
        SetNoButton(buttonWidth, buttonHeight, noButtonPos, padding/2.0f, fontSize);
    }

    internal void SetUpModalBackground(Vector3 pos, float width, float height, Color modalColor)
    {
        // Set up modal and parent it under the canvas
        // Parent object will be a grey rectangle
        m_Modal = new GameObject("Modal");
        m_Modal.transform.SetParent(m_CanvasObject.transform);

        // Set up solid color image of dimension width x height
        CanvasRenderer renderer = m_Modal.AddComponent<CanvasRenderer>();
        renderer.cullTransparentMesh = true;
        Image image = m_Modal.AddComponent<Image>();
        image.color = modalColor;
        RectTransform rectTransform = m_Modal.GetComponent<RectTransform>();
        rectTransform.position = pos;
        rectTransform.sizeDelta = new Vector2(width, height);
    }

    internal void SetUpModalText(
                   string message,
                   Vector3 pos,
                   float width,
                   float height,
                   float padding,
                   Color textColor)
    {
        // Set up child GameObject for the text
        GameObject modalText = new GameObject("ModalText");
        modalText.transform.SetParent(m_Modal.transform);
        TextMeshProUGUI text = modalText.AddComponent<TextMeshProUGUI>();
        text.text = message;
        text.color = textColor;

        RectTransform textRectTransform = modalText.GetComponent<RectTransform>();
        textRectTransform.position = pos;
        textRectTransform.sizeDelta = new Vector2(width - padding, height - padding);
    }

    internal void Destroy()
    {
        GameObject.Destroy(m_YesButton.gameObject);
        GameObject.Destroy(m_NoButton.gameObject);
        GameObject.Destroy(m_Modal.gameObject);
    }

    internal void SetYesButton(float width, float height, Vector3 pos, float padding, int fontSize)
    {
        m_YesButton = GetButton("Yes", width, height, pos, padding, fontSize);
    }

    internal void SetNoButton(float width, float height, Vector3 pos, float padding, int fontSize)
    {
        m_NoButton = GetButton("No", width, height, pos, padding, fontSize);
    }

    internal Button GetButton(
                     string message,
                     float width,
                     float height,
                     Vector3 buttonPosition,
                     float padding,
                     int fontSize)
    {
        Button buttonGameObject = Instantiate(m_ButtonPrefab, buttonPosition, Quaternion.identity);
        buttonGameObject.transform.SetParent(m_CanvasObject.transform, false);

        var rectTransform = buttonGameObject.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(width, height);

        // TODO: find a way to get the delegate working so we can define callback here
        // Button button = buttonGameObject.GetComponent<Button>();
        // button.onClick.AddListener(Callback);
        TextMeshProUGUI textMesh = buttonGameObject.GetComponentInChildren<TextMeshProUGUI>();
        textMesh.fontSize = fontSize;
        textMesh.text = message;
        textMesh.rectTransform.sizeDelta = new Vector2(width - padding, height - padding);
        return buttonGameObject;
    }
}
