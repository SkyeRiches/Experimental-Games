using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskFailManager : MonoBehaviour
{
    public void TriggerPenalty(string a_step)
    {
        gameObject.GetComponent<ScoreSystem>().CasualtyNumber++;

        switch (a_step)
        {
            case "PeelLeaves":
                PullCustomer();
                break;

            case "Chop":
                ThrowKnife();
                break;

            case "Tenderise":
                ThrowKnife();
                break;

            case "Cook":
                ThrowOven();
                break;

            default:
                break;
        }
    }

    private void ThrowKnife()
    {
        gameObject.GetComponent<AnimationManager>().ThrowKnife();
    }

    private void ThrowOven()
    {
        gameObject.GetComponent<AnimationManager>().ThrowHob();
    }

    private void PullCustomer()
    {
        gameObject.GetComponent<AnimationManager>().PullCustomer();
    }
}
