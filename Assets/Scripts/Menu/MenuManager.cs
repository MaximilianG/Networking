using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    [SerializeField] Menu[] menus;

    private string _pseudoInputField;

    private void Awake()
    {
        Instance = this;

        _pseudoInputField = "";

        LoadPseudo();
    }

    private void Start()
    {
        LoadPseudo();
        openMenu("MainMenu");
    }

    public string GetPseudoInputField()
    {
        return _pseudoInputField;
    }

    public void openMenu(string _menuName)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].name == _menuName)
            {
                menus[i].openMenu();
            }
            else
            {
                menus[i].closeMenu();
            }
        }
    }

    public void setInputfieldText(string _name)
    {
        _pseudoInputField = _name;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Menu" && Input.GetKeyDown(KeyCode.Escape))
            openMenu("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void SavePseudo()
    {
        SaveSystem.SavePseudo(this);
    }

    public void LoadPseudo()
    {
        PlayerData data = SaveSystem.LoadPseudo();

        _pseudoInputField = data._pseudoInputField;
    }

    
}
