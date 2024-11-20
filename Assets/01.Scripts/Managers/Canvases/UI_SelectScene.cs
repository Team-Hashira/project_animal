using UnityEngine;
using UnityEngine.UIElements;

public class UI_SelectScene : UI_Scene
{
    public enum Images
    {
        Image_BackGround
    }

    public enum Buttons
    {
        Button_Select1,
        Button_Select2,
        Button_Select3,
        Button_Select4,
        Button_Select5,
        Button_Select6
    }

    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindImages(typeof(Images));
        BindButtons(typeof(Buttons));

        GetButtons((int)Buttons.Button_Select1).onClick.AddListener(ClickEvent1);
        GetButtons((int)Buttons.Button_Select2).onClick.AddListener(ClickEvent2);
        GetButtons((int)Buttons.Button_Select3).onClick.AddListener(ClickEvent3);
        GetButtons((int)Buttons.Button_Select4).onClick.AddListener(ClickEvent4);
        GetButtons((int)Buttons.Button_Select5).onClick.AddListener(ClickEvent5);
        GetButtons((int)Buttons.Button_Select6).onClick.AddListener(ClickEvent6);

        return true;
    }

    private void ClickEvent1()
    {
        Debug.Log("1");
    }

    private void ClickEvent2()
    {
        Debug.Log("2");
    }

    private void ClickEvent3()
    {
        Debug.Log("3");
    }

    private void ClickEvent4()
    {
        Debug.Log("4");
    }

    private void ClickEvent5()
    {
        Debug.Log("5");
    }

    private void ClickEvent6()
    {
        Debug.Log("6");
    }
}
