using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace Kaizen.Test.Helpers
{
    internal class TestAsyncEnumerator<T> : IDbAsyncEnumerator<T>, IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _inner;

        public TestAsyncEnumerator(IEnumerator<T> inner)
        {
            _inner = inner;
        }

        public Task<bool> MoveNextAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(_inner.MoveNext());
        }

        public ValueTask<bool> MoveNextAsync()
        {
            return new(_inner.MoveNext());
        }

        object IDbAsyncEnumerator.Current => Current;

        public T Current => _inner.Current;

        public Task<bool> MoveNext(CancellationToken cancellationToken)
        {
            return MoveNextAsync(cancellationToken);
        }

        public void Dispose()
        {
            _inner.Dispose();
        }

        public ValueTask DisposeAsync()
        {
            _inner.Dispose();
            return default;
        }
    }
}
