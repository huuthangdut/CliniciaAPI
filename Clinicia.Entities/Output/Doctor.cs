namespace Clinicia.Dtos.Output
{
    public class Doctor : User
    {
        public string MedicalSchool { get; set; }

        public string Awards { get; set; }

        public int? YearExperience { get; set; }

        public Location MyProperty { get; set; }
    }
}
