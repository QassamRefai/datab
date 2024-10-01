using form_builder.DataAccess.Models;
using System.Text.Json.Serialization;

namespace form_builder.Application.DtoModels
{
    public class NewForm
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }

        public NewFormAppearance FormAppearance { get; set; }
        public List<NewFormGroup> FormGroups { get; set; } // تحديث هنا لتشمل المجموعات
    }

    public class NewFormGroup
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int Contents { get; set; }
        public List<NewFormField> FormFields { get; set; } // تضمين الحقول ضمن المجموعة
    }

    public class NewFormField
    {
        //   public int Id { get; set; }
        //    public int FormId { get; set; }
        public int FieldTypeId { get; set; }
        public int FieldContentId { get; set; }
        public int? DesignId { get; set; }
        public string Label { get; set; }
        public string? Placeholder { get; set; }
        public string? Description { get; set; }
        public bool IsRequired { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<NewFieldOption>? FieldOptions { get; set; }

    }

    public class NewFieldOption
    {
        public string? OptionText { get; set; }
    }

    public class NewFormAppearance
    {
        public string StyleName { get; set; }
        public string Dir { get; set; }
        public string Language { get; set; }
        public string TitleFontSize { get; set; }
        public string TitleFontType { get; set; }
        public string TextFontSize { get; set; }
        public string TextFontType { get; set; }
        public string QuestionFontSize { get; set; }
        public string QuestionFontType { get; set; }
        public string GeneralColor { get; set; }
        public string BackgroundColor { get; set; }
    }
}
