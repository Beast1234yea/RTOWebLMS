using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RTOWebLMS.Enums;

namespace RTOWebLMS.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? PasswordHash { get; set; }

        public UserRole Role { get; set; } = UserRole.STUDENT;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // RTO/AVETMISS Compliance Fields
        [MaxLength(100)]
        public string? FirstName { get; set; }

        [MaxLength(100)]
        public string? LastName { get; set; }

        [MaxLength(100)]
        public string? MiddleName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [MaxLength(1)]
        public string? Gender { get; set; }

        [MaxLength(4)]
        public string? IndigenousStatus { get; set; }

        [MaxLength(4)]
        public string? LanguageSpokenAtHome { get; set; }

        [MaxLength(4)]
        public string? ProficiencyInSpokenEnglish { get; set; }

        [MaxLength(2)]
        public string? HighestSchoolLevel { get; set; }

        [MaxLength(3)]
        public string? YearCompletedSchool { get; set; }

        [MaxLength(1)]
        public string? LaborForceStatus { get; set; }

        [MaxLength(4)]
        public string? CountryOfBirth { get; set; }

        public bool? DisabilityFlag { get; set; }

        [MaxLength(2)]
        public string? PriorEducationAchievement { get; set; }

        [MaxLength(1)]
        public string? AtSchoolFlag { get; set; }

        [MaxLength(255)]
        public string? StreetAddress { get; set; }

        [MaxLength(100)]
        public string? Suburb { get; set; }

        [MaxLength(4)]
        public string? Postcode { get; set; }

        [MaxLength(3)]
        public string? State { get; set; }

        [MaxLength(50)]
        public string? Phone { get; set; }

        [MaxLength(50)]
        public string? Mobile { get; set; }

        [MaxLength(100)]
        public string? UniqueStudentIdentifier { get; set; }

        [MaxLength(20)]
        public string? StudentNumber { get; set; }

        // Navigation Properties
        public virtual ICollection<Course> InstructorCourses { get; set; } = new List<Course>();
        public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public virtual ICollection<Certificate> Certificates { get; set; } = new List<Certificate>();
        public virtual ICollection<Assessment> Assessments { get; set; } = new List<Assessment>();
        public virtual ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();
    }
}
