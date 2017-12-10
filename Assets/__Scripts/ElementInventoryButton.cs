using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementInventoryButton : MonoBehaviour {

    public ElementType type;

    void Awake()
    {
        char c = gameObject.name[0];
        string s = c.ToString();
        int typeNum = int.Parse(s);

        type = (ElementType)typeNum;
    }

    void OnMouseUpAsButton()
    {
        //Mage.S.SelectElement(type);
    }
}
