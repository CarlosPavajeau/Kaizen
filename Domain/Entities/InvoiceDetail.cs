using System;
using System.Collections.Generic;
using System.Text;

namespace Kaizen.Domain.Entities
{
    public interface IInvoiceDetail<T>
    {
        public T Detail { get; set; }
    }
}
