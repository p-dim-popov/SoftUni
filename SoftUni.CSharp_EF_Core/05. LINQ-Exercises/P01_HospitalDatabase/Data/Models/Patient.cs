namespace P01_HospitalDatabase.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Collections.Generic;

    public class Patient
    {
        public int PatientId { get; set; }
        [MaxLength(50)] public string FirstName { get; set; }
        [MaxLength(50)] public string LastName { get; set; }
        [MaxLength(250)] public string Address { get; set; }
        [Column(TypeName = "varchar(80)")] public string Email { get; set; }
        public bool HasInsurance { get; set; }

        public virtual ICollection<Visitation> Visitations { get; set; }
            = new HashSet<Visitation>();

        public virtual ICollection<Diagnose> Diagnoses { get; set; }
            = new HashSet<Diagnose>();

        public virtual ICollection<PatientMedicament> Prescriptions { get; set; }
            = new HashSet<PatientMedicament>();
    }
}