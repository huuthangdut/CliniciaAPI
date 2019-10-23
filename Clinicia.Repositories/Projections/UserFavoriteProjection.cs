using System;

namespace Clinicia.Repositories.Projections
{
    public class UserFavoriteProjection
    {
        public Guid Id { get; set; }

        public FavoriteDoctorProjection Doctor { get; set; }
    }

    public class FavoriteDoctorProjection
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ImageProfile { get; set; }

        public decimal? Rating { get; set; }
    }
}
