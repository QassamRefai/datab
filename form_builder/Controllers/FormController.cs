using form_builder.Application.DtoModels;
using form_builder.DataAccess;
using form_builder.DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.FileIO;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace form_builder.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FormController : Controller
    {
        private readonly Database database;
        private readonly Database _context;

        public FormController(Database context)
        {
            database = context;
            _context = context;
        }


    //    [Authorize]
        [HttpGet("ViewUsers")]
        public IActionResult ViewUsers()
        {
            var userExists = _context.Users.ToList();
            if (userExists.Count<1)
            {
                return Conflict("No users found.");
            }
            return Ok(userExists);
        }







        // اضافة مستخدم
        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] UserDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest("User data is null.");
            }

            // تحقق من صحة البيانات إذا لزم الأمر، مثل التحقق من البريد الإلكتروني الفريد
            if (string.IsNullOrWhiteSpace(userDto.Email))
            {
                return BadRequest("Email is required.");
            }

            bool userExists = await _context.Users
              .AnyAsync(u => u.Username == userDto.Username);

            if (userExists)
            {
                return Conflict("Username already exists.");
            }
            bool emailExists = await _context.Users
                .AnyAsync(u => u.Email == userDto.Email);

            if (emailExists)
            {
                return Conflict("email already exists.");
            }


            // إنشاء كائن UserModel من UserDto
            var user = new UserModel
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Username = userDto.Username,
                Email = userDto.Email,
                Password = userDto.Password, // تأكد من تشفير كلمة المرور في تطبيق حقيقي
                Role = userDto.Role
            };

            // إضافة المستخدم إلى قاعدة البيانات
            _context.Users.Add(user);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // تسجيل الخطأ والرد برسالة خطأ
                // يمكنك استخدام مكتبة تسجيل مثل Serilog أو NLog هنا
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred while saving user data.");
            }

            // إرجاع المستخدم المضاف مع رمز الحالة 201 Created
            return CreatedAtAction(nameof(AddUser), new { id = user.Id }, user);
        }


        // تعديل مستخدم
        [HttpPut("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest("User data is null.");
            }

            // البحث عن المستخدم المطلوب تعديله
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // تحقق من صحة البيانات إذا لزم الأمر، مثل التحقق من البريد الإلكتروني الفريد
            if (string.IsNullOrWhiteSpace(userDto.Email))
            {
                return BadRequest("Email is required.");
            }

            bool emailExists = await _context.Users
                .AnyAsync(u => u.Email == userDto.Email && u.Id != id);

            if (emailExists)
            {
                return Conflict("Email already exists.");
            }

            bool usernameExists = await _context.Users
                .AnyAsync(u => u.Username == userDto.Username && u.Id != id);

            if (usernameExists)
            {
                return Conflict("Username already exists.");
            }

            // تحديث بيانات المستخدم
            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.Username = userDto.Username;
            user.Email = userDto.Email;
            user.Password = userDto.Password; // تأكد من تشفير كلمة المرور في تطبيق حقيقي
            user.Role = userDto.Role;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // تسجيل الخطأ والرد برسالة خطأ
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred while updating user data.");
            }

            // إرجاع المستخدم المعدل مع رمز الحالة 200 OK
            return Ok(user);
        }

   



        // اضافة نموذج جديد
        [HttpPost("AddForm")]
        public async Task<IActionResult> AddForm([FromBody] FormDto formDto)
        {
            if (formDto == null)
            {
                return BadRequest("Form data is null.");
            }
            var userExists = _context.Users.Any(user => user.Id == formDto.UserId);

            if (!userExists)
            {
                return Conflict("users not found.");
            }
            // تحقق من عدم وجود نموذج بنفس العنوان بالفعل في قاعدة البيانات
            //bool formExists = await _context.Forms
            //    .AnyAsync(f => f.Title == formDto.Title);

            //if (formExists)
            //{
            //    return Conflict("A form with the same title already exists.");
            //}

            // تحقق من صحة البيانات
            if (string.IsNullOrWhiteSpace(formDto.Title))
            {
                return BadRequest("Form title is required.");
            }

            // إنشاء كائن FormModel من FormDto
            var form = new FormModel
            {
                UserId = formDto.UserId,
                Title = formDto.Title,
                Description = formDto.Description,
                IsAvailable = formDto.IsAvailable,
                IsActive = formDto.IsActive
            };

            // إضافة النموذج إلى قاعدة البيانات
            _context.Forms.Add(form);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // تسجيل الخطأ والرد برسالة خطأ
                // يمكنك استخدام مكتبة تسجيل مثل Serilog أو NLog هنا
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred while saving form data.");
            }

            // إرجاع النموذج المضاف مع رمز الحالة 201 Created
            return CreatedAtAction(nameof(AddForm), new { id = form.Id }, form);
        }



        // تعديل نموذج موجود
        [HttpPut("UpdateForm/{id}")]
        public async Task<IActionResult> UpdateForm(int id, [FromBody] FormDto formDto)
        {
            if (formDto == null)
            {
                return BadRequest("Form data is null.");
            }

            // البحث عن النموذج المطلوب تعديله
            var form = await _context.Forms.FindAsync(id);
            if (form == null)
            {
                return NotFound("Form not found.");
            }

            // التحقق من وجود المستخدم
            var userExists = await _context.Users.AnyAsync(user => user.Id == formDto.UserId);
            if (!userExists)
            {
                return Conflict("User not found.");
            }

            // التحقق من صحة البيانات
            if (string.IsNullOrWhiteSpace(formDto.Title))
            {
                return BadRequest("Form title is required.");
            }

            // التحقق من عدم وجود نموذج آخر بنفس العنوان
            bool formExists = await _context.Forms
                .AnyAsync(f => f.Title == formDto.Title && f.Id != id);

            if (formExists)
            {
                return Conflict("A form with the same title already exists.");
            }

            // تحديث بيانات النموذج
            form.UserId = formDto.UserId;
            form.Title = formDto.Title;
            form.Description = formDto.Description;
            form.IsAvailable = formDto.IsAvailable;
            form.IsActive = formDto.IsActive;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // تسجيل الخطأ والرد برسالة خطأ
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred while updating form data.");
            }

            // إرجاع النموذج المعدل مع رمز الحالة 200 OK
            return Ok(form);
        }




        //تخصيص المظهر
        [HttpPost("AddFormAppearance")]
        public async Task<IActionResult> AddFormAppearance([FromBody] FormAppearanceDto formAppearanceDto)
        {
            if (formAppearanceDto == null)
            {
                return BadRequest("FormAppearance data is null.");
            }
            // تحقق من صحة البيانات
            if (formAppearanceDto.FormId < 1)
            {
                return BadRequest("FormId is required and must be greater than 0.");
            }

            var form = _context.Forms.Any(form => form.Id == formAppearanceDto.FormId);
            if (!form)
            {
                return BadRequest("Form not found.");
            }


            if (string.IsNullOrWhiteSpace(formAppearanceDto.TitleFontSize) ||
                string.IsNullOrWhiteSpace(formAppearanceDto.TitleFontType))
            {
                return BadRequest("Title font size and font type are required.");
            }

            // إنشاء كائن FormAppearanceModel من FormAppearanceDto
            var formAppearance = _context.FormAppearances
                .FirstOrDefault(form => form.FormId == formAppearanceDto.FormId);

            if (formAppearance == null)
            {
                // Create a new FormAppearance if it doesn't exist
                formAppearance = new FormAppearanceModel
                {
                    FormId = formAppearanceDto.FormId
                };

                _context.FormAppearances.Add(formAppearance);
            }

            // Update the existing or newly created FormAppearance
            formAppearance.StyleName = formAppearanceDto.StyleName;
            formAppearance.Dir = formAppearanceDto.Dir;
            formAppearance.TitleFontSize = formAppearanceDto.TitleFontSize;
            formAppearance.TitleFontType = formAppearanceDto.TitleFontType;
            formAppearance.TextFontSize = formAppearanceDto.TextFontSize;
            formAppearance.TextFontType = formAppearanceDto.TextFontType;
            formAppearance.QuestionFontSize = formAppearanceDto.QuestionFontSize;
            formAppearance.QuestionFontType = formAppearanceDto.QuestionFontType;
            formAppearance.GeneralColor = formAppearanceDto.GeneralColor;
            formAppearance.BackgroundColor = formAppearanceDto.BackgroundColor;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // تسجيل الخطأ والرد برسالة خطأ
                // يمكنك استخدام مكتبة تسجيل مثل Serilog أو NLog هنا
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred while saving form appearance data.");
            }

            // إرجاع تخصيص المظهر المضاف مع رمز الحالة 201 Created
            return CreatedAtAction(nameof(AddFormAppearance), new { id = formAppearance.Id }, formAppearance);
        }


        // تعديل تخصيص المظهر
        [HttpPut("UpdateFormAppearance/{id}")]
        public async Task<IActionResult> UpdateFormAppearance(int id, [FromBody] FormAppearanceDto formAppearanceDto)
        {
            if (formAppearanceDto == null)
            {
                return BadRequest("FormAppearance data is null.");
            }

            // تحقق من صحة البيانات
            if (formAppearanceDto.FormId < 1)
            {
                return BadRequest("FormId is required and must be greater than 0.");
            }

            var formExists = await _context.Forms.AnyAsync(form => form.Id == formAppearanceDto.FormId);
            if (!formExists)
            {
                return BadRequest("Form not found.");
            }

            if (string.IsNullOrWhiteSpace(formAppearanceDto.TitleFontSize) ||
                string.IsNullOrWhiteSpace(formAppearanceDto.TitleFontType))
            {
                return BadRequest("Title font size and font type are required.");
            }

            // البحث عن تخصيص المظهر المطلوب تعديله
            var formAppearance = await _context.FormAppearances
                .FirstOrDefaultAsync(fa => fa.FormId == formAppearanceDto.FormId);

            if (formAppearance == null)
            {
                return NotFound("Form appearance not found.");
            }

            // تحديث تخصيص المظهر
            formAppearance.StyleName = formAppearanceDto.StyleName;
            formAppearance.Dir = formAppearanceDto.Dir;
            formAppearance.TitleFontSize = formAppearanceDto.TitleFontSize;
            formAppearance.TitleFontType = formAppearanceDto.TitleFontType;
            formAppearance.TextFontSize = formAppearanceDto.TextFontSize;
            formAppearance.TextFontType = formAppearanceDto.TextFontType;
            formAppearance.QuestionFontSize = formAppearanceDto.QuestionFontSize;
            formAppearance.QuestionFontType = formAppearanceDto.QuestionFontType;
            formAppearance.GeneralColor = formAppearanceDto.GeneralColor;
            formAppearance.BackgroundColor = formAppearanceDto.BackgroundColor;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // تسجيل الخطأ والرد برسالة خطأ
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred while updating form appearance data.");
            }

            // إرجاع تخصيص المظهر المعدل مع رمز الحالة 200 OK
            return Ok(formAppearance);
        }



        //اضافة مجموعة
        [HttpPost("AddGroup")]
        public async Task<IActionResult> AddGroup([FromBody] FormGroupDto formGroupDto)
        {
            if (formGroupDto == null)
            {
                return BadRequest("Form Group data is null.");
            }
            
            if (formGroupDto.FormId < 1)
            {
                return BadRequest("Form number must be specified");
            }
            if (formGroupDto.Contents < 1)
            {
                return BadRequest("The number of items in a row must be specified..");
            }

            var form = _context.Forms.Any(form => form.Id == formGroupDto.FormId);
            if (!form)
            {
                return BadRequest("Form not found.");
            }
            var formGroup = new FormGroupModel
            {
                FormId = formGroupDto.FormId,
                Title = formGroupDto.Title,
                Description = formGroupDto.Description,
                Contents = formGroupDto.Contents,
            };

            _context.FormGroups.Add(formGroup);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred while saving form Group data.");
            }
            return CreatedAtAction(nameof(AddGroup), formGroup);
        }


        // تعديل حقل موجود
        [HttpPut("EditGroup/{id}")]
        public async Task<IActionResult> EditGroup(int id, [FromBody] FormGroupDto formGroupDto)
        {
            if (formGroupDto == null)
            {
                return BadRequest("Form Group data is null.");
            }

            if (formGroupDto.FormId < 1)
            {
                return BadRequest("Form number must be specified");
            }
            if (formGroupDto.Contents < 1)
            {
                return BadRequest("The number of items in a row must be specified..");
            }

            var form = _context.Forms.Any(form => form.Id == formGroupDto.FormId);
            if (!form)
            {
                return BadRequest("Form not found.");
            }



            var group = await _context.FormGroups.FirstOrDefaultAsync(ff => ff.Id == id);
            if (group == null)
            {
                return NotFound("Form field not found.");
            }


            group.FormId = formGroupDto.FormId;
            group.Title = formGroupDto.Title;
            group.Description = formGroupDto.Description;
            group.Contents = formGroupDto.Contents;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred while saving form Group data.");
            }
            return CreatedAtAction(nameof(EditGroup), group);

        }













        //اضافة الحقول
        [HttpPost("AddFormField")]
        public async Task<IActionResult> AddFormField([FromBody] FormFieldDto formFieldDto)
        {
            if (formFieldDto == null)
            {
                return BadRequest("FormField data is null.");
            }

            // تحقق من صحة البيانات
            if (formFieldDto.GroupId < 1)
            {
                return BadRequest("FormId is required and must be greater than 0.");
            }

            if (string.IsNullOrWhiteSpace(formFieldDto.Label))
            {
                return BadRequest("Field label is required.");
            }


            var groups = _context.FormGroups.Any(group => group.Id == formFieldDto.GroupId);
            if (!groups)
            {
                return BadRequest("Form not found.");
            }

            // تحقق من عدم وجود حقل بنفس FormId و Label بالفعل في قاعدة البيانات
            bool fieldExists = await _context.GroupFields
                .AnyAsync(ff => ff.GroupId == formFieldDto.GroupId && ff.Label == formFieldDto.Label && ff.FieldTypeId == formFieldDto.FieldTypeId);

            if (fieldExists)
            {
                return Conflict("A field with the same FormId and label already exists.");
            }

            bool textFieldTypeId = await _context.FieldTypes.AnyAsync(ft => ft.Id == formFieldDto.FieldTypeId);
            if (!textFieldTypeId)
            {
                return Conflict("Field type not found.");
            }

        // إنشاء كائن FormFieldModel من FormFieldDto
        var formField = new FormFieldModel
            {
                GroupId = formFieldDto.GroupId,
                FieldTypeId = formFieldDto.FieldTypeId,
                FieldContentId = formFieldDto.FieldContentId,
                Label = formFieldDto.Label,
                Placeholder = formFieldDto.Placeholder,
                DesignId = formFieldDto.DesignId,
                IsRequired = formFieldDto.IsRequired
            };

            // إضافة الحقل إلى قاعدة البيانات
            _context.GroupFields.Add(formField);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // تسجيل الخطأ والرد برسالة خطأ
                // يمكنك استخدام مكتبة تسجيل مثل Serilog أو NLog هنا
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred while saving form field data.");
            }

            // إرجاع الحقل المضاف مع رمز الحالة 201 Created
            return CreatedAtAction(nameof(AddFormField), new { id = formField.Id }, formField);
        }


        // تعديل حقل موجود
        [HttpPut("UpdateFormField/{id}")]
        public async Task<IActionResult> UpdateFormField(int id, [FromBody] FormFieldDto formFieldDto)
        {
            if (formFieldDto == null)
            {
                return BadRequest("FormField data is null.");
            }

            // تحقق من صحة البيانات
            if (formFieldDto.GroupId < 1)
            {
                return BadRequest("FormId is required and must be greater than 0.");
            }

            if (string.IsNullOrWhiteSpace(formFieldDto.Label))
            {
                return BadRequest("Field label is required.");
            }

            var formExists = await _context.GroupFields.AnyAsync(g => g.Id == formFieldDto.GroupId);
            if (!formExists)
            {
                return BadRequest("Form not found.");
            }
     

            bool textFieldTypeExists = await _context.FieldTypes.AnyAsync(ft => ft.Id == formFieldDto.FieldTypeId);
            if (!textFieldTypeExists)
            {
                return Conflict("Field type not found.");
            }

            // البحث عن الحقل المطلوب تعديله
            var formField = await _context.GroupFields.FirstOrDefaultAsync(ff => ff.Id == id);
            if (formField == null)
            {
                return NotFound("Form field not found.");
            }

        


            // التحقق من عدم وجود حقل آخر بنفس FormId و Label
            bool fieldExists = await _context.GroupFields
                .AnyAsync(ff => ff.GroupId == formFieldDto.GroupId && ff.Label == formFieldDto.Label && ff.FieldTypeId == formFieldDto.FieldTypeId && ff.Id != id);

            if (fieldExists)
            {
                return Conflict("A field with the same FormId and label already exists.");
            }

            // تحديث بيانات الحقل
            formField.GroupId = formFieldDto.GroupId;
            formField.FieldTypeId = formFieldDto.FieldTypeId;
            formField.FieldContentId = formFieldDto.FieldContentId;
            formField.Label = formFieldDto.Label;
            formField.Placeholder = formFieldDto.Placeholder;
            formField.IsRequired = formFieldDto.IsRequired;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // تسجيل الخطأ والرد برسالة خطأ
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred while updating form field data.");
            }

            // إرجاع الحقل المعدل مع رمز الحالة 200 OK
            return Ok(formField);
        }



        //اضافة خيارات الحقول
        [HttpPost("AddFieldOption")]
        public async Task<IActionResult> AddFieldOption([FromBody] FieldOptionModel fieldOptionDto)
        {
            if (fieldOptionDto == null)
            {
                return BadRequest("FieldOption data is null.");
            }



            var formField = _context.GroupFields.Any(ff => ff.Id == fieldOptionDto.FormFieldId);
            if (!formField)
            {
                return BadRequest("form Field not found.");
            }





            var fieldTypeIds = new HashSet<int> { 5, 6, 7 };
            var exists = _context.GroupFields
                .Any(ff => ff.Id == fieldOptionDto.FormFieldId && fieldTypeIds.Contains(ff.FieldTypeId));

    
            if (!exists)
            {
                return BadRequest("Options cannot be added to this field.");
            }


            // تحقق من عدم وجود خيار بنفس FormFieldId و Value بالفعل في قاعدة البيانات
            bool optionExists = await _context.FieldOptions
                .AnyAsync(fo => fo.FormFieldId == fieldOptionDto.FormFieldId && fo.OptionText == fieldOptionDto.OptionText);

            if (optionExists)
            {
                return Conflict("A field option with the same FormFieldId and value already exists.");
            }

            // تحقق من صحة البيانات
            if (fieldOptionDto.FormFieldId <= 0)
            {
                return BadRequest("FormFieldId is required and must be greater than 0.");
            }

            if (string.IsNullOrWhiteSpace(fieldOptionDto.OptionText))
            {
                return BadRequest("Field option value is required.");
            }

            // إنشاء كائن FieldOptionModel من FieldOptionDto
            var fieldOption = new FieldOptionModel
            {
                FormFieldId = fieldOptionDto.FormFieldId,
                OptionText = fieldOptionDto.OptionText
            };

            // إضافة خيار الحقل إلى قاعدة البيانات
            _context.FieldOptions.Add(fieldOption);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // تسجيل الخطأ والرد برسالة خطأ
                // يمكنك استخدام مكتبة تسجيل مثل Serilog أو NLog هنا
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred while saving field option data.");
            }

            // إرجاع خيار الحقل المضاف مع رمز الحالة 201 Created
            return CreatedAtAction(nameof(AddFieldOption), new { id = fieldOption.Id }, fieldOption);
        }

        // تعديل خيار حقل موجود
        [HttpPut("UpdateFieldOption/{id}")]
        public async Task<IActionResult> UpdateFieldOption(int id, [FromBody] FieldOptionModel fieldOptionDto)
        {
            if (fieldOptionDto == null)
            {
                return BadRequest("FieldOption data is null.");
            }

            // تحقق من صحة البيانات
            if (fieldOptionDto.FormFieldId <= 0)
            {
                return BadRequest("FormFieldId is required and must be greater than 0.");
            }

            if (string.IsNullOrWhiteSpace(fieldOptionDto.OptionText))
            {
                return BadRequest("Field option value is required.");
            }

            var formFieldExists = await _context.GroupFields.AnyAsync(ff => ff.Id == fieldOptionDto.FormFieldId);
            if (!formFieldExists)
            {
                return BadRequest("Form field not found.");
            }

            var fieldTypeIds = new HashSet<int> { 5, 6, 7 };
            var validFieldType = await _context.GroupFields
                .AnyAsync(ff => ff.Id == fieldOptionDto.FormFieldId && fieldTypeIds.Contains(ff.FieldTypeId));

            if (!validFieldType)
            {

                return BadRequest("Options cannot be added to this field.");
            }

            // البحث عن الخيار المطلوب تعديله
            var fieldOption = await _context.FieldOptions.FirstOrDefaultAsync(fo => fo.Id == id);
            if (fieldOption == null)
            {
                return NotFound("Field option not found.");
            }

            // التحقق من عدم وجود خيار آخر بنفس FormFieldId و OptionText
            bool optionExists = await _context.FieldOptions
                .AnyAsync(fo => fo.FormFieldId == fieldOptionDto.FormFieldId && fo.OptionText == fieldOptionDto.OptionText && fo.Id != id);

            if (optionExists)
            {
                return Conflict("A field option with the same FormFieldId and value already exists.");
            }

            // تحديث بيانات الخيار
            fieldOption.FormFieldId = fieldOptionDto.FormFieldId;
            fieldOption.OptionText = fieldOptionDto.OptionText;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // تسجيل الخطأ والرد برسالة خطأ
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred while updating field option data.");
            }

            // إرجاع الخيار المعدل مع رمز الحالة 200 OK
            return Ok(fieldOption);
        }


        [HttpGet("responseField")]
        public IActionResult ViewResponse()
        {
            var formResponses = _context.FormResponses
              //  .Include(fr => fr.Form)             
               // .Include(fr => fr.User)
                .Include(fr => fr.ResponseDetails) 
                    .ThenInclude(rd => rd.FormField) 
                .ToList();

 


            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            var jsonString = JsonSerializer.Serialize(formResponses, options);

            return Ok(jsonString);

        }


        [HttpGet("responseDetails")]
        public IActionResult ViewResponseDetails()
        {
            var ResponseDetails = _context.ResponseDetails.ToList();

            return Ok(ResponseDetails);

        }



        [HttpGet("forms")]
        public IActionResult ViewForms()
        {
            var Forms = _context.Forms.ToList();
            if (Forms.Count < 1)
            {
                return Conflict("No users found.");
            }
            return Ok(Forms);
        }







        [HttpGet("form")]
        public IActionResult ViewForm(string id)
        {
            var formLinkId = id;
            var form = _context.Forms.FirstOrDefault(fa => fa.Link == formLinkId);
            if (form == null)
            {
                return NotFound("Form not found.");
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
                         Description = field.gf.Description??null,
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
                Description = form.Description??null,
                IsActive = form.IsActive,
                IsAvailable = form.IsAvailable,
                FormAppearance = formAppearance,
                FormGroups = formGroupsView 
            };

            return Ok(formView);
        }



    }
}