namespace BusinessObjects.Location
{
    public abstract class BaseLocation : ILocation
    {

        public BaseLocation()
        {

        }

        public string GetCountry { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}