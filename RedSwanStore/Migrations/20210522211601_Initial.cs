using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RedSwanStore.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Developer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PriceCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MinPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Login = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameFilters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameFilters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameFilters_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    Cover = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<float>(type: "real", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rating = table.Column<byte>(type: "tinyint", nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DetailedDescription = table.Column<string>(type: "nvarchar(max)", maxLength: 10000, nullable: true),
                    LegalInfo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameInfos_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameMedias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    Trailers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Screenshots = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameMedias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameMedias_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameSystemRequirements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    MinCpu = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    MaxCpu = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    MinRamMB = table.Column<long>(type: "bigint", nullable: false),
                    MaxRamMB = table.Column<long>(type: "bigint", nullable: false),
                    DiskSpaceMB = table.Column<long>(type: "bigint", nullable: false),
                    DirectX = table.Column<byte>(type: "tinyint", nullable: false),
                    MinGpu = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    MaxGpu = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    SupportedOses = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ExtraInfo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SupportedVoiceLanguages = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupportedTextLanguages = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameSystemRequirements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameSystemRequirements_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLibraries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLibraries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLibraries_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeatureGameFilter",
                columns: table => new
                {
                    FeaturesId = table.Column<int>(type: "int", nullable: false),
                    GameFiltersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureGameFilter", x => new { x.FeaturesId, x.GameFiltersId });
                    table.ForeignKey(
                        name: "FK_FeatureGameFilter_Features_FeaturesId",
                        column: x => x.FeaturesId,
                        principalTable: "Features",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeatureGameFilter_GameFilters_GameFiltersId",
                        column: x => x.GameFiltersId,
                        principalTable: "GameFilters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameFilterGenre",
                columns: table => new
                {
                    GameFiltersId = table.Column<int>(type: "int", nullable: false),
                    GenresId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameFilterGenre", x => new { x.GameFiltersId, x.GenresId });
                    table.ForeignKey(
                        name: "FK_GameFilterGenre_GameFilters_GameFiltersId",
                        column: x => x.GameFiltersId,
                        principalTable: "GameFilters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameFilterGenre_Genres_GenresId",
                        column: x => x.GenresId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLibraryGames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserLibraryId = table.Column<int>(type: "int", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    HoursPlayed = table.Column<long>(type: "bigint", nullable: false),
                    LastPlayed = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsFavourite = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLibraryGames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLibraryGames_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserLibraryGames_UserLibraries_UserLibraryId",
                        column: x => x.UserLibraryId,
                        principalTable: "UserLibraries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeatureGameFilter_GameFiltersId",
                table: "FeatureGameFilter",
                column: "GameFiltersId");

            migrationBuilder.CreateIndex(
                name: "IX_GameFilterGenre_GenresId",
                table: "GameFilterGenre",
                column: "GenresId");

            migrationBuilder.CreateIndex(
                name: "IX_GameFilters_GameId",
                table: "GameFilters",
                column: "GameId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameInfos_GameId",
                table: "GameInfos",
                column: "GameId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameMedias_GameId",
                table: "GameMedias",
                column: "GameId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameSystemRequirements_GameId",
                table: "GameSystemRequirements",
                column: "GameId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserLibraries_UserId",
                table: "UserLibraries",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserLibraryGames_GameId",
                table: "UserLibraryGames",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLibraryGames_UserLibraryId",
                table: "UserLibraryGames",
                column: "UserLibraryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeatureGameFilter");

            migrationBuilder.DropTable(
                name: "GameFilterGenre");

            migrationBuilder.DropTable(
                name: "GameInfos");

            migrationBuilder.DropTable(
                name: "GameMedias");

            migrationBuilder.DropTable(
                name: "GameSystemRequirements");

            migrationBuilder.DropTable(
                name: "PriceCategories");

            migrationBuilder.DropTable(
                name: "UserLibraryGames");

            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropTable(
                name: "GameFilters");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "UserLibraries");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
