using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using TodoApp.Models;
using Microsoft.Extensions.Options;

namespace TodoApp.Factory
{
    public class TodoFactory
    {
        private MySqlOptions _options;
        public TodoFactory(IOptions<MySqlOptions> config)
        {
            _options = config.Value;
        }
        internal IDbConnection Connection
        {
            get 
            {
                return new MySqlConnection(_options.ConnectionString);
            }
        }

        public void CreateTodo(Todo todo)
        {
            using(IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var query = @"
                    INSERT INTO todos (title, description, due_date, created_at, updated_at) 
                    VALUES (@title, @description, @due_date, NOW(), NOW())
                ";
                dbConnection.Execute(query, todo);
            }
        }
        public IEnumerable<Todo> FindAllTodos()
        {
            using(IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Todo>("SELECT * FROM todos");
            }
        }
    }
}