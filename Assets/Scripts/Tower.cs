using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    public int selected;
    Dropdown DropDown;
    // Start is called before the first frame update
    void Start()
    {
        DropDown = GetComponent<Dropdown>();
        DropDown.onValueChanged.AddListener(delegate { DropdownValueChanged(DropDown); });
    }
    void DropdownValueChanged(Dropdown change)
    {
        selected = change.value;
    }
}
