using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskFailManager : MonoBehaviour
{
    [SerializeField] private Animator anim;

    public void TriggerPenalty(string a_step)
    {
        gameObject.GetComponent<ScoreSystem>().CasualtyNumber++;

        switch (a_step)
        {
            case "PeelLeaves":
                break;

            case "Chop":
                break;

            case "Tenderise":
                break;

            case "Salt":
                break;

            case "Cook":
                break;

            default:
                break;
        }
    }

    private void ThrowKnife()
    {

    }

    private void ThrowOven()
    {

    }
}
