
using UnityEngine;

public abstract class GUIPanel : MonoBehaviour 
{
    public UnitSelectionSystem selectionSystem;

    void Start()
    {
        
    }
    public virtual void HidePanel()
    {
        this.gameObject.SetActive(false);
    }

    public virtual void ShowPanel()
    {
        this.gameObject.SetActive(true);
    }
}