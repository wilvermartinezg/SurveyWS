using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveyWS.Infrastructure.EntityFramework.Entities
{
    [Table("survey_template")]
    public class SurveyTemplateEf
    {
        [Column("id")] [Key] public long Id { get; set; }

        [Column("name")] public string? Name { get; set; }

        [Column("description")] public string? Description { get; set; }

        [Column("created_at")] public DateTime? createdAt { get; set; } = DateTime.Now;

        [Column("created_by")] public string? createdBy { get; set; } = "ANONIMO";

        [Column("modified_at")] public DateTime? ModifiedAt { get; set; }

        [Column("modified_by")] public string? ModifiedBy { get; set; }

        [Column("active")] public bool Active { get; set; } = true;
    }
}