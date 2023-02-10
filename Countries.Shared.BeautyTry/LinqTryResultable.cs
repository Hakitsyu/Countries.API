using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Shared.BeautyTry
{
    public class LinqTryResultable<T> : ILinqTryResultableCatch<T>, ILinqTryResultableHandler<T>
    {
        private Func<T> _try;
        private Func<T> _catch;

        private LinqTryResultable([NotNull] Func<T> func)
            => _try = func;

        public static ILinqTryResultableCatch<T> Try<T>([NotNull] Func<T> func)
            => new LinqTryResultable<T>(func);

        public ILinqTryResultableHandler<T> Catch(Func<T> func)
        {
            _catch = func;
            return this;
        }

        public T Handle()
        {
            T value;
            try
            {
                value = _try();
            } catch
            {
                value = _catch();
            }

            return value;
        }
    }

    public interface ILinqTryResultableCatch<T>
    {
        public ILinqTryResultableHandler<T> Catch(Func<T> func);
    }

    public interface ILinqTryResultableHandler<T>
    {
        public T Handle();
    }
}
