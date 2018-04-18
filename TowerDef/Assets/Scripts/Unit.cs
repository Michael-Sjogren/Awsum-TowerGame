

using UnityEngine;

public abstract class Unit : MonoBehaviour
{   
    public bool isFocused = false;
    void OnMouseDown()
    {
        bool preFocused = isFocused;
        isFocused = !isFocused;
        if( isFocused && !preFocused )
        {
            SetUnitSelected();
        }
    }

    public void SetUnitSelected()
    {
        UnitSelectionSystem.instance.SetFocus(this.gameObject);
    }
   
}