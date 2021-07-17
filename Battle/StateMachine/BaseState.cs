public class BaseState
{
    protected float timer;
    protected float threshold;
    protected int time = 0;

    public virtual void Start()
    {
    }

    public virtual void Update()
    {
    }

    public virtual void Finish()
    {
    }

    public virtual void WaitForAnimationDone()
    {
    }

    public virtual void DoActionEffect()
    {
    }
}