using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] private Animator anim;

    private float second = 1;

    public void Chop()
    {
        anim.SetTrigger("Chop");
    }

    public void FlipPan()
    {
        anim.SetTrigger("Flip");
    }

    public void Tenderise()
    {
        anim.SetTrigger("Tenderise");
    }

    public void PullLeaves()
    {
        anim.SetTrigger("PullLeaves");
    }

    public void Salt()
    {
        anim.SetTrigger("Salt");
    }

    public void ThrowKnife()
    {
        anim.SetTrigger("Throw");
    }

    public void ThrowHob()
    {
        anim.SetTrigger("ThrowHob");
    }

    public void PullCustomer()
    {
        anim.SetTrigger("PullCustomer");
    }

    public void Idle()
    {
        anim.SetTrigger("Idle");
    }

    private void Update()
    {
        second -= Time.deltaTime;
        if (second <= 0)
        {
            Idle();
            second = 3;
        }
    }
}
