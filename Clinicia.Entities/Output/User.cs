using System;

namespace Clinicia.Dtos.Output
{
    public class User
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ImageProfile { get; set; }

        public bool? Gender { get; set; }

        public bool IsActive { get; set; }
    }
}
