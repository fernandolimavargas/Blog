﻿// <auto-generated />
using System;
using BlogFecomercio.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BlogFecomercio.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.36")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BlogFecomercio.Models.Comentario", b =>
                {
                    b.Property<int>("ComentarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ComentarioId"), 1L, 1);

                    b.Property<DateTime>("DataHoraComentario")
                        .HasColumnType("datetime2");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<string>("Texto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("ComentarioId");

                    b.HasIndex("PostId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Comentarios");
                });

            modelBuilder.Entity("BlogFecomercio.Models.Curtida", b =>
                {
                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataCurtida")
                        .HasColumnType("datetime2");

                    b.HasKey("UsuarioId", "PostId");

                    b.HasIndex("PostId");

                    b.ToTable("Curtidas");
                });

            modelBuilder.Entity("BlogFecomercio.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Body")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DataHoraPostados")
                        .HasColumnType("datetime2");

                    b.Property<string>("TagNome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Usuarioid")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Usuarioid");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("BlogFecomercio.Models.Seguidor", b =>
                {
                    b.Property<int>("SeguidorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataSeguimento")
                        .HasColumnType("datetime2");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("SeguidorId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Seguidores");
                });

            modelBuilder.Entity("BlogFecomercio.Models.Tag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TagId"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TagId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("BlogFecomercio.Models.Usuario", b =>
                {
                    b.Property<int>("Usuarioid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Usuarioid"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Usuarioid");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("BlogFecomercio.Models.Comentario", b =>
                {
                    b.HasOne("BlogFecomercio.Models.Post", "Post")
                        .WithMany("Comentarios")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlogFecomercio.Models.Usuario", "Usuario")
                        .WithMany("Comentarios")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("BlogFecomercio.Models.Curtida", b =>
                {
                    b.HasOne("BlogFecomercio.Models.Post", "Post")
                        .WithMany("Curtidas")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlogFecomercio.Models.Usuario", "Usuario")
                        .WithMany("Curtidas")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("BlogFecomercio.Models.Post", b =>
                {
                    b.HasOne("BlogFecomercio.Models.Usuario", "Usuario")
                        .WithMany("Posts")
                        .HasForeignKey("Usuarioid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("BlogFecomercio.Models.Seguidor", b =>
                {
                    b.HasOne("BlogFecomercio.Models.Usuario", "SeguidorUsuario")
                        .WithMany("Seguindo")
                        .HasForeignKey("SeguidorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BlogFecomercio.Models.Usuario", "Usuario")
                        .WithMany("Seguidores")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("SeguidorUsuario");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("BlogFecomercio.Models.Post", b =>
                {
                    b.Navigation("Comentarios");

                    b.Navigation("Curtidas");
                });

            modelBuilder.Entity("BlogFecomercio.Models.Usuario", b =>
                {
                    b.Navigation("Comentarios");

                    b.Navigation("Curtidas");

                    b.Navigation("Posts");

                    b.Navigation("Seguidores");

                    b.Navigation("Seguindo");
                });
#pragma warning restore 612, 618
        }
    }
}
