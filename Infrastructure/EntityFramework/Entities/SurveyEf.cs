using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveyWS.Infrastructure.EntityFramework.Entities
{
    [Table("survey")]
    public class SurveyEf
    {
        [Column("id")] [Key] public long Id { get; set; }

        [Column("created_at")] public DateTime? CreatedAt { get; set; } = DateTime.Now;

        [Column("active")] public bool Active { get; set; } = true;

        [ForeignKey("survey_template_id")] public SurveyTemplateEf? SurveyTemplate { get; set; }
    }
}