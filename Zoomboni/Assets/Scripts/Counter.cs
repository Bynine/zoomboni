using System;
using UnityEngine;


public class Counter{

    private float count = 0;
    private float MAX;
    private Counter me;
    
    public Counter(float max, float init=0) {
        me = this;
        this.MAX = max;
    }

    public void Update() {
        count += Time.deltaTime;
    }

    public bool IsActive() {
        return count < MAX;
    }

    public void Reset() {
        count = 0;
    }
    public void Reset(float max_new)
    {
        MAX = max_new;
        this.Reset();
    }

    internal float GetPercent() {
        return count/MAX;
    }

    internal float GetCounter()
    {
        return count;
    }

    internal float GetMax()
    {
        return MAX;
    }

    internal void End()
    {
        count = MAX;
    }
}
