using System;
using System.Threading;

namespace Binix.Database
{
    public class Once
    {
        private long _val;

        public void Dispose()
        {
            if (Interlocked.Increment(ref _val) > 0)
                Throw();
        }

        public bool IsDisposed() => Interlocked.Read(ref _val) > 0;
        private void Throw() => throw new ObjectDisposedException(nameof(IRepoTransaction));
        public void ThrowIfDisposed() { if (IsDisposed()) Throw(); }
        private Once() {}
        public static Once Configure() => new();
    }
}