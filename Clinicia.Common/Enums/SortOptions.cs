namespace Clinicia.Common.Enums
{
    public class SortOptions<TField>
    {
        public TField SortByField { get; set; }

        public SortOrder SortOrder { get; set; }
    }

    public enum SortDoctorField
    {
        Price,
        StarRating,
        Distance
    }
}
