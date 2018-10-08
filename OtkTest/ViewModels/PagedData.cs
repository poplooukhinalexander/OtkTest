using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace OtkTest.ViewModels
{
    public class PagedData<T> where T:class
    {
        public IEnumerable<T> Data { get; }

        public int Total { get; }

        public PagedData(IEnumerable<T> data, int total)
        {
            Contract.Requires(data != null);

            Data = data;
            Total = total;
        }
    }
}
