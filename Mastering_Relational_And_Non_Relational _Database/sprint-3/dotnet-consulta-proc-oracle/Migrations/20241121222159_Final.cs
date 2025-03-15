using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LexusTech.Migrations
{
    /// <inheritdoc />
    public partial class Final : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_Alerta_Consumo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    IdUsuario = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Ambiente = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    ConsumoMinimo = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    EmailDestino = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Ativo = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Descricao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    TipoAlerta = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Alerta_Consumo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Alerta_Gerado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    IdConsumo = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Comodo = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Item = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    ConsumoDiario = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    DataConsumo = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    ConsumoDiarioAnterior = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    VariacaoConsumo = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Recomendacao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Alerta_Gerado", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Comodo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    IdUsuario = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Descricao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Comodo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Consumo",
                columns: table => new
                {
                    IdConsumo = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    IdUsuario = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Comodo = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Item = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    ConsumoDiario = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DataConsumo = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Valor = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Consumo", x => x.IdConsumo);
                });

            migrationBuilder.CreateTable(
                name: "T_Endereco",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    IdUsuario = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CEP = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Estado = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Cidade = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Bairro = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Rua = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Complemento = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Endereco", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Imagem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    IdUsuario = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    NomeImagem = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    LinkImagem = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Imagem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Item_Casa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    IdUsuario = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DescricaoItem = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Item_Casa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Login",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Email = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Senha = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Login", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_LoginLog",
                columns: table => new
                {
                    IdLogin = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    IdUsuario = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    DataHora = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_LoginLog", x => x.IdLogin);
                });

            migrationBuilder.CreateTable(
                name: "T_Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Sobrenome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Telefone = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Email = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Senha = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Usuario", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_Alerta_Consumo");

            migrationBuilder.DropTable(
                name: "T_Alerta_Gerado");

            migrationBuilder.DropTable(
                name: "T_Comodo");

            migrationBuilder.DropTable(
                name: "T_Consumo");

            migrationBuilder.DropTable(
                name: "T_Endereco");

            migrationBuilder.DropTable(
                name: "T_Imagem");

            migrationBuilder.DropTable(
                name: "T_Item_Casa");

            migrationBuilder.DropTable(
                name: "T_Login");

            migrationBuilder.DropTable(
                name: "T_LoginLog");

            migrationBuilder.DropTable(
                name: "T_Usuario");
        }
    }
}
