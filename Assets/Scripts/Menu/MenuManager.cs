using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    [SerializeField] Menu[] menus;

    
    private void Awake()
    {
        Instance = this;
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

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Menu" && Input.GetKeyDown(KeyCode.Escape))
            openMenu("MainMenu");
    }

    private void Start()
    {
        openMenu("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }

    
}
