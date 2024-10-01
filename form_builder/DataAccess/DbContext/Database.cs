using form_builder.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace form_builder.DataAccess
{
    public class Database : DbContext
    {
        public Database(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<FormModel> Forms { get; set; }
        public DbSet<FormAppearanceModel> FormAppearances { get; set; }
        public DbSet<FormGroupModel> FormGroups { get; set; }
        public DbSet<FormFieldModel> GroupFields { get; set; }
        public DbSet<FieldTypeModel> FieldTypes { get; set; }
        public DbSet<FieldContentModel> FieldContent { get; set; }
        public DbSet<FieldDesignModel> FieldDesign { get; set; }
        public DbSet<FieldOptionModel> FieldOptions { get; set; }
        public DbSet<FormResponseModel> FormResponses { get; set; }
        public DbSet<ResponseDetailModel> ResponseDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserModel>()
                .HasMany(u => u.Forms)
                .WithOne(f => f.User)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FormModel>()
                .HasOne(f => f.FormAppearance)
                .WithOne(fa => fa.Form)
                .HasForeignKey<FormAppearanceModel>(fa => fa.FormId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FormModel>()
                .HasMany(f => f.FormGroups)
                .WithOne(ff => ff.Form)
                .HasForeignKey(ff => ff.FormId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FormModel>()
            .HasIndex(u => u.Link)
            .IsUnique();


            modelBuilder.Entity<FormGroupModel>()
                .HasMany(f => f.Fields)
                .WithOne(ff => ff.Group)
                .HasForeignKey(ff => ff.GroupId)
                .OnDelete(DeleteBehavior.Cascade);


            // علاقة الـ FormField مع FieldType: FormField لديه FieldType واحد
            modelBuilder.Entity<FormFieldModel>()
                .HasOne(ff => ff.FieldType)
                .WithMany(ft => ft.FormFields)
                .HasForeignKey(ff => ff.FieldTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // علاقة الـ FormField مع FieldType: FormField لديه FieldType واحد
            modelBuilder.Entity<FormFieldModel>()
                .HasOne(ff => ff.FieldContent)
                .WithMany(ft => ft.FormFields)
                .HasForeignKey(ff => ff.FieldContentId)
                .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<FormFieldModel>()
               .HasOne(ff => ff.FieldDesign)
               .WithMany(ft => ft.FormFields)
               .HasForeignKey(ff => ff.DesignId)
               .OnDelete(DeleteBehavior.Restrict);





            // علاقة الـ FormField مع FieldOptions: FormField يمكن أن يكون لديه العديد من FieldOptions
            modelBuilder.Entity<FormFieldModel>()
                .HasMany(ff => ff.FieldOptions)
                .WithOne(fo => fo.FormField)
                .HasForeignKey(fo => fo.FormFieldId)
                .OnDelete(DeleteBehavior.Cascade);

            // علاقة الـ Form مع FormResponses: Form يمكن أن يكون لديه العديد من FormResponses
            modelBuilder.Entity<FormModel>()
                .HasMany(f => f.FormResponses)
                .WithOne(fr => fr.Form)
                .HasForeignKey(fr => fr.FormId)
                .OnDelete(DeleteBehavior.Cascade);

            // علاقة الـ User مع FormResponses: User يمكن أن يكون لديه العديد من FormResponses
            modelBuilder.Entity<UserModel>()
                .HasMany(u => u.FormResponses)
                .WithOne(fr => fr.User)
                .HasForeignKey(fr => fr.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // علاقة الـ FormResponse مع ResponseDetails: FormResponse يمكن أن يكون لديه العديد من ResponseDetails
            modelBuilder.Entity<FormResponseModel>()
                .HasMany(fr => fr.ResponseDetails)
                .WithOne(rd => rd.FormResponse)
                .HasForeignKey(rd => rd.FormResponseId)
                .OnDelete(DeleteBehavior.Cascade);

            // علاقة الـ FormField مع ResponseDetails: FormField يمكن أن يكون لديه العديد من ResponseDetails
            modelBuilder.Entity<FormFieldModel>()
                .HasMany(ff => ff.ResponseDetails)
                .WithOne(rd => rd.FormField)
                .HasForeignKey(rd => rd.FormFieldId)
                .OnDelete(DeleteBehavior.Restrict);




            modelBuilder.Entity<FieldDesignModel>()
                    .HasOne(fd => fd.FieldType)
                    .WithMany(ft => ft.FormDesign)
                    .HasForeignKey(fd => fd.FieldTypeId)
                    .OnDelete(DeleteBehavior.Restrict);






            modelBuilder.Entity<FieldTypeModel>()
                .HasData(new SetDefaultData().SetFieldType());

            modelBuilder.Entity<FieldContentModel>()
                    .HasData(new SetDefaultData().SetFieldContent());

            modelBuilder.Entity<FieldDesignModel>()
         .HasData(new SetDefaultData().SetFieldDesign());

            

        }

    }


}