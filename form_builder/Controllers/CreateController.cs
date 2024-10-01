using form_builder.Application.DtoModels;
using form_builder.DataAccess;
using form_builder.DataAccess.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.VisualBasic;
using System.Text;
using Microsoft.VisualBasic.FileIO;
using System.IO;

namespace form_builder.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CreateController : Controller
    {
        private readonly Database _context;
   //     private FormView? form;

        public CreateController(Database context)
        {
            _context = context;
        }


        [HttpPost("Form")]
        public async Task<IActionResult> AddNewForm(NewForm newForm)
        {
            var userExists = await _context.Users.AnyAsync(user => user.Id == newForm.UserId);
            
            if (!userExists)
            {
                return Conflict("User not found.");
            }

            if (newForm == null)
            {
                return BadRequest("Form data is null.");
            }

            if (string.IsNullOrWhiteSpace(newForm.Title))
            {
                return BadRequest("Form title is required.");
            }

            if (newForm.FormGroups == null || !newForm.FormGroups.Any())
            {
                return BadRequest("At least one group must be added.");
            }





            foreach (var group in newForm.FormGroups)
            {
                if (group.FormFields == null || !group.FormFields.Any())
                {
                    return BadRequest("Each group must contain at least one field.");
                }
                
                    var duplicateLabels = group.FormFields
                    .GroupBy(f => f.Label)
                    .Where(g => g.Count() > 1)
                    .Select(g => g.Key)
                    .ToList();

                if (duplicateLabels.Any())
                {
                    return Conflict("Duplicate labels found in group: " + string.Join(", ", duplicateLabels));
                }

                foreach (var field in group.FormFields)
                {
                    if (string.IsNullOrWhiteSpace(field.Label))
                    {
                        return BadRequest("Field label is required.");
                    }
                    if (field.FieldContentId == null || field.FieldContentId == 0)
                    {
                        return Conflict("Field content not found: " + field.FieldContentId);
                    }

                        // تحقق من وجود نوع الحقل
                        if (!await _context.FieldTypes.AnyAsync(ft => ft.Id == field.FieldTypeId))
                    {
                        return Conflict("Field type not found.");
                    }

                    // تحقق من وجود FieldContent
                    if (field.FieldContentId != null && field.FieldContentId != 0)
                    {
                        var fieldContentExists = await _context.FieldContent
                            .AnyAsync(fc => fc.Id == field.FieldContentId);

                        if (!fieldContentExists)
                        {
                            return Conflict("Field content not found.");
                        }
                    }
                  

                    // تحقق من وجود DesignId في FieldDesign
                    if (field.DesignId != null && !await _context.FieldDesign.AnyAsync(fd => fd.Id == field.DesignId && fd.FieldTypeId == field.FieldTypeId))
                    {
                        return Conflict("Field design not found.");
                    }





              
            


                    if (field.FieldContentId == 6)
                    {
                        if (field.FieldTypeId != 16 && field.FieldTypeId != 17 && field.FieldTypeId != 24)
                        {
                            return Conflict("Multiple options not selected correctly: " + field.Label);
                        }

                        if (field.FieldTypeId == 16 && (field.FieldOptions == null || field.FieldOptions.Count < 1))
                        {
                            return Conflict("At least one options must be added for field: " + field.Label);
                        }
                        else if ((field.FieldTypeId != 16) && (field.FieldOptions == null || field.FieldOptions.Count < 2))
                        {
                            return Conflict("At least two options must be added for field: " + field.Label);
                        }

                        var duplicateOptions = field.FieldOptions
                            .Select(o => o.OptionText)
                            .GroupBy(o => o)
                            .Where(g => g.Count() > 1)
                            .Select(g => g.Key)
                            .ToList();

                        if (duplicateOptions.Any())
                        {
                            return Conflict("Duplicate options found: " + string.Join(", ", duplicateOptions));
                        }

                        foreach (var option in field.FieldOptions)
                        {
                            if (string.IsNullOrWhiteSpace(option.OptionText))
                            {
                                return BadRequest("Option text is required.");
                            }
                        }
                    }
                }
            }









            var form = new FormModel
            {
                Link = GenerateCodeFromId(1),
                UserId = newForm.UserId,
                Title = newForm.Title,
                Description = newForm.Description,
                IsAvailable = true,
                IsActive = newForm.IsActive
            };

            _context.Forms.Add(form);
            await _context.SaveChangesAsync();

            form.Link = GenerateCodeFromId(form.Id);
            _context.Forms.Update(form);
            await _context.SaveChangesAsync();


            var formAppearance = new FormAppearanceModel
            {
                FormId = form.Id,
                StyleName = newForm.FormAppearance.StyleName,
                Dir = newForm.FormAppearance.Dir,
                Language = newForm.FormAppearance.Language,
                TitleFontSize = newForm.FormAppearance.TitleFontSize,
                TitleFontType = newForm.FormAppearance.TitleFontType,
                TextFontSize = newForm.FormAppearance.TextFontSize,
                TextFontType = newForm.FormAppearance.TextFontType,
                QuestionFontSize = newForm.FormAppearance.QuestionFontSize,
                QuestionFontType = newForm.FormAppearance.QuestionFontType,
                GeneralColor = newForm.FormAppearance.GeneralColor,
                BackgroundColor = newForm.FormAppearance.BackgroundColor
            };

            _context.FormAppearances.Add(formAppearance);
            await _context.SaveChangesAsync();

            var listGroups = new List<FormGroupModel>();

            foreach (var group in newForm.FormGroups)
            {
                var formGroup = new FormGroupModel
                {
                    FormId = form.Id,
                    Title = group.Title,
                    Description = group.Description,
                    Contents = group.Contents,
                };

                _context.FormGroups.Add(formGroup);
                await _context.SaveChangesAsync();

                var listFields = new List<FormFieldModel>();

                foreach (var field in group.FormFields)
                {
                    var formField = new FormFieldModel
                    {
                        GroupId = formGroup.Id,
                        Label = field.Label,
                        Placeholder = field.Placeholder,
                        Description = field.Description,
                        IsRequired = field.IsRequired,
                        DesignId = field.DesignId,
                        FieldTypeId = field.FieldTypeId,
                        FieldContentId = field.FieldContentId // تأكد من تعيين FieldContentId
                    };

                    _context.GroupFields.Add(formField);
                    listFields.Add(formField);
                }

                await _context.SaveChangesAsync();

                foreach (var field in group.FormFields)
                {
                    if (field.FieldContentId != null && field.FieldContentId == 6 && field.FieldOptions != null)
                    {
                        foreach (var option in field.FieldOptions)
                        {
                            var fieldOption = new FieldOptionModel
                            {
                                FormFieldId = listFields.First(f => f.Label == field.Label).Id,
                                OptionText = option.OptionText
                            };

                            _context.FieldOptions.Add(fieldOption);
                        }
                    }
                }
                await _context.SaveChangesAsync();

                formGroup.Fields = listFields;
                listGroups.Add(formGroup);
            }

            form.FormAppearance = formAppearance;
            form.FormGroups = listGroups;

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            var jsonString = JsonSerializer.Serialize(form, options);

            return Ok(jsonString);
        }




        
        [HttpPost("Response")]
        public async Task<IActionResult> AddNewResponse(FormResponseDto response)
        {

            if (response == null)
            {
                return BadRequest("Form data is null.");
            }

            int? userId = response.UserId;
            if (userId != null)
            {
                var userExists = await _context.Users.AnyAsync(user => user.Id == userId);
                if (!userExists)
                {
                    return Conflict("User not found.");
                }
            }

            var form = await _context.Forms.Include(f => f.FormGroups)
                                           .ThenInclude(g => g.Fields)
                                           .ThenInclude(ff => ff.FieldOptions)
                                           .FirstOrDefaultAsync(f => f.Id == response.FormId);
            if (form == null)
            {
                return Conflict("Form not found.");
            }

            if (response.ResponseDetails == null || !response.ResponseDetails.Any())
            {
                return Conflict("Field not found.");
            }
            var fieldTypeIds = new HashSet<int> { 16, 17, 24 };

            foreach (var resField in response.ResponseDetails)
            {
                if (resField.FormFieldId > 0)
                {
                    var formField = form.FormGroups?
                        .SelectMany(g => g.Fields)
                        .FirstOrDefault(field => field.Id == resField.FormFieldId);

                    if (formField != null)
                    {
                        if (formField.IsRequired && string.IsNullOrEmpty(resField.ResponseValue))
                        {
                            return BadRequest($"يجب الإجابة عن الأسئلة المطلوبة.");
                        }

                        if (fieldTypeIds.Contains(formField.FieldTypeId) &&
                            formField.FieldOptions != null &&
                            formField.FieldOptions.Any() &&
                            resField.ResponseValue != null)
                        {
                            bool exists = formField.FieldOptions.Any(option => option.OptionText == resField.ResponseValue);
                            if (!exists)
                            {
                                return BadRequest($"القيمة '{resField.ResponseValue}' غير موجودة في القائمة.");
                            }
                        }
                    }
                }
            }







            var formResponse = new FormResponseModel
            {
                FormId = form.Id,
                UserId = response.UserId,
                SubmissionDate = DateTime.Now,
                ResponseDetails = new List<ResponseDetailModel>() 
            };

            foreach (var responseDetail in response.ResponseDetails)
            {
                if(responseDetail.ResponseValue != null) {
                var rd = new ResponseDetailModel
                {
                    ResponseValue = responseDetail.ResponseValue,
                    FormFieldId = responseDetail.FormFieldId,
                    FormResponse = formResponse 
                };

                formResponse.ResponseDetails.Add(rd);
                }
            }

            _context.FormResponses.Add(formResponse);

            await _context.SaveChangesAsync();








            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            var jsonString = JsonSerializer.Serialize(formResponse, options);

            return Ok(jsonString);


        }



        public static string GenerateCodeFromId(int id)
        {
            string idString = id.ToString();

            byte[] idBytes = Encoding.UTF8.GetBytes(idString);

            string base64String = Convert.ToBase64String(idBytes);

            base64String = base64String.Replace("=", "");  
            base64String = base64String.Replace("+", "-");
            base64String = base64String.Replace("/", "_"); 
            if (base64String.Length < 10)
            {
                const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_";
                Random random = new Random();
                while (base64String.Length < 10)
                {
                    base64String += characters[random.Next(characters.Length)];
                }
            }

           
            return base64String;
        }



        private FormView? Form(int id)
        {
            var form = _context.Forms.FirstOrDefault(fa => fa.Id == id);
            if (form == null)
            {
                return null;
            }

            var formApp = _context.FormAppearances.FirstOrDefault(fa => fa.FormId == form.Id);
            var formAppearance = new FormAppearanceView();
            if (formApp != null)
            {
                formAppearance.Id = formApp.Id;
                formAppearance.StyleName = formApp.StyleName;
                formAppearance.Dir = formApp.Dir;
                formAppearance.Language = formApp.Language;
                formAppearance.TitleFontSize = formApp.TitleFontSize;
                formAppearance.TitleFontType = formApp.TitleFontType;
                formAppearance.TextFontSize = formApp.TextFontSize;
                formAppearance.TextFontType = formApp.TextFontType;
                formAppearance.QuestionFontSize = formApp.QuestionFontSize;
                formAppearance.QuestionFontType = formApp.QuestionFontType;
                formAppearance.GeneralColor = formApp.GeneralColor;
                formAppearance.BackgroundColor = formApp.BackgroundColor;
            }

            var user = _context.Users.FirstOrDefault(u => u.Id == form.UserId);
            string createdBy = "unknown";
            if (user != null)
            {
                createdBy = user.FirstName + " " + user.LastName;
            }

            var formGroups = _context.FormGroups
                .Where(g => g.FormId == form.Id)
                .Select(g => new
                {
                    Group = g,
                    Fields = _context.GroupFields
                        .Join(_context.FieldTypes,
                            gf => gf.FieldTypeId,
                            ft => ft.Id,
                            (gf, ft) => new { gf, ft })
                        .Where(res => res.gf.GroupId == g.Id)
                        .ToList()
                })
                .ToList();

            List<FormGroupView> formGroupsView = new List<FormGroupView>();

            foreach (var group in formGroups)
            {
                var formGroupView = new FormGroupView
                {
                    Id = group.Group.Id,
                    Title = group.Group.Title,
                    Description = group.Group.Description,
                    Contents = group.Group.Contents,
                    FormFields = group.Fields.Select(field => new FormFieldView
                    {
                        Id = field.gf.Id,
                        FieldTypeId = field.gf.FieldTypeId,
                        DesignId = field.gf.DesignId,
                        FieldContentId = field.gf.FieldContentId,
                        FieldType = field.ft.Name,
                        Label = field.gf.Label,
                        Placeholder = field.gf.Placeholder,
                        Description = field.gf.Description ?? null,
                        IsRequired = field.gf.IsRequired,
                        FieldOptions = field.gf.FieldContentId == 6
                           ? _context.FieldOptions
                               .Where(fo => fo.FormFieldId == field.gf.Id)
                               .Select(fo => new FieldOptionDto
                               {
                                   Id = fo.Id,
                                   OptionText = fo.OptionText
                               })
                               .ToList()
                           : null
                    }).ToList()
                };

                formGroupsView.Add(formGroupView);
            }

            var formView = new FormView
            {
                Id = form.Id,
                UserId = form.UserId,
                Link = form.Link,
                CreatedBy = createdBy,
                Title = form.Title,
                Description = form.Description ?? null,
                IsActive = form.IsActive,
                IsAvailable = form.IsAvailable,
                FormAppearance = formAppearance,
                FormGroups = formGroupsView
            };

            return formView;
        }






    }
}
