using UnityEngine;

public class UI_MainScene : UI_Scene
{
    public enum Images
    {
        Image_Test
    }

    public enum Buttons
    {
        Button_Test
    }

    protected override bool Init()
    {
        if(base.Init() == false)
            return false;

        //BindImages(typeof(Images));
        BindButtons(typeof(Buttons));
        
        //GetImages((int)Images.Image_Test);
        GetButtons((int)Buttons.Button_Test).onClick.AddListener(Test);

        return true;
    }

    private void Test()
    {
        
    }
}
