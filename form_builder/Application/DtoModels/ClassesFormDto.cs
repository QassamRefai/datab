using form_builder.Application.DtoModels;

namespace form_builder.DataAccess.Models
{
    public class UserDto
    {
    //    public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Role { get; set; } // admin, editor, user
    }

    public class FormDto
    {
     //   public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsActive { get; set; }
    }


    public class FormAppearanceDto
    {
        //  public int Id { get; set; }
        public string StyleName { get; set; }
        public string Dir { get; set; }
        public string Language { get; set; }
        public int FormId { get; set; }
        public string TitleFontSize { get; set; }
        public string TitleFontType { get; set; }
        public string TextFontSize { get; set; }
        public string TextFontType { get; set; }
        public string QuestionFontSize { get; set; }
        public string QuestionFontType { get; set; }
        public string GeneralColor { get; set; }
        public string BackgroundColor { get; set; }
    }

    public class FormGroupDto
    {
        public int FormId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int Contents { get; set; }

        
    }

    public class FormFieldDto
    {
     //   public int Id { get; set; }
        public int GroupId { get; set; }
        public int FieldTypeId { get; set; }
        public int FieldContentId { get; set; }
        public int? DesignId { get; set; }
        public string Label { get; set; }
        public string? Placeholder { get; set; }
        public string? Description { get; set; }
        public bool IsRequired { get; set; }
     //   public List<FieldOptionDto> FieldOptions { get; set; } // Optional
    }


    public class FieldTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } // textLong, textShort, number, date, choice, radio, checkbox
    }


    public class FieldOptionDto
    {
        public int Id { get; set; }
        public string OptionText { get; set; }
    }


    public class FormResponseDto
    {
        // public int Id { get; set; }
        public int FormId { get; set; }
        public int? UserId { get; set; }
        //     public DateTime SubmissionDate { get; set; }

        public ICollection<ResponseDetailDto> ResponseDetails { get; set; }
    }

    public class ResponseDetailDto
    {
        //public int Id { get; set; }
        //   public int FormResponseId { get; set; }
        public int FormFieldId { get; set; }
        public string? ResponseValue { get; set; }

    }
}
