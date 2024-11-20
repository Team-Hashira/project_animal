using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour 
{
        private int _order = 10;

    private UI_Scene _sceneUI = null;

    private UI_Scene UIScene
    {
        get { return _sceneUI; }
        set { _sceneUI = value; }
    }

    public GameObject UIRoot()
    {
        GameObject root = GameObject.Find("@Root");
        if (root == null)
        {
            root = new GameObject { name = "@Root" };
        }

        return root;
    }

    public void SetCanvas(GameObject gameObject,  bool sort= true, int sortOrder = 0)
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(gameObject);
        if(canvas != null)
        {
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.overrideSorting = true;
        }

        CanvasScaler canvasScaler = Util.GetOrAddComponent<CanvasScaler>(gameObject);
        if(canvasScaler != null)
        {
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.referenceResolution = new Vector2(1080, 1920);
        }
        
    }
}
