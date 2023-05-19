using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI_MAM.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Diagnosis_diagnosticId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_MedicInfo_medicInfoId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_medicInfoId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_diagnosticId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "medicInfoId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "diagnosticId",
                table: "Appointments");

            migrationBuilder.AddColumn<int>(
                name: "patientId",
                table: "MedicInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "appointmentId",
                table: "Diagnosis",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MedicInfo_patientId",
                table: "MedicInfo",
                column: "patientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Diagnosis_appointmentId",
                table: "Diagnosis",
                column: "appointmentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Diagnosis_Appointments_appointmentId",
                table: "Diagnosis",
                column: "appointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicInfo_Patients_patientId",
                table: "MedicInfo",
                column: "patientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diagnosis_Appointments_appointmentId",
                table: "Diagnosis");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicInfo_Patients_patientId",
                table: "MedicInfo");

            migrationBuilder.DropIndex(
                name: "IX_MedicInfo_patientId",
                table: "MedicInfo");

            migrationBuilder.DropIndex(
                name: "IX_Diagnosis_appointmentId",
                table: "Diagnosis");

            migrationBuilder.DropColumn(
                name: "patientId",
                table: "MedicInfo");

            migrationBuilder.DropColumn(
                name: "appointmentId",
                table: "Diagnosis");

            migrationBuilder.AddColumn<int>(
                name: "medicInfoId",
                table: "Patients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "diagnosticId",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_medicInfoId",
                table: "Patients",
                column: "medicInfoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_diagnosticId",
                table: "Appointments",
                column: "diagnosticId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Diagnosis_diagnosticId",
                table: "Appointments",
                column: "diagnosticId",
                principalTable: "Diagnosis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_MedicInfo_medicInfoId",
                table: "Patients",
                column: "medicInfoId",
                principalTable: "MedicInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
