namespace Toyota.Shared.Logging
{
    public class CompositeDisposable(IEnumerable<IDisposable> disposables) : IDisposable
    {
        private readonly List<IDisposable> _disposables = [..disposables];

        public void Dispose()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
        }
    }
}
