using BusinessObjects.Location;

namespace BusinessObjects.Order
{
    public interface IOrder
    {
        ILocation? ToLocation { get; set; }
        ILocation? FromLocation { get; set; }

        Dictionary<string, int>? LineItems { get; set; }
    }
}