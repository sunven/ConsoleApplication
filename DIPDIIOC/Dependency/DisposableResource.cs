using System;

namespace DIPDIIOC.Dependency
{
    public abstract class DisposableResource
    {
        ~DisposableResource()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }
    }
}