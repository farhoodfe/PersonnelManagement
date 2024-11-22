﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PersonnelManagement.Data;

#nullable disable

namespace PersonnelManagement.Data.Migrations
{
    [DbContext(typeof(PersonnelDBContext))]
    [Migration("20241122085958_3entitiessadded")]
    partial class _3entitiessadded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PersonnelManagement.Data.Entities.FieldDefinition", b =>
                {
                    b.Property<long>("Pk_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("pk_Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Pk_Id"), 1L, 1);

                    b.Property<string>("DisplayName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("DisplayName");

                    b.Property<string>("FieldName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("FieldName");

                    b.Property<bool?>("IsRequired")
                        .HasColumnType("bit")
                        .HasColumnName("IsRequired");

                    b.Property<int?>("Type")
                        .HasColumnType("int")
                        .HasColumnName("FieldType");

                    b.HasKey("Pk_Id");

                    b.ToTable("FieldDefinition", "MIS");
                });

            modelBuilder.Entity("PersonnelManagement.Data.Entities.FieldSubmission", b =>
                {
                    b.Property<long>("Pk_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("pk_Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Pk_Id"), 1L, 1);

                    b.Property<string>("FieldValue")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("FieldValue");

                    b.Property<long>("Fk_FieldDefinition")
                        .HasColumnType("bigint")
                        .HasColumnName("fk_FieldDefinition");

                    b.Property<long>("Fk_PersonInfo")
                        .HasColumnType("bigint")
                        .HasColumnName("fk_PersonInfo");

                    b.HasKey("Pk_Id");

                    b.HasIndex("Fk_FieldDefinition");

                    b.HasIndex("Fk_PersonInfo");

                    b.ToTable("FieldSubmission", "MIS");
                });

            modelBuilder.Entity("PersonnelManagement.Data.Entities.PersonInfo", b =>
                {
                    b.Property<long>("Pk_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("pk_Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Pk_Id"), 1L, 1);

                    b.Property<string>("FName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("FName");

                    b.Property<string>("LName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("LName");

                    b.Property<string>("PersonnelCode")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("PersonnelCode");

                    b.HasKey("Pk_Id");

                    b.ToTable("PersonInfo", "MIS");
                });

            modelBuilder.Entity("PersonnelManagement.Data.Entities.FieldSubmission", b =>
                {
                    b.HasOne("PersonnelManagement.Data.Entities.FieldDefinition", "fieldDefinition")
                        .WithMany()
                        .HasForeignKey("Fk_FieldDefinition")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

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
