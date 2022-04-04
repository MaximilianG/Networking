using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class PlayerData
{
    public string _pseudoInputField;

    public PlayerData (MenuManager _menuManager)
    {
        _pseudoInputField = _menuManager.GetPseudoInputField();
    }
}
