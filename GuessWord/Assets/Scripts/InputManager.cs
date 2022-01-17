

using UnityEngine;

internal class InputManager
{
    internal string m_KeyboardInput;
    internal KeyCode[] m_LetterKeyCodes;
    internal KeyCode[] m_EnterKeyCodes;
    internal KeyCode[] m_DeleteKeyCodes;

    internal InputManager()
    {
        SetUpKeyCodeLists();
        ClearKeyboardInput();
    }

    internal void ClearKeyboardInput()
    {
        m_KeyboardInput = "";
    }

    internal void SetUpKeyCodeLists()
    {
        m_LetterKeyCodes = new KeyCode[]
        {
            KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.E,
            KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.I, KeyCode.J,
            KeyCode.K, KeyCode.L, KeyCode.M, KeyCode.N, KeyCode.O,
            KeyCode.P, KeyCode.Q, KeyCode.R, KeyCode.S, KeyCode.T,
            KeyCode.U, KeyCode.V, KeyCode.W, KeyCode.X, KeyCode.Y, KeyCode.Z
        };
        m_EnterKeyCodes = new KeyCode[] {KeyCode.Return, KeyCode.KeypadEnter};
        m_DeleteKeyCodes = new KeyCode[] {KeyCode.Delete, KeyCode.Backspace};
    }

    internal bool ParseKeyboardInput()
    {
        foreach (KeyCode keyCode in m_LetterKeyCodes)
        {
            if (Input.GetKeyDown(keyCode))
            {
                m_KeyboardInput += keyCode.ToString();
            }
        }

        foreach (KeyCode keyCode in m_DeleteKeyCodes)
        {
            if (Input.GetKeyDown(keyCode))
            {
                if (m_KeyboardInput.Length > 0)
                {
                    m_KeyboardInput = m_KeyboardInput.Substring(0, m_KeyboardInput.Length - 1);
                }
            }
        }

        foreach (KeyCode keyCode in m_EnterKeyCodes)
        {
            if (Input.GetKeyDown(keyCode))
            {
                // callback()
                return true;
            }
        }

        return false;
    }
}