﻿// <auto-generated />
using MSA.backend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MSA.backend.Api.Migrations
{
    [DbContext(typeof(WebAPIDBContext))]
    partial class WebAPIDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.17");

            modelBuilder.Entity("MSA.backend.Api.Model.Move", b =>
                {
                    b.Property<string>("move")
                        .HasColumnType("TEXT");

                    b.Property<string>("name")
                        .HasColumnType("TEXT");

                    b.HasKey("move");

                    b.ToTable("moves");
                });
#pragma warning restore 612, 618
        }
    }
}