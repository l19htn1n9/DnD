using Dapper;
using DnD.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DnD.Models.Repository
{
    public class UserRepository
    {
        private IDbConnection _db;

        public UserRepository()
        {
            _db = new MySql.Data.MySqlClient.MySqlConnection("Data Source=localhost;Database=DnD;Uid=root;Pwd=Admin123;SslMode=None");
        }

        public User GetUser(int id)
        {
            _db.Open();
            
            return _db.Query<User>("SELECT Id, Username, Password FROM USERS WHERE Id=@Id", new { Id = id }).FirstOrDefault();
        }
    }
}
