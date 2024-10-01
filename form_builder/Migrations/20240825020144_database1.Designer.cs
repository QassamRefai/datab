﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using form_builder.DataAccess;

#nullable disable

namespace form_builder.Migrations
{
    [DbContext(typeof(Database))]
    [Migration("20240825020144_database1")]
    partial class database1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("form_builder.DataAccess.Models.FieldContentModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("FieldContent");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "textLong"
                        },
                        new
                        {
                            Id = 2,
                            Name = "textShort"
                        },
                        new
                        {
                            Id = 3,
                            Name = "number"
                        },
                        new
                        {
                            Id = 4,
                            Name = "date"
                        },
                        new
                        {
                            Id = 5,
                            Name = "time"
                        },
                        new
                        {
                            Id = 6,
                            Name = "multipleOptions"
                        },
                        new
                        {
                            Id = 7,
                            Name = "rating"
                        });
                });

            modelBuilder.Entity("form_builder.DataAccess.Models.FieldDesignModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FieldTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FieldTypeId");

                    b.ToTable("FieldDesign");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FieldTypeId = 16,
                            Name = "switch"
                        });
                });

            modelBuilder.Entity("form_builder.DataAccess.Models.FieldOptionModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FormFieldId")
                        .HasColumnType("int");

                    b.Property<string>("OptionText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FormFieldId");

                    b.ToTable("FieldOptions");
                });

            modelBuilder.Entity("form_builder.DataAccess.Models.FieldTypeModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("FieldTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "text"
                        },
                        new
                        {
                            Id = 2,
                            Name = "email"
                        },
                        new
                        {
                            Id = 3,
                            Name = "tel"
                        },
                        new
                        {
                            Id = 4,
                            Name = "url"
                        },
                        new
                        {
                            Id = 5,
                            Name = "password"
                        },
                        new
                        {
                            Id = 6,
                            Name = "search"
                        },
                        new
                        {
                            Id = 7,
                            Name = "number"
                        },
                        new
                        {
                            Id = 8,
                            Name = "textarea"
                        },
                        new
                        {
                            Id = 9,
                            Name = "datetime-local"
                        },
                        new
                        {
                            Id = 10,
                            Name = "date"
                        },
                        new
                        {
                            Id = 11,
                            Name = "time"
                        },
                        new
                        {
                            Id = 12,
                            Name = "week"
                        },
                        new
                        {
                            Id = 13,
                            Name = "month"
                        },
                        new
                        {
                            Id = 14,
                            Name = "color"
                        },
                        new
                        {
                            Id = 15,
                            Name = "file"
                        },
                        new
                        {
                            Id = 16,
                            Name = "checkbox"
                        },
                        new
                        {
                            Id = 17,
                            Name = "radio"
                        },
                        new
                        {
                            Id = 18,
                            Name = "range"
                        },
                        new
                        {
                            Id = 19,
                            Name = "hidden"
                        },
                        new
                        {
                            Id = 20,
                            Name = "image"
                        },
                        new
                        {
                            Id = 21,
                            Name = "button"
                        },
                        new
                        {
                            Id = 22,
                            Name = "submit"
                        },
                        new
                        {
                            Id = 23,
                            Name = "reset"
                        },
                        new
                        {
                            Id = 24,
                            Name = "select"
                        });
                });

            modelBuilder.Entity("form_builder.DataAccess.Models.FormAppearanceModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BackgroundColor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Dir")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FormId")
                        .HasColumnType("int");

                    b.Property<string>("GeneralColor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuestionFontSize")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuestionFontType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StyleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TextFontSize")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TextFontType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TitleFontSize")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TitleFontType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FormId")
                        .IsUnique();

                    b.ToTable("FormAppearances");
                });

            modelBuilder.Entity("form_builder.DataAccess.Models.FormFieldModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DesignId")
                        .HasColumnType("int");

                    b.Property<int>("FieldContentId")
                        .HasColumnType("int");

                    b.Property<int>("FieldTypeId")
                        .HasColumnType("int");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<bool>("IsRequired")
                        .HasColumnType("bit");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Placeholder")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DesignId");

                    b.HasIndex("FieldContentId");

                    b.HasIndex("FieldTypeId");

                    b.HasIndex("GroupId");

                    b.ToTable("GroupFields");
                });

            modelBuilder.Entity("form_builder.DataAccess.Models.FormGroupModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Contents")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FormId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FormId");

                    b.ToTable("FormGroups");
                });

            modelBuilder.Entity("form_builder.DataAccess.Models.FormModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Forms");
                });

            modelBuilder.Entity("form_builder.DataAccess.Models.FormResponseModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FormId")
                        .HasColumnType("int");

                    b.Property<DateTime>("SubmissionDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FormId");

                    b.HasIndex("UserId");

                    b.ToTable("FormResponses");
                });

            modelBuilder.Entity("form_builder.DataAccess.Models.ResponseDetailModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FormFieldId")
                        .HasColumnType("int");

                    b.Property<int>("FormResponseId")
                        .HasColumnType("int");

                    b.Property<string>("ResponseValue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FormFieldId");

                    b.HasIndex("FormResponseId");

                    b.ToTable("ResponseDetails");
                });

            modelBuilder.Entity("form_builder.DataAccess.Models.UserModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("form_builder.DataAccess.Models.FieldDesignModel", b =>
                {
                    b.HasOne("form_builder.DataAccess.Models.FieldTypeModel", "FieldType")
                        .WithMany("FormDesign")
                        .HasForeignKey("FieldTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("FieldType");
                });

            modelBuilder.Entity("form_builder.DataAccess.Models.FieldOptionModel", b =>
                {
                    b.HasOne("form_builder.DataAccess.Models.FormFieldModel", "FormField")
                        .WithMany("FieldOptions")
                        .HasForeignKey("FormFieldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FormField");
                });

            modelBuilder.Entity("form_builder.DataAccess.Models.FormAppearanceModel", b =>
                {
                    b.HasOne("form_builder.DataAccess.Models.FormModel", "Form")
                        .WithOne("FormAppearance")
                        .HasForeignKey("form_builder.DataAccess.Models.FormAppearanceModel", "FormId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Form");
                });

            modelBuilder.Entity("form_builder.DataAccess.Models.FormFieldModel", b =>
                {
                    b.HasOne("form_builder.DataAccess.Models.FieldDesignModel", "FieldDesign")
                        .WithMany("FormFields")
                        .HasForeignKey("DesignId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("form_builder.DataAccess.Models.FieldContentModel", "FieldContent")
                        .WithMany("FormFields")
                        .HasForeignKey("FieldContentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("form_builder.DataAccess.Models.FieldTypeModel", "FieldType")
                        .WithMany("FormFields")
                        .HasForeignKey("FieldTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("form_builder.DataAccess.Models.FormGroupModel", "Group")
                        .WithMany("Fields")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FieldContent");

                    b.Navigation("FieldDesign");

                    b.Navigation("FieldType");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("form_builder.DataAccess.Models.FormGroupModel", b =>
                {
                    b.HasOne("form_builder.DataAccess.Models.FormModel", "Form")
                        .WithMany("FormGroups")
                        .HasForeignKey("FormId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Form");
                });

            modelBuilder.Entity("form_builder.DataAccess.Models.FormModel", b =>
                {
                    b.HasOne("form_builder.DataAccess.Models.UserModel", "User")
                        .WithMany("Forms")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("form_builder.DataAccess.Models.FormResponseModel", b =>
                {
                    b.HasOne("form_builder.DataAccess.Models.FormModel", "Form")
                        .WithMany("FormResponses")
                        .HasForeignKey("FormId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("form_builder.DataAccess.Models.UserModel", "User")
                        .WithMany("FormResponses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Form");

                    b.Navigation("User");
                });

            modelBuilder.Entity("form_builder.DataAccess.Models.ResponseDetailModel", b =>
                {
                    b.HasOne("form_builder.DataAccess.Models.FormFieldModel", "FormField")
                        .WithMany("ResponseDetails")
                        .HasForeignKey("FormFieldId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("form_builder.DataAccess.Models.FormResponseModel", "FormResponse")
                        .WithMany("ResponseDetails")
                        .HasForeignKey("FormResponseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FormField");

                    b.Navigation("FormResponse");
                });

            modelBuilder.Entity("form_builder.DataAccess.Models.FieldContentModel", b =>
                {
                    b.Navigation("FormFields");
                });

            modelBuilder.Entity("form_builder.DataAccess.Models.FieldDesignModel", b =>
                {
                    b.Navigation("FormFields");
                });

            modelBuilder.Entity("form_builder.DataAccess.Models.FieldTypeModel", b =>
                {
                    b.Navigation("FormDesign");

                    b.Navigation("FormFields");
                });

            modelBuilder.Entity("form_builder.DataAccess.Models.FormFieldModel", b =>
                {
                    b.Navigation("FieldOptions");

                    b.Navigation("ResponseDetails");
                });

            modelBuilder.Entity("form_builder.DataAccess.Models.FormGroupModel", b =>
                {
                    b.Navigation("Fields");
                });

            modelBuilder.Entity("form_builder.DataAccess.Models.FormModel", b =>
                {
                    b.Navigation("FormAppearance")
                        .IsRequired();

                    b.Navigation("FormGroups");

                    b.Navigation("FormResponses");
                });

            modelBuilder.Entity("form_builder.DataAccess.Models.FormResponseModel", b =>
                {
                    b.Navigation("ResponseDetails");
                });

            modelBuilder.Entity("form_builder.DataAccess.Models.UserModel", b =>
                {
                    b.Navigation("FormResponses");

                    b.Navigation("Forms");
                });
#pragma warning restore 612, 618
        }
    }
}
