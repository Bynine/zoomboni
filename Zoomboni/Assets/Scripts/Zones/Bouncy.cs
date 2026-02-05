using UnityEngine;

public class Bouncy : Zone
{

    public GameObject model;
    private Vector3 baseScale;
    private Counter counter = new Counter(2);
    
    public void Start()
    {
        if (model)
        {
            baseScale = model.transform.localScale;
        }
        else
        {
            baseScale = new Vector3(); // will not be used
        }

        counter.End();
    }

    public void Update()
    {
        counter.Update();
        if (model)
        {
            Vector3 scaleToLerpTo;
            if (counter.IsActive())
            {
                Vector3 scale = new Vector3();
                float f = 1.0f - Mathf.Abs((counter.GetCounter() - (float)counter.GetMax() / 2.0f)) / (float)(counter.GetMax());
                f = (Mathf.Pow(f * 2.0f, 2)) / 4.0f;
                scale.x = baseScale.x * (f + .5f);
                scale.y = baseScale.y * (f + .5f);
                scale.z = baseScale.z * (1.5f - f);
                scaleToLerpTo = scale;
            }
            else
            {
                scaleToLerpTo = baseScale;
            }
            model.transform.localScale = Vector3.Lerp(model.transform.localScale, scaleToLerpTo, 15.0f * Time.deltaTime);
        }
    }

    internal override void Enter(Collider collider)
    {
        Player playerOptional = collider.gameObject.GetComponent<Player>();
        if (playerOptional)
        {
            playerOptional.SignalBounce();
            OnTrigger();
        }
    }

    protected virtual void OnTrigger()
    {
        counter.Reset();
    }

    internal override void Exit(Collider collider)
    {
        /**/
    }

}
