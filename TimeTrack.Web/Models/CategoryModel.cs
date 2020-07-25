using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeTrack.Web.Models
{
    public class CategoryModel {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public float? BillableRate { get; set; }
        public Guid? ParentId { get; set; }
        [ForeignKey(nameof(ParentId))]
        public CategoryModel? Parent { get; set; }
    }
}