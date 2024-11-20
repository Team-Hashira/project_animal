using UnityEngine;

public class UI_PauseScene : UI_Scene
{
    public enum Images
    {
        Image_BackGround
    }

    public enum Sliders
    {
        Slider_Sound
    }

    public enum Toggles
    {
        Toggle_Test
    }

    protected override bool Init()
    {
        if(base.Init() == false)
            return false;

        BindImages(typeof(Images));
        BindSliders(typeof(Sliders));

        GetImages((int)Images.Image_BackGround).transform.position = new Vector3(0, 0, 0);
        GetSlider((int)Sliders.Slider_Sound).value = 0.5f;

        return true;
    }
}
