using System.Data;
using LexusTech.Infrastructure.Interfaces;
using LexusTech.Models;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;

namespace LexusTech.Repositories
{
    public class MedicoRepository : IMedicoRepository
    {
        private readonly ApplicationDbContext _context;

        public MedicoRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Medico> Criar(Medico medico)
        {
            using (var connection = (OracleConnection)_context.Database.GetDbConnection())
            using (var command = new OracleCommand("RM553472.PKG_CLIENTES.INSERIR_DENTISTA", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("P_NOME", OracleDbType.Varchar2).Value = medico.Nome;
                command.Parameters.Add("P_SOBRENOME", OracleDbType.Varchar2).Value = medico.Sobrenome;
                command.Parameters.Add("P_TELEFONE", OracleDbType.Varchar2).Value = medico.Telefone;
                command.Parameters.Add("P_ID_CLINICA", OracleDbType.Varchar2).Value = medico.IdClinica;
                command.Parameters.Add("P_ID_ESPECIALIDADE", OracleDbType.Date).Value = medico.IdEspecialidade;
                command.Parameters.Add("P_AVALIACAO", OracleDbType.Varchar2).Value = medico.Avaliacao;

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
                await connection.CloseAsync();
            }

            return medico;
        }

        public async Task<Medico> ConsultarId(int id)
        {
            Medico medico = null;

            using (var connection = (OracleConnection)_context.Database.GetDbConnection())
            using (var command = new OracleCommand("RM553472.PKG_CLIENTES.CONSULTAR_DENTISTA_POR_ID", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("P_ID", OracleDbType.Int32).Value = id;
                command.Parameters.Add("P_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        medico = new Medico
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("ID_DENTISTA")),
                            Nome = reader.GetString(reader.GetOrdinal("NOME")),
                            Sobrenome = reader.GetString(reader.GetOrdinal("SOBRENOME")),
                            Telefone = reader.GetString(reader.GetOrdinal("TELEFONE")),
                            IdClinica = reader.GetString(reader.GetOrdinal("ID_CLINICA")),
                            IdEspecialidade = reader.GetString(reader.GetOrdinal("ID_ESPECIALIDADE")),
                            Avaliacao = reader.GetString(reader.GetOrdinal("AVALIACAO"))
                        };
                    }
                }
                await connection.CloseAsync();
            }

            return medico;
        }
        public async Task<List<Medico>> ConsultarTodos()
        {
            var medicos = new List<Medico>();

            using (var connection = (OracleConnection)_context.Database.GetDbConnection())
            using (var command = new OracleCommand("RM553472.PKG_CLIENTES.LISTAR_DENTISTAS", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("P_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        medicos.Add(new Medico
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("ID_DENTISTA")),
                            Nome = reader.GetString(reader.GetOrdinal("NOME")),
                            Sobrenome = reader.GetString(reader.GetOrdinal("SOBRENOME")),
                            Telefone = reader.GetString(reader.GetOrdinal("TELEFONE")),
                            IdClinica = reader.GetString(reader.GetOrdinal("ID_CLINICA")),
                            IdEspecialidade = reader.GetString(reader.GetOrdinal("ID_ESPECIALIDADE")),
                            Avaliacao = reader.GetString(reader.GetOrdinal("AVALIACAO"))
                        });
                    }
                }
                await connection.CloseAsync();
            }

            return medicos;
        }

        public async Task<Medico> BuscarCliente(int id)
        {
            Medico medico = null;

            using (var connection = (OracleConnection)_context.Database.GetDbConnection())
            using (var command = new OracleCommand("RM553472.PKG_CLIENTES.BUSCAR_DENTISTAS", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("P_ID", OracleDbType.Int32).Value = id;
                command.Parameters.Add("P_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        medico = new Medico
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("ID_DENTISTA")),
                            Nome = reader.GetString(reader.GetOrdinal("NOME")),
                            Sobrenome = reader.GetString(reader.GetOrdinal("SOBRENOME")),
                            Telefone = reader.GetString(reader.GetOrdinal("TELEFONE")),
                            IdClinica = reader.GetString(reader.GetOrdinal("ID_CLINICA")),
                            IdEspecialidade = reader.GetString(reader.GetOrdinal("ID_ESPECIALIDADE")),
                            Avaliacao = reader.GetString(reader.GetOrdinal("AVALIACAO"))
                        };
                    }
                }
                await connection.CloseAsync();
            }

            return medico;
        }



        public async Task<Medico> Atualizar(Medico medico)
        {
            using (var connection = (OracleConnection)_context.Database.GetDbConnection())
            using (var command = new OracleCommand("RM553472.PKG_CLIENTES.ATUALIZAR_CLIENTE", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("P_ID", OracleDbType.Int32).Value = medico.Id;
                command.Parameters.Add("P_NOME", OracleDbType.Varchar2).Value = medico.Nome;
                command.Parameters.Add("P_SOBRENOME", OracleDbType.Varchar2).Value = medico.Sobrenome;
                command.Parameters.Add("P_TELEFONE", OracleDbType.Varchar2).Value = medico.Telefone;
                command.Parameters.Add("P_ID_CLINICA", OracleDbType.Varchar2).Value = medico.IdClinica;
                command.Parameters.Add("P_ID_ESPECIALIDADE", OracleDbType.Date).Value = medico.IdEspecialidade;
                command.Parameters.Add("P_AVALIACAO", OracleDbType.Varchar2).Value = medico.Avaliacao;

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
                await connection.CloseAsync();
            }

            return medico;
        }


        public async Task<Medico> Excluir(Medico medico)
        {
            using (var connection = (OracleConnection)_context.Database.GetDbConnection())
            using (var command = new OracleCommand("RM553472.PKG_CLIENTES.EXCLUIR_DENTISTA", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("P_ID", OracleDbType.Int32).Value = medico.Id;
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
                await connection.CloseAsync();
            }

            return medico;
        }

    }
}
