using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public string menuName;
    public bool open;

    public void openMenu()
    {
        open = true;
        gameObject.SetActive(true);
    }

    public void closeMenu()
    {
        open = false;
        gameObject.SetActive(false);
    }
}
