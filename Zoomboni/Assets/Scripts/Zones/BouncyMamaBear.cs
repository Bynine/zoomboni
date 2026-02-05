using UnityEngine;

public class BouncyMamaBear : Bouncy
{

    public Animator animator;
    protected override void OnTrigger()
    {
        animator.Play("Armature|throw");
    }
}
