using System.ComponentModel.DataAnnotations;

namespace form_builder.DataAccess.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // admin, editor, user

        public ICollection<FormModel> Forms { get; set; }
        public ICollection<FormResponseModel> FormResponses { get; set; }
    }


    public class FormModel
    {
        public int Id { get; set; }
        public string? Link { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsActive { get; set; }

        public UserModel User { get; set; }
        public FormAppearanceModel FormAppearance { get; set; }
        public ICollection<FormGroupModel> FormGroups { get; set; }
        public ICollection<FormResponseModel> FormResponses { get; set; }
    }

    public class FormAppearanceModel
    {
        public int Id { get; set; }
        public int FormId { get; set; }
        public string StyleName { get; set; }
        public string Language { get; set; }
        public string Dir { get; set; }
        public string TitleFontSize { get; set; }
        public string TitleFontType { get; set; }
        public string TextFontSize { get; set; }
        public string TextFontType { get; set; }
        public string QuestionFontSize { get; set; }
        public string QuestionFontType { get; set; }
        public string GeneralColor { get; set; }
        public string BackgroundColor { get; set; }

        public FormModel Form { get; set; }
    }


    
  public class FormGroupModel
    {
        public int Id { get; set; }
        public int FormId { get; set; }
        public int Contents { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        public FormModel Form { get; set; }
        public ICollection<FormFieldModel> Fields { get; set; }


    }
    public class FormFieldModel
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int FieldTypeId { get; set; }
        public int FieldContentId { get; set; }
        public int? DesignId { get; set; }
        public string Label { get; set; }
        public string? Placeholder { get; set; }
        public string? Description { get; set; }

        
        public bool IsRequired { get; set; }

        public FormGroupModel Group { get; set; }
        public FieldTypeModel FieldType { get; set; }
        public FieldContentModel FieldContent { get; set; }
        public FieldDesignModel FieldDesign { get; set; }
        public ICollection<FieldOptionModel> FieldOptions { get; set; }
        public ICollection<ResponseDetailModel> ResponseDetails { get; set; }
    }

    public class FieldTypeModel
    {
        public int Id { get; set; }
        public string Name { get; set; } // textLong, textShort, number, date, choice, radio, checkbox

        public ICollection<FieldDesignModel> FormDesign { get; set; }
        public ICollection<FormFieldModel> FormFields { get; set; }
    }
    public class FieldContentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<FormFieldModel> FormFields { get; set; }
    }

    public class FieldDesignModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FieldTypeId { get; set; }

        public FieldTypeModel FieldType { get; set; }
        public ICollection<FormFieldModel> FormFields { get; set; }
    }

    public class FieldOptionModel
    {
        public int Id { get; set; }
        public int FormFieldId { get; set; }
        public string OptionText { get; set; }

        public FormFieldModel FormField { get; set; }
    }

    public class FormResponseModel
    {
        public int Id { get; set; }
        public int FormId { get; set; }
        public int? UserId { get; set; }
        public DateTime SubmissionDate { get; set; }

        public FormModel Form { get; set; }
        public UserModel? User { get; set; }
        public ICollection<ResponseDetailModel> ResponseDetails { get; set; }
    }

    public class ResponseDetailModel
    {
        public int Id { get; set; }
        public int FormResponseId { get; set; }
        public int FormFieldId { get; set; }
        public string? ResponseValue { get; set; }

        public FormResponseModel FormResponse { get; set; }
        public FormFieldModel FormField { get; set; }
    }





    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }


}
