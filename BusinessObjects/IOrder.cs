namespace BusinessObjects
{
    public interface IOrder
    {
        ILocation? Location { get; set; }

        double CalculateTaxes();
    }
}