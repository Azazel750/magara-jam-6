using UnityEngine;


public abstract class Item : ScriptableObject
{
    public string name;
    public string description;

    public abstract void OnCreate();
    public abstract void OnUpdate();
    public abstract void OnDestroy();
}