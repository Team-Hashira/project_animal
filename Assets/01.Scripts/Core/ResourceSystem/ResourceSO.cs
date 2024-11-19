using UnityEngine;

[CreateAssetMenu(fileName = "ResourceSO", menuName = "SO/Resource/Resource")]
public class ResourceSO : ScriptableObject
{
    public string resourceName;
    public EResourceType resourceType;
    public Sprite sprite;
}
