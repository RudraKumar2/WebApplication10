using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using Microsoft.Extensions.Configuration;
using WebApplication10.Models;
using ReadDB.Models;

namespace Postgres.Data
{
    public class DataAccess
    {
        private readonly string _connectionString;

        public DataAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<YourModel> GetData()
        {
            List<YourModel> dataList = new List<YourModel>();

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT pid, productname, price, productid FROM prices", conn))
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            YourModel data = new YourModel
                            {
                                pid = reader.GetInt32(reader.GetOrdinal("pid")),
                                productname = reader.GetString(reader.GetOrdinal("productname")),
                                price = reader.GetInt32(reader.GetOrdinal("price")),
                                productid = reader.GetInt32(reader.GetOrdinal("productid"))
                            };
                            dataList.Add(data);
                        }
                    }
                }
            }
            return dataList;
        }
    }
}