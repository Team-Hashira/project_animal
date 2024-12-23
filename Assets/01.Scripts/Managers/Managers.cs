using UnityEngine;

public class Managers : MonoBehaviour
{
    public static bool Initialized { get; set; } = false;

    private static Managers s_instance;
    private static Managers Instance { get { Init(); return s_instance; } }


    //여기에 Manager를 등록해주세요.
    #region Register
    private UIManager _ui = new UIManager();
    private ResourceManager _resourceManager = new ResourceManager();

    public static UIManager UI { get { return Instance?._ui; } }
    public static ResourceManager Resource { get { return Instance?._resourceManager; } }
    #endregion

    public static void Init()
    {
        if (s_instance == null && Initialized == false)
        {
            Initialized = true;

            GameObject gameObject = GameObject.Find("@Managers");
            if (gameObject == null)
            {
                gameObject = new GameObject { name = "@Managers" };
                gameObject.AddComponent<Managers>();
            }
            DontDestroyOnLoad(gameObject);

            s_instance = gameObject.GetComponent<Managers>();
        }
    }
}