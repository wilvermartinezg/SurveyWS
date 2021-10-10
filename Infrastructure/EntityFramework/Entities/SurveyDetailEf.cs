using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveyWS.Infrastructure.EntityFramework.Entities
{
    [Table("survey_detail")]
    public class SurveyDetailEf
    {
        [Column("id")] [Key] public long Id { get; set; }

        [Column("response")] public string Response { get; set; }

        [ForeignKey("survey_id")] public SurveyEf? Survey { get; set; }

        [ForeignKey("survey_template_detail_id")]
        public SurveyTemplateDetailEf? SurveyTemplateDetail { get; set; }
    }
}