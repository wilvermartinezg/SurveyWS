using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveyWS.Infrastructure.EntityFramework.Entities
{
    [Table("survey_template_detail")]
    public class SurveyTemplateDetailEf
    {
        [Column("id")] [Key] public long Id { get; set; }

        [Column("field_name")] public string? FieldName { get; set; }

        [Column("field_description")] public string? FieldDescription { get; set; }

        [Column("field_type")] public string? FieldType { get; set; }

        [Column("is_required")] public bool? IsRequired { get; set; }

        [Column("active")] public bool Active { get; set; } = true;

        [ForeignKey("survey_template_id")] public SurveyTemplateEf? SurveyTemplate { get; set; }
    }
}