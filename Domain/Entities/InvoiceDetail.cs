namespace Kaizen.Domain.Entities
{
    public interface IInvoiceDetail<T>
    {
        public int Id { get; set; }
        public T Detail { get; set; }
        public decimal Total { get; set; }
    }
}
