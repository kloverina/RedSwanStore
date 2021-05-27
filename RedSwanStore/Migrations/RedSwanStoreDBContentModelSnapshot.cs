﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RedSwanStore.Data;

namespace RedSwanStore.Migrations
{
    [DbContext(typeof(RedSwanStoreDBContent))]
    partial class RedSwanStoreDBContentModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FeatureGameFilter", b =>
                {
                    b.Property<int>("FeaturesId")
                        .HasColumnType("int");

                    b.Property<int>("GameFiltersId")
                        .HasColumnType("int");

                    b.HasKey("FeaturesId", "GameFiltersId");

                    b.HasIndex("GameFiltersId");

                    b.ToTable("FeatureGameFilter");
                });

            modelBuilder.Entity("GameFilterGenre", b =>
                {
                    b.Property<int>("GameFiltersId")
                        .HasColumnType("int");

                    b.Property<int>("GenresId")
                        .HasColumnType("int");

                    b.HasKey("GameFiltersId", "GenresId");

                    b.HasIndex("GenresId");

                    b.ToTable("GameFilterGenre");
                });

            modelBuilder.Entity("RedSwanStore.Data.Models.Feature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Features");
                });

            modelBuilder.Entity("RedSwanStore.Data.Models.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Developer")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("GameUrl")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("RedSwanStore.Data.Models.GameFilter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GameId")
                        .IsUnique();

                    b.ToTable("GameFilters");
                });

            modelBuilder.Entity("RedSwanStore.Data.Models.GameInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cover")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DetailedDescription")
                        .HasMaxLength(10000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Discount")
                        .HasColumnType("real");

                    b.Property<DateTime>("DiscountEndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<string>("LegalInfo")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.Property<byte>("Rating")
                        .HasColumnType("tinyint");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ShortDescription")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.HasIndex("GameId")
                        .IsUnique();

                    b.ToTable("GameInfos");
                });

            modelBuilder.Entity("RedSwanStore.Data.Models.GameMedia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<string>("Screenshots")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Trailers")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GameId")
                        .IsUnique();

                    b.ToTable("GameMedias");
                });

            modelBuilder.Entity("RedSwanStore.Data.Models.GameSystemRequirement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte>("DirectX")
                        .HasColumnType("tinyint");

                    b.Property<long>("DiskSpaceMB")
                        .HasColumnType("bigint");

                    b.Property<string>("ExtraInfo")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<string>("MaxCpu")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("MaxGpu")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<long>("MaxRamMB")
                        .HasColumnType("bigint");

                    b.Property<string>("MinCpu")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("MinGpu")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<long>("MinRamMB")
                        .HasColumnType("bigint");

                    b.Property<string>("SupportedOses")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("SupportedTextLanguages")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SupportedVoiceLanguages")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GameId")
                        .IsUnique();

                    b.ToTable("GameSystemRequirements");
                });

            modelBuilder.Entity("RedSwanStore.Data.Models.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("RedSwanStore.Data.Models.PriceCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("MaxPrice")
                        .HasColumnType("money");

                    b.Property<decimal>("MinPrice")
                        .HasColumnType("money");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("PriceCategories");
                });

            modelBuilder.Entity("RedSwanStore.Data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Balance")
                        .HasColumnType("money");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("GetNewsOnEmail")
                        .HasColumnType("bit");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserUrl")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RedSwanStore.Data.Models.UserLibrary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserLibraries");
                });

            modelBuilder.Entity("RedSwanStore.Data.Models.UserLibraryGame", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<long>("HoursPlayed")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsFavourite")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastPlayed")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserLibraryId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("UserLibraryId");

                    b.ToTable("UserLibraryGames");
                });

            modelBuilder.Entity("FeatureGameFilter", b =>
                {
                    b.HasOne("RedSwanStore.Data.Models.Feature", null)
                        .WithMany()
                        .HasForeignKey("FeaturesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RedSwanStore.Data.Models.GameFilter", null)
                        .WithMany()
                        .HasForeignKey("GameFiltersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GameFilterGenre", b =>
                {
                    b.HasOne("RedSwanStore.Data.Models.GameFilter", null)
                        .WithMany()
                        .HasForeignKey("GameFiltersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RedSwanStore.Data.Models.Genre", null)
                        .WithMany()
                        .HasForeignKey("GenresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RedSwanStore.Data.Models.GameFilter", b =>
                {
                    b.HasOne("RedSwanStore.Data.Models.Game", "Game")
                        .WithOne("GameFilter")
                        .HasForeignKey("RedSwanStore.Data.Models.GameFilter", "GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");
                });

            modelBuilder.Entity("RedSwanStore.Data.Models.GameInfo", b =>
                {
                    b.HasOne("RedSwanStore.Data.Models.Game", "Game")
                        .WithOne("GameInfo")
                        .HasForeignKey("RedSwanStore.Data.Models.GameInfo", "GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");
                });

            modelBuilder.Entity("RedSwanStore.Data.Models.GameMedia", b =>
                {
                    b.HasOne("RedSwanStore.Data.Models.Game", "Game")
                        .WithOne("GameMedia")
                        .HasForeignKey("RedSwanStore.Data.Models.GameMedia", "GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");
                });

            modelBuilder.Entity("RedSwanStore.Data.Models.GameSystemRequirement", b =>
                {
                    b.HasOne("RedSwanStore.Data.Models.Game", "Game")
                        .WithOne("GameSystemRequirements")
                        .HasForeignKey("RedSwanStore.Data.Models.GameSystemRequirement", "GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");
                });

            modelBuilder.Entity("RedSwanStore.Data.Models.UserLibrary", b =>
                {
                    b.HasOne("RedSwanStore.Data.Models.User", "User")
                        .WithOne("Library")
                        .HasForeignKey("RedSwanStore.Data.Models.UserLibrary", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("RedSwanStore.Data.Models.UserLibraryGame", b =>
                {
                    b.HasOne("RedSwanStore.Data.Models.Game", "Game")
                        .WithMany("UserLibraryGames")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RedSwanStore.Data.Models.UserLibrary", "UserLibrary")
                        .WithMany("UserLibraryGames")
                        .HasForeignKey("UserLibraryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("UserLibrary");
                });

            modelBuilder.Entity("RedSwanStore.Data.Models.Game", b =>
                {
                    b.Navigation("GameFilter");

                    b.Navigation("GameInfo");

                    b.Navigation("GameMedia");

                    b.Navigation("GameSystemRequirements");

                    b.Navigation("UserLibraryGames");
                });

            modelBuilder.Entity("RedSwanStore.Data.Models.User", b =>
                {
                    b.Navigation("Library");
                });

            modelBuilder.Entity("RedSwanStore.Data.Models.UserLibrary", b =>
                {
                    b.Navigation("UserLibraryGames");
                });
#pragma warning restore 612, 618
        }
    }
}
