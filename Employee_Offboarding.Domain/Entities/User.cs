using System.ComponentModel.DataAnnotations;

namespace Employee_Offboarding.Domain.Entities
{
    public enum UserRole { HR = 1, Auditor = 2 }
    public enum UserStatus
    {
        Pending, 
        Active,  
        Rejected 
    }
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public UserRole Role { get; set; }
        public bool IsActive { get; set; } = true;
        [Required]
        public UserStatus Status { get; set; }
        public Guid? RegistrationApprovalToken { get; set; }        
        public Guid? PasswordResetToken { get; set; }
        public DateTime? PasswordResetTokenExpiry { get; set; }
        public DateTime CreatedAtUtc { get; set; } = DateTime.Now;
        public DateTime? LastLoginAtUtc { get; set; }        
        public bool IsHrFinalizer { get; set; }
    }
}
