﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Infraestucture.Data;

#nullable disable

namespace Pizzaria_WebApiAspNet_8._0RESTful.Migrations
{
    [DbContext(typeof(PizzariaContext))]
    [Migration("20240822124848_Att3")]
    partial class Att3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Core.Models.PizzaCategoriaModel", b =>
                {
                    b.Property<int>("PizzaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("PizzaId"));

                    b.Property<string>("PizzaCategoria")
                        .HasColumnType("longtext");

                    b.Property<int>("PizzaEstoque")
                        .HasColumnType("int");

                    b.HasKey("PizzaId");

                    b.ToTable("CategoriaPizza");
                });

            modelBuilder.Entity("Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Core.Models.PizzariaModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CategoriaPizzaId")
                        .HasColumnType("int");

                    b.Property<string>("Descricao")
                        .HasColumnType("longtext");

                    b.Property<int>("PizzaCategoriaId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Sabor")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaPizzaId");

                    b.ToTable("Pizza");
                });

            modelBuilder.Entity("Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Core.Models.PizzariaModel", b =>
                {
                    b.HasOne("Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Core.Models.PizzaCategoriaModel", "Categoria")
                        .WithMany("Pizzaria")
                        .HasForeignKey("CategoriaPizzaId");

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Core.Models.PizzaCategoriaModel", b =>
                {
                    b.Navigation("Pizzaria");
                });
#pragma warning restore 612, 618
        }
    }
}
