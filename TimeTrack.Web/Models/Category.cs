using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace TimeTrack.Web.Models
{
    public class Category
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [MaxLength(256)]
        [Required]
        public string Name { get; set; }
        [MaxLength(1024)]
        public string? Description { get; set; }
        public float? BillableRate { get; set; }
        public Guid? ParentId { get; set; }
        [ForeignKey(nameof(ParentId))]
        public Category? Parent { get; set; }

        [MaxLength(256)]
        [Required]
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; }
    }
}