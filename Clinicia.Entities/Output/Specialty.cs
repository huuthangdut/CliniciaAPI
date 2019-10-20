using System;

namespace Clinicia.Dtos.Output
{
    public class Specialty
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public bool IsActive { get; set; }

        public int NumberOfDoctors { get; set; }
    }
}