using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFAereoNuvem.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNewDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adresses_Clients_Id",
                table: "Adresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_ClientStatuses_ClientStatusId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Airplanes_AirplaneId",
                table: "Flights");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Armchairs_ArmchairId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Scales_Airports_AirportId",
                table: "Scales");

            migrationBuilder.DropIndex(
                name: "IX_Clients_CurrentAdressId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_FutureAdressId",
                table: "Clients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Scales",
                table: "Scales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClientStatuses",
                table: "ClientStatuses");

            migrationBuilder.DropColumn(
                name: "SeatId",
                table: "Reservations");

            migrationBuilder.RenameTable(
                name: "Scales",
                newName: "Scale");

            migrationBuilder.RenameTable(
                name: "ClientStatuses",
                newName: "ClientStatus");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Airports",
                newName: "AirportsName");

            migrationBuilder.RenameColumn(
                name: "AirportId",
                table: "Scale",
                newName: "FlightId");

            migrationBuilder.RenameIndex(
                name: "IX_Scales_AirportId",
                table: "Scale",
                newName: "IX_Scale_FlightId");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Reservations",
                type: "float",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateReservation",
                table: "Reservations",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<Guid>(
                name: "CodeRersevation",
                table: "Reservations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RealDeparture",
                table: "Flights",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RealArrival",
                table: "Flights",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Origin",
                table: "Flights",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<double>(
                name: "Duration",
                table: "Flights",
                type: "float",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<string>(
                name: "Destination",
                table: "Flights",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Departure",
                table: "Flights",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateFlight",
                table: "Flights",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "CodeFlight",
                table: "Flights",
                type: "varchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Arrival",
                table: "Flights",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<bool>(
                name: "ExistScale",
                table: "Flights",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Clients",
                type: "varchar(13)",
                maxLength: 13,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Clients",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Cpf",
                table: "Clients",
                type: "varchar(11)",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Side",
                table: "Armchairs",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<string>(
                name: "IATA",
                table: "Airports",
                type: "varchar(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Airplanes",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Prefix",
                table: "Airplanes",
                type: "varchar(5)",
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            // CORREÇÃO: Processo para alterar a coluna Id da tabela Adresses
            // 1. Remover todas as foreign keys que referenciam Adresses
            migrationBuilder.DropForeignKey(
                name: "FK_Airports_Adresses_AdressId",
                table: "Airports");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Adresses_CurrentAdressId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Adresses_FutureAdressId",
                table: "Clients");

            // 2. Criar tabela temporária com a nova configuração
            migrationBuilder.CreateTable(
                name: "Adresses_temp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Street = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Number = table.Column<string>(type: "varchar(10)", nullable: true),
                    Complement = table.Column<string>(type: "varchar(50)", nullable: true),
                    City = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    State = table.Column<string>(type: "char(2)", maxLength: 2, nullable: false),
                    Country = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Cep = table.Column<string>(type: "varchar(9)", maxLength: 9, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adresses_temp", x => x.Id);
                });

            // 3. Copiar dados da tabela antiga para a nova
            migrationBuilder.Sql(@"
                SET IDENTITY_INSERT Adresses_temp ON;
                INSERT INTO Adresses_temp (Id, Street, Number, Complement, City, State, Country, Cep)
                SELECT Id, Street, Number, Complement, City, State, Country, Cep FROM Adresses;
                SET IDENTITY_INSERT Adresses_temp OFF;
            ");

            // 4. Dropar a tabela antiga
            migrationBuilder.DropTable(name: "Adresses");

            // 5. Renomear a tabela temporária para o nome original
            migrationBuilder.RenameTable(
                name: "Adresses_temp",
                newName: "Adresses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Scale",
                table: "Scale",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClientStatus",
                table: "ClientStatus",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_Cpf",
                table: "Clients",
                column: "Cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_CurrentAdressId",
                table: "Clients",
                column: "CurrentAdressId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_FutureAdressId",
                table: "Clients",
                column: "FutureAdressId");

            // 6. Recriar as foreign keys
            migrationBuilder.AddForeignKey(
                name: "FK_Airports_Adresses_AdressId",
                table: "Airports",
                column: "AdressId",
                principalTable: "Adresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Adresses_CurrentAdressId",
                table: "Clients",
                column: "CurrentAdressId",
                principalTable: "Adresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Adresses_FutureAdressId",
                table: "Clients",
                column: "FutureAdressId",
                principalTable: "Adresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_ClientStatus_ClientStatusId",
                table: "Clients",
                column: "ClientStatusId",
                principalTable: "ClientStatus",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Airplanes_AirplaneId",
                table: "Flights",
                column: "AirplaneId",
                principalTable: "Airplanes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Armchairs_ArmchairId",
                table: "Reservations",
                column: "ArmchairId",
                principalTable: "Armchairs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Scale_Flights_FlightId",
                table: "Scale",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Airports_Adresses_AdressId",
                table: "Airports");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Adresses_CurrentAdressId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Adresses_FutureAdressId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_ClientStatus_ClientStatusId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Airplanes_AirplaneId",
                table: "Flights");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Armchairs_ArmchairId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Scale_Flights_FlightId",
                table: "Scale");

            migrationBuilder.DropIndex(
                name: "IX_Clients_Cpf",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_CurrentAdressId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_FutureAdressId",
                table: "Clients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Scale",
                table: "Scale");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClientStatus",
                table: "ClientStatus");

            migrationBuilder.DropColumn(
                name: "ExistScale",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "IATA",
                table: "Airports");

            migrationBuilder.RenameTable(
                name: "Scale",
                newName: "Scales");

            migrationBuilder.RenameTable(
                name: "ClientStatus",
                newName: "ClientStatuses");

            migrationBuilder.RenameColumn(
                name: "AirportsName",
                table: "Airports",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "FlightId",
                table: "Scales",
                newName: "AirportId");

            migrationBuilder.RenameIndex(
                name: "IX_Scale_FlightId",
                table: "Scales",
                newName: "IX_Scales_AirportId");

            migrationBuilder.AlterColumn<float>(
                name: "Price",
                table: "Reservations",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateReservation",
                table: "Reservations",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<string>(
                name: "CodeRersevation",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWID()");

            migrationBuilder.AddColumn<int>(
                name: "SeatId",
                table: "Reservations",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "RealDeparture",
                table: "Flights",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RealArrival",
                table: "Flights",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<string>(
                name: "Origin",
                table: "Flights",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<float>(
                name: "Duration",
                table: "Flights",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "Destination",
                table: "Flights",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Departure",
                table: "Flights",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateFlight",
                table: "Flights",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<string>(
                name: "CodeFlight",
                table: "Flights",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Arrival",
                table: "Flights",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(13)",
                oldMaxLength: 13,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Cpf",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(11)",
                oldMaxLength: 11);

            migrationBuilder.AlterColumn<bool>(
                name: "Side",
                table: "Armchairs",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Airplanes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Prefix",
                table: "Airplanes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldMaxLength: 5);

            // CORREÇÃO: Processo reverso para a tabela Adresses
            // 1. Criar tabela temporária sem IDENTITY
            migrationBuilder.CreateTable(
                name: "Adresses_temp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Complement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cep = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adresses_temp", x => x.Id);
                });

            // 2. Copiar dados
            migrationBuilder.Sql(@"
                INSERT INTO Adresses_temp (Id, Street, Number, Complement, City, State, Country, Cep)
                SELECT Id, Street, Number, Complement, City, State, Country, Cep FROM Adresses
            ");

            // 3. Dropar tabela atual
            migrationBuilder.DropTable(name: "Adresses");

            // 4. Renomear tabela temporária
            migrationBuilder.RenameTable(
                name: "Adresses_temp",
                newName: "Adresses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Scales",
                table: "Scales",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClientStatuses",
                table: "ClientStatuses",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_CurrentAdressId",
                table: "Clients",
                column: "CurrentAdressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_FutureAdressId",
                table: "Clients",
                column: "FutureAdressId",
                unique: true,
                filter: "[FutureAdressId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Adresses_Clients_Id",
                table: "Adresses",
                column: "Id",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Airports_Adresses_AdressId",
                table: "Airports",
                column: "AdressId",
                principalTable: "Adresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Adresses_CurrentAdressId",
                table: "Clients",
                column: "CurrentAdressId",
                principalTable: "Adresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Adresses_FutureAdressId",
                table: "Clients",
                column: "FutureAdressId",
                principalTable: "Adresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_ClientStatuses_ClientStatusId",
                table: "Clients",
                column: "ClientStatusId",
                principalTable: "ClientStatuses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Airplanes_AirplaneId",
                table: "Flights",
                column: "AirplaneId",
                principalTable: "Airplanes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Armchairs_ArmchairId",
                table: "Reservations",
                column: "ArmchairId",
                principalTable: "Armchairs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Scales_Airports_AirportId",
                table: "Scales",
                column: "AirportId",
                principalTable: "Airports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}