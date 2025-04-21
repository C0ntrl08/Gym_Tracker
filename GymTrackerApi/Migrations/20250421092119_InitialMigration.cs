using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GymTrackerApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "applicationuser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmailAddress = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HashedPassword = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicationuser", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BaseImage = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LongDescription = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VideoUrl = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Difficulty = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Trainings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ApplicationUserId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trainings_applicationuser_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "applicationuser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TrainingExercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TrainingId = table.Column<int>(type: "int", nullable: false),
                    ExerciseId = table.Column<int>(type: "int", nullable: false),
                    Sets = table.Column<int>(type: "int", nullable: false),
                    Repetitions = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<float>(type: "float", nullable: true),
                    DurationMinutes = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingExercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingExercises_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainingExercises_Trainings_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "Trainings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Exercises",
                columns: new[] { "Id", "BaseImage", "Category", "Description", "Difficulty", "LongDescription", "Name", "VideoUrl" },
                values: new object[,]
                {
                    { 1, "pullup.png", "Bodyweight Exercises", "A basic upper-body exercise targeting chest, shoulders, and triceps.", 0, "Push-ups are a classic exercise to build upper-body strength. They primarily target the chest, shoulders, and triceps, and also engage the core muscles. To perform a push-up, start in a plank position, keep your body straight, and lower yourself until your chest almost touches the floor. Then push back up.", "Push-up", "https://www.youtube.com/watch?v=Eh00_rniF8E&t=0s" },
                    { 2, "pullup.png", "Bodyweight Exercises", "Strengthens the core and stabilizes the spine.", 1, "Plank is lorem ipsum", "Plank", "https://www.youtube.com/watch?v=u6ZelKyUM6g" },
                    { 3, "pullup.png", "Bodyweight Exercises", "Engages lower body muscles, focusing on quads and glutes.", 2, "", "Squat", "" },
                    { 4, "pullup.png", "Bodyweight Exercises", "Builds upper-body strength, mainly in the back and biceps.", 0, "", "Pull-up", "" },
                    { 5, "pullup.png", "Bodyweight Exercises", "Targets triceps, chest, and shoulders.", 0, "", "Dips", "" },
                    { 6, "pullup.png", "Bodyweight Exercises", "Focuses on core strength and lower abdominal muscles.", 0, "", "Leg Raises", "" },
                    { 7, "pullup.png", "Bodyweight Exercises", "A dynamic core and cardio exercise mimicking climbing motion.", 0, "", "Mountain Climbers", "" },
                    { 8, "pullup.png", "Bodyweight Exercises", "An advanced core isometric exercise.", 0, "", "L-Sit", "" },
                    { 9, "pullup.png", "Bodyweight Exercises", "Builds shoulder and core strength with balance training.", 0, "", "Handstand", "" },
                    { 10, "pullup.png", "Bodyweight Exercises", "Static exercise engaging the quads and core.", 0, "", "Wall Sit", "" },
                    { 11, "pullup.png", "Bodyweight Exercises", "Strengthens lower back and core while mimicking flight.", 0, "", "Superman Hold", "" },
                    { 12, "dumbbell.png", "Weight Training Exercises", "Develops chest, shoulders, and triceps with heavy resistance.", 0, "", "Bench Press", "" },
                    { 13, "dumbbell.png", "Weight Training Exercises", "A compound exercise targeting back, legs, and glutes.", 0, "", "Deadlift", "" },
                    { 14, "dumbbell.png", "Weight Training Exercises", "Builds leg and core strength with loaded resistance.", 0, "", "Barbell Squat", "" },
                    { 15, "dumbbell.png", "Weight Training Exercises", "Strengthens shoulders and upper arms.", 0, "", "Shoulder Press", "" },
                    { 16, "dumbbell.png", "Weight Training Exercises", "Targets back muscles, mimicking pull-up motion.", 0, "", "Cable Pulldown", "" },
                    { 17, "dumbbell.png", "Weight Training Exercises", "Isolates and builds the biceps.", 0, "", "Bicep Curl", "" },
                    { 18, "dumbbell.png", "Weight Training Exercises", "Isolates the triceps using a cable machine.", 0, "", "Tricep Pushdown", "" },
                    { 19, "dumbbell.png", "Weight Training Exercises", "Targets quads, hamstrings, and glutes.", 0, "", "Leg Press", "" },
                    { 20, "dumbbell.png", "Weight Training Exercises", "Focuses on the upper chest and shoulders.", 0, "", "Incline Dumbbell Press", "" },
                    { 21, "dumbbell.png", "Weight Training Exercises", "Strengthens the back and improves posture.", 0, "", "Lat Pulldown", "" },
                    { 22, "dumbbell.png", "Weight Training Exercises", "Enhances shoulder stability and targets rear delts.", 0, "", "Face Pull", "" },
                    { 23, "dumbbell.png", "Weight Training Exercises", "Develops glutes with emphasis on hip extension.", 0, "", "Hip Thrust", "" },
                    { 24, "running.png", "Aerobic Exercises", "A full-body exercise improving cardio and strength.", 0, "", "Burpee", "" },
                    { 25, "running.png", "Aerobic Exercises", "A dynamic cardio exercise.", 0, "", "Jumping Jacks", "" },
                    { 26, "running.png", "Aerobic Exercises", "Improves cardio and core strength.", 0, "", "Mountain Climbers", "" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrainingExercises_ExerciseId",
                table: "TrainingExercises",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingExercises_TrainingId",
                table: "TrainingExercises",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_ApplicationUserId",
                table: "Trainings",
                column: "ApplicationUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrainingExercises");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "Trainings");

            migrationBuilder.DropTable(
                name: "applicationuser");
        }
    }
}
