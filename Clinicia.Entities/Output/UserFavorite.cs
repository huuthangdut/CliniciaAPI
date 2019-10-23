using System;

namespace Clinicia.Dtos.Output
{
    public class UserFavorite
    {
        public Guid Id { get; set; }

        public FavoriteDoctor Doctor { get; set; }
    }

    public class FavoriteDoctor
    {
        public Guid Id { get; set; }

        public string ImageProfile { get; set; }

        public string Name { get; set; }

        public decimal? Rating { get; set; }
    }
}