﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SurveyWS.Infrastructure.EntityFramework;

#nullable disable

namespace SurveyWS.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0-rc.1.21452.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SurveyWS.Infrastructure.EntityFramework.Entities.SurveyTemplateDetailEf", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<bool>("Active")
                        .HasColumnType("bit")
                        .HasColumnName("active");

                    b.Property<string>("FieldDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("field_description");

                    b.Property<string>("FieldName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("field_name");

                    b.Property<string>("FieldType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("field_type");

                    b.Property<long>("survey_template_id")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("survey_template_id");

                    b.ToTable("survey_template_detail");
                });

            modelBuilder.Entity("SurveyWS.Infrastructure.EntityFramework.Entities.SurveyTemplateEf", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<bool>("Active")
                        .HasColumnType("bit")
                        .HasColumnName("active");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("description");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("modified_at");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("modified_by");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<DateTime?>("createdAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<string>("createdBy")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("created_by");

                    b.HasKey("Id");

                    b.ToTable("survey_template");
                });

            modelBuilder.Entity("SurveyWS.Infrastructure.EntityFramework.Entities.SurveyTemplateDetailEf", b =>
                {
                    b.HasOne("SurveyWS.Infrastructure.EntityFramework.Entities.SurveyTemplateEf", "SurveyTemplate")
                        .WithMany()
                        .HasForeignKey("survey_template_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SurveyTemplate");
                });
#pragma warning restore 612, 618
        }
    }
}
