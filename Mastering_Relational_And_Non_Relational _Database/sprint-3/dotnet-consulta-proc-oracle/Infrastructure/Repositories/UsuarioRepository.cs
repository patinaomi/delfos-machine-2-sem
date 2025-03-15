using System.Data;
using LexusTech.Infrastructure.Interfaces;
using LexusTech.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Oracle.ManagedDataAccess.Client;

namespace LexusTech.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Usuario> Criar(Usuario usuario)
        {
            using (var connection = (OracleConnection)_context.Database.GetDbConnection())
            using (var command = new OracleCommand("RM553472.PKG_CLIENTES.INSERIR_CLIENTE", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("P_NOME", OracleDbType.Varchar2).Value = usuario.Nome;
                command.Parameters.Add("P_SOBRENOME", OracleDbType.Varchar2).Value = usuario.Sobrenome;
                command.Parameters.Add("P_EMAIL", OracleDbType.Varchar2).Value = usuario.Email;
                command.Parameters.Add("P_TELEFONE", OracleDbType.Varchar2).Value = usuario.Telefone;
                command.Parameters.Add("P_DATA_NASCIMENTO", OracleDbType.Date).Value = usuario.DataNasc;
                command.Parameters.Add("P_ENDERECO", OracleDbType.Varchar2).Value = usuario.Endereco;

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
                await connection.CloseAsync();
            }

            return usuario;
        }

        public async Task<Usuario> ConsultarId(int id)
        {
            Usuario usuario = null;

            using (var connection = (OracleConnection)_context.Database.GetDbConnection())
            using (var command = new OracleCommand("RM553472.PKG_CLIENTES.CONSULTAR_CLIENTE_POR_ID", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("P_ID", OracleDbType.Int32).Value = id;
                command.Parameters.Add("P_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        usuario = new Usuario
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("ID_CLIENTE")),
                            Nome = reader.GetString(reader.GetOrdinal("NOME")),
                            Sobrenome = reader.GetString(reader.GetOrdinal("SOBRENOME")),
                            Email = reader.GetString(reader.GetOrdinal("EMAIL")),
                            Telefone = reader.GetString(reader.GetOrdinal("TELEFONE")),
                            DataNasc = reader.GetDateTime(reader.GetOrdinal("DATA_NASC")),
                            Endereco = reader.GetString(reader.GetOrdinal("ENDERECO"))
                        };
                    }
                }
                await connection.CloseAsync();
            }

            return usuario;
        }
        public async Task<List<Usuario>> ConsultarTodos()
        {
            var usuarios = new List<Usuario>();

            using (var connection = (OracleConnection)_context.Database.GetDbConnection())
            using (var command = new OracleCommand("RM553472.PKG_CLIENTES.LISTAR_CLIENTES", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("P_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        usuarios.Add(new Usuario
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("ID_CLIENTE")),
                            Nome = reader.GetString(reader.GetOrdinal("NOME")),
                            Sobrenome = reader.GetString(reader.GetOrdinal("SOBRENOME")),
                            Email = reader.GetString(reader.GetOrdinal("EMAIL")),
                            Telefone = reader.GetString(reader.GetOrdinal("TELEFONE")),
                            DataNasc = reader.GetDateTime(reader.GetOrdinal("DATA_NASC")),
                            Endereco = reader.GetString(reader.GetOrdinal("ENDERECO"))
                        });
                    }
                }
                await connection.CloseAsync();
            }

            return usuarios;
        }

        public async Task<Usuario> BuscarCliente(int idCliente)
        {
            Usuario usuario = null;

            using (var connection = (OracleConnection)_context.Database.GetDbConnection())
            using (var command = new OracleCommand("RM553472.PKG_CLIENTES.BUSCAR_CLIENTE", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Adicionando o parâmetro de ID para a stored procedure
                command.Parameters.Add("P_ID_CLIENTE", OracleDbType.Int32).Value = idCliente;

                // Parâmetro de saída para o cursor
                command.Parameters.Add("P_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                await connection.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    // Se encontrar o cliente, pega os dados
                    if (await reader.ReadAsync())
                    {
                        usuario = new Usuario
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("ID_CLIENTE")),
                            Nome = reader.GetString(reader.GetOrdinal("NOME")),
                            Sobrenome = reader.GetString(reader.GetOrdinal("SOBRENOME")),
                            Email = reader.GetString(reader.GetOrdinal("EMAIL")),
                            Telefone = reader.GetString(reader.GetOrdinal("TELEFONE")),
                            DataNasc = reader.GetDateTime(reader.GetOrdinal("DATA_NASC")),
                            Endereco = reader.GetString(reader.GetOrdinal("ENDERECO"))
                        };
                    }
                }
                await connection.CloseAsync();
            }

            return usuario;  // Retorna o usuário encontrado ou null
        }


        public async Task<Usuario> Atualizar(Usuario usuario)
        {
            using (var connection = (OracleConnection)_context.Database.GetDbConnection())
            using (var command = new OracleCommand("RM553472.PKG_CLIENTES.ATUALIZAR_CLIENTE", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("P_ID", OracleDbType.Int32).Value = usuario.Id;
                command.Parameters.Add("P_NOME", OracleDbType.Varchar2).Value = usuario.Nome;
                command.Parameters.Add("P_SOBRENOME", OracleDbType.Varchar2).Value = usuario.Sobrenome;
                command.Parameters.Add("P_EMAIL", OracleDbType.Varchar2).Value = usuario.Email;
                command.Parameters.Add("P_TELEFONE", OracleDbType.Varchar2).Value = usuario.Telefone;
                command.Parameters.Add("P_DATA_NASCIMENTO", OracleDbType.Date).Value = usuario.DataNasc;
                command.Parameters.Add("P_ENDERECO", OracleDbType.Varchar2).Value = usuario.Endereco;

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
                await connection.CloseAsync();
            }

            return usuario;
        }


        public async Task<Usuario> Excluir(Usuario usuario)
        {
            using (var connection = (OracleConnection)_context.Database.GetDbConnection())
            using (var command = new OracleCommand("RM553472.PKG_CLIENTES.EXCLUIR_CLIENTE", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("P_ID", OracleDbType.Int32).Value = usuario.Id;
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
                await connection.CloseAsync();
            }

            return usuario;
        }

    }
}
