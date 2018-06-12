using System;

namespace PerformanceModeller.Model
{
    public class CachedItem<T>
    {
        public T Value {
            get
            {
                if (hasValue) return backingValue;
                
                this.Value = getter.Invoke();
                return backingValue;
            }
            set
            {
                this.backingValue = value;
                this.hasValue = true;
            }
        }
        
        public CachedItem(Func<T> getter)
        {
            this.getter = getter;
            this.hasValue = false;
        }

        public void Invalidate()
        {
            this.hasValue = false;
        }

        private Func<T> getter;

        private T backingValue;
        private bool hasValue;
    }
}