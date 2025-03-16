using System.Data;
using LexusTech.Infrastructure.Interfaces;
using LexusTech.Models;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;

namespace LexusTech.Repositories
{
    public class ClinicaRepository : IClinicaRepository
    {
        private readonly ApplicationDbContext _context;

        public ClinicaRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Clinica> Criar(Clinica clinica)
        {
            using (var connection = (OracleConnection)_context.Database.GetDbConnection())
            using (var command = new OracleCommand("RM553472.PKG_CLINICA.INSERIR_CLINICA", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("P_NOME", OracleDbType.Varchar2).Value = clinica.Nome;
                command.Parameters.Add("P_ENDERECO", OracleDbType.Varchar2).Value = clinica.Endereco;
                command.Parameters.Add("P_TELEFONE", OracleDbType.Varchar2).Value = clinica.Telefone;
                command.Parameters.Add("P_AVALIACAO", OracleDbType.Varchar2).Value = clinica.Avaliacao;
                command.Parameters.Add("P_PRECO_MEDIO", OracleDbType.Varchar2).Value = clinica.PrecoMedio;

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
                await connection.CloseAsync();
            }

            return clinica;
        }

        public async Task<Clinica> ConsultarId(int id)
        {
            Clinica clinica = null;

            using (var connection = (OracleConnection)_context.Database.GetDbConnection())
            using (var command = new OracleCommand("RM553472.PKG_CLINICA.CONSULTAR_CLINICA_POR_ID", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("P_ID", OracleDbType.Int32).Value = id;
                command.Parameters.Add("P_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        clinica = new Clinica
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("ID_CLINICA")),
                            Nome = reader.GetString(reader.GetOrdinal("NOME")),
                            Endereco = reader.GetString(reader.GetOrdinal("ENDERECO")),
                            Telefone = reader.GetString(reader.GetOrdinal("TELEFONE")),
                            Avaliacao = reader.GetString(reader.GetOrdinal("AVALIACAO")),
                            PrecoMedio = reader.GetString(reader.GetOrdinal("PRECO_MEDIO")),
                        };
                    }
                }
                await connection.CloseAsync();
            }

            return clinica;
        }
        public async Task<List<Clinica>> ConsultarTodos()
        {
            var clinicas = new List<Clinica>();

            using (var connection = (OracleConnection)_context.Database.GetDbConnection())
            using (var command = new OracleCommand("RM553472.PKG_CLINICA.LISTAR_CLINICAS", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("P_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        clinicas.Add(new Clinica
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("ID_CLINICA")),
                            Nome = reader.GetString(reader.GetOrdinal("NOME")),
                            Endereco = reader.GetString(reader.GetOrdinal("ENDERECO")),
                            Telefone = reader.GetString(reader.GetOrdinal("Telefone")),
                            Avaliacao = reader.GetString(reader.GetOrdinal("AVALIACAO")),
                            PrecoMedio = reader.GetString(reader.GetOrdinal("PRECO_MEDIO")),
                        });
                    }
                }
                await connection.CloseAsync();
            }

            return clinicas;
        }

        public async Task<Clinica> BuscarClinica(int id)
        {
            Clinica clinica = null;

            using (var connection = (OracleConnection)_context.Database.GetDbConnection())
            using (var command = new OracleCommand("RM553472.PKG_CLINICA.BUSCAR_CLINICA", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("P_ID", OracleDbType.Int32).Value = id;
                command.Parameters.Add("P_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        clinica = new Clinica
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("ID_CLINICA")),
                            Nome = reader.GetString(reader.GetOrdinal("NOME")),
                            Endereco = reader.GetString(reader.GetOrdinal("ENDERECO")),
                            Telefone = reader.GetString(reader.GetOrdinal("Telefone")),
                            Avaliacao = reader.GetString(reader.GetOrdinal("AVALIACAO")),
                            PrecoMedio = reader.GetString(reader.GetOrdinal("PRECO_MEDIO")),
                        };
                    }
                }
                await connection.CloseAsync();
            }

            return clinica;
        }



        public async Task<Clinica> Atualizar(Clinica clinica)
        {
            using (var connection = (OracleConnection)_context.Database.GetDbConnection())
            using (var command = new OracleCommand("RM553472.PKG_CLINICA.ATUALIZAR_CLINICA", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("P_ID", OracleDbType.Int32).Value = clinica.Id;
                command.Parameters.Add("P_NOME", OracleDbType.Varchar2).Value = clinica.Nome;
                command.Parameters.Add("P_ENDERECO", OracleDbType.Varchar2).Value = clinica.Endereco;
                command.Parameters.Add("P_TELEFONE", OracleDbType.Varchar2).Value = clinica.Telefone;
                command.Parameters.Add("P_AVALIACAO", OracleDbType.Varchar2).Value = clinica.Avaliacao;
                command.Parameters.Add("P_PRECO_MEDIO", OracleDbType.Varchar2).Value = clinica.PrecoMedio;

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
                await connection.CloseAsync();
            }

            return clinica;
        }


        public async Task<Clinica> Excluir(Clinica clinica)
        {
            using (var connection = (OracleConnection)_context.Database.GetDbConnection())
            using (var command = new OracleCommand("RM553472.PKG_CLINICA.EXCLUIR_CLINICA", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("P_ID", OracleDbType.Int32).Value = clinica.Id;
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
                await connection.CloseAsync();
            }

            return clinica;
        }

    }
}
