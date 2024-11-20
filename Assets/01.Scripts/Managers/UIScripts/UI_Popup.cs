using UnityEngine;

public class UI_Popup : UI_Base
{
    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        //Managers.UI.SetCanvas(gameObject, false);
        return true;
    }
}
