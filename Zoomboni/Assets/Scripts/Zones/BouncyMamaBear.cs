using UnityEngine;

public class BouncyMamaBear : Bouncy
{

    public Animator animator;
    public AudioSource audio;
    public float animationSpeed;
    public Timer animationDelay;



    public void Update()
    {
        if (!animationDelay.IsActive()){
            animator.speed = 1;
        }
    }

    protected override void OnTrigger()
    {
        animator.speed = animationSpeed;
        animator.Play("Armature|throw");
        audio.Play();
        animationDelay.Reset();
    }
    
    

}
