namespace CoreSchool.Entities
{
    public interface IPlace
    {
        string Address { get; set; }

        void CleanPlace();

    }
}
