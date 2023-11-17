using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTrigger : MonoBehaviour
{
    public Player player;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Next")) player.OnStageNextTrigger(other);
        else if(other.CompareTag("Back")) player.OnStageBackTrigger(other);
    }
}
