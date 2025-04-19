namespace GymTrackerApi.Models
{
    [Table("applicationuser")]
    public class ApplicationUser
    {
        
        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        public string FirstName { get; set; } = string.Empty;
        [StringLength(255)]
        public string LastName { get; set; } = string.Empty;
        [EmailAddress]
        public string EmailAddress { get; set; } = string.Empty;
        public string HashedPassword { get; set; } = string.Empty;
        // navigationproperty: one user can have many trainings
        public ICollection<Training> Trainings { get; set; } = new List<Training>();
        

    }
}
