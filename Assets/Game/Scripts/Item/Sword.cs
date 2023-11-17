using UnityEngine;

[CreateAssetMenu(fileName = "Sword", menuName = "Sword", order = 0)]
public class Sword : Item
{
    public GameObject prefab;
    public int damage;

    private GameObject instance;
    public override void OnCreate()
    {
        instance = Instantiate(prefab);
    }

    public override void OnUpdate()
    {
        instance.transform.position = Player.position;
    }

    public override void OnDestroy()
    {
        Destroy(instance);
    }
}
