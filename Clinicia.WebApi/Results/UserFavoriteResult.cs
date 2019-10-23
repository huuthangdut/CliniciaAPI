namespace Clinicia.WebApi.Results
{
    public class UserFavoriteResult
    {
        public string Id { get; set; }

        public FavoriteDoctorResult Doctor { get; set; }
    }

    public class FavoriteDoctorResult
    {
        public string Id { get; set; }

        public string ImageProfile { get; set; }

        public string Name { get; set; }

        public decimal? Rating { get; set; }
    }
}
