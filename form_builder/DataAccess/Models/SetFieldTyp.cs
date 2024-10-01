namespace form_builder.DataAccess.Models
{
    public class SetDefaultData
    {

        public List<FieldTypeModel> SetFieldType()
        {
            var fieldType = new List<FieldTypeModel> {

        new FieldTypeModel
        {
            Id = 1,
            Name = "text",
        },
        new FieldTypeModel
        {
            Id = 2,
            Name = "email",
        },
        new FieldTypeModel
        {
            Id = 3,
            Name = "tel",
        },
        new FieldTypeModel
        {
            Id = 4,
            Name = "url",
        },
        new FieldTypeModel
        {
            Id = 5,
            Name = "password",
        },
        new FieldTypeModel
        {
            Id = 6,
            Name = "search",
        },
        new FieldTypeModel
        {
            Id = 7,
            Name = "number",
        },
        new FieldTypeModel
        {
            Id = 8,
            Name = "textarea",
        },
        new FieldTypeModel
        {
            Id = 9,
            Name = "datetime-local",
        },
        new FieldTypeModel
        {
            Id = 10,
            Name = "date",
        },
        new FieldTypeModel
        {
            Id = 11,
            Name = "time",
        },
        new FieldTypeModel
        {
            Id = 12,
            Name = "week",
        },
        new FieldTypeModel
        {
            Id = 13,
            Name = "month",
        },
        new FieldTypeModel
        {
            Id = 14,
            Name = "color",
        },
        new FieldTypeModel
        {
            Id = 15,
            Name = "file",
        },
        new FieldTypeModel
        {
            Id = 16,
            Name = "checkbox",
        },
        new FieldTypeModel
        {
            Id = 17,
            Name = "radio", // زر اختيار
        },
        new FieldTypeModel
        {
            Id = 18,
            Name = "range", // نطاق رقمي (شريط تمرير)
        },
        new FieldTypeModel
        {
            Id = 19,
            Name = "hidden",
        },
        new FieldTypeModel
        {
            Id = 20,
            Name = "image",
        },
        new FieldTypeModel
        {
            Id = 21,
            Name = "button",
        },
        new FieldTypeModel
        {
            Id = 22,
            Name = "submit",
        },
        new FieldTypeModel
        {
            Id = 23,
            Name = "reset",
        },
        new FieldTypeModel
        {
            Id = 24,
            Name = "select",
        }
    };


            return fieldType;
        }

  
        public List<FieldContentModel> SetFieldContent()
        {
            var fieldContent = new List<FieldContentModel> {

            new FieldContentModel
            {
                         Id = 1,
                         Name = "textLong",
                     }, new FieldContentModel
                     {
                         Id = 2,
                         Name = "textShort",
                     }, new FieldContentModel
                    {
                        Id = 3,
                        Name = "number",
                    }, new FieldContentModel
                    {
                        Id = 4,
                        Name = "date",
                    }, new FieldContentModel
                    {
                        Id = 5,
                        Name = "time",
                    }, new FieldContentModel
                    {
                        Id = 6,
                        Name = "multipleOptions",
                    }, new FieldContentModel
                    {
                        Id = 7,
                        Name = "rating",
                    }
            };
            return fieldContent;
        }




        public List<FieldDesignModel> SetFieldDesign()
        {
            var fieldDesign = new List<FieldDesignModel> {

            new FieldDesignModel
            {
                         Id = 1,
                         Name = "switch",
                         FieldTypeId = 16,
                     }
                     
            };
            return fieldDesign;
        }



    }
}
