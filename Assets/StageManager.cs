using DG.Tweening;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    private int index = 0;
    public void SetStage(int stageNumber)
    {
        /*
        var next = stageNumber > index;
        if (stageNumber >= transform.childCount) stageNumber = transform.childCount - 1;
        if (stageNumber < 0) stageNumber = 0;
        index = stageNumber;
        GameCamera.Instance.Move(transform.GetChild(stageNumber).position, 1);
        var pos = Player.Instance.transform.position;
        var n = (next ? GameCamera.Instance.nextSpawn : GameCamera.Instance.backSpawn).position;
        pos.z = n.z;
        Player.Instance.transform.position = pos;*/
    }


    public void Next()
    {
        SetStage(index + 1);
    }
    
    public void Back()
    {
        SetStage(index - 1);
    }
}
