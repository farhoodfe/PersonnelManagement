﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PersonnelManagement.Data;

#nullable disable

namespace PersonnelManagement.Data.Migrations
{
    [DbContext(typeof(PersonnelDBContext))]
    partial class PersonnelDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PersonnelManagement.Data.Entities.DynamicFieldDefinition", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("pk_Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("DisplayName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("DisplayName");

                    b.Property<string>("FieldName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("FieldName");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("IsDeleted");

                    b.Property<bool?>("IsRequired")
                        .HasColumnType("bit")
                        .HasColumnName("IsRequired");

                    b.Property<int?>("Type")
                        .HasColumnType("int")
                        .HasColumnName("FieldType");

                    b.HasKey("Id");

                    b.ToTable("DynamicFieldDefinition", "MIS");
                });

            modelBuilder.Entity("PersonnelManagement.Data.Entities.FieldSubmission", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("pk_Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("FieldValue")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("FieldValue");

                    b.Property<long?>("Fk_FieldDefinition")
                        .HasColumnType("bigint")
                        .HasColumnName("fk_FieldDefinition");

                    b.Property<long>("Fk_PersonInfo")
                        .HasColumnType("bigint")
                        .HasColumnName("fk_PersonInfo");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("IsDeleted");

                    b.HasKey("Id");

                    b.HasIndex("Fk_FieldDefinition");

                    b.HasIndex("Fk_PersonInfo");

                    b.ToTable("FieldSubmission", "MIS");
                });

            modelBuilder.Entity("PersonnelManagement.Data.Entities.Formula", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("pk_Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("FormulaName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("FormulaName");

                    b.Property<string>("FormulaText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("FormulaText");

                    b.HasKey("Id");

                    b.ToTable("Formula", "MIS");
                });

            modelBuilder.Entity("PersonnelManagement.Data.Entities.PersonInfo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("pk_Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("FName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("FName");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("IsDeleted");

                    b.Property<string>("LName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("LName");

                    b.Property<string>("PersonnelCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("PersonnelCode");

                    b.HasKey("Id");

                    b.ToTable("PersonInfo", "MIS");
                });

            modelBuilder.Entity("PersonnelManagement.Data.Entities.FieldSubmission", b =>
                {
                    b.HasOne("PersonnelManagement.Data.Entities.DynamicFieldDefinition", "fieldDefinition")
                        .WithMany()
                        .HasForeignKey("Fk_FieldDefinition");

                    b.HasOne("PersonnelManagement.Data.Entities.PersonInfo", "PersonInfo")
                        .WithMany("FieldSubmissions")
                        .HasForeignKey("Fk_PersonInfo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PersonInfo");

                    b.Navigation("fieldDefinition");
                });

            modelBuilder.Entity("PersonnelManagement.Data.Entities.PersonInfo", b =>
                {
                    b.Navigation("FieldSubmissions");
                });
#pragma warning restore 612, 618
        }
    }
}
