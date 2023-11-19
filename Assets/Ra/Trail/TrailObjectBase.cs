using System;
using Ra.Trail.Mono;

namespace Ra.Trail
{
    public abstract class TrailObject
    {
        public abstract void Initialize(MonoTrail newMonoTrail = null);
        public abstract void OnTrailStarted();
    }
}