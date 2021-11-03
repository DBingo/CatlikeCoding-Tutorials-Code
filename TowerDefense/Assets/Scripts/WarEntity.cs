using UnityEngine;

public abstract class WarEntity : GameBehavior
{
    WarFactory originFactory;

    public WarFactory OriginFactory
    {
        get => originFactory;
        set
        {
            Debug.Assert(originFactory == null, "");
            originFactory = value;
        }
    }

    public void Recycle()
    {
        originFactory.Reclaim(this);
    }
}
