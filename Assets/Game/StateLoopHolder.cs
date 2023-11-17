using System.Collections;

public class StateLoopHolder : ForceProtectedSingleton<StateLoopHolder>
{
    public static void AddLoop(IEnumerator loop)
    {
        Instance.StartCoroutine(loop);
    }
    
    public static void RemoveLoop(IEnumerator loop)
    {
        Instance.StopCoroutine(loop);
    }
}