using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperApp
{
    [RankColumn]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [MemoryDiagnoser]
    public class DatabaseOperations
    {

        UserDBContext db;
        public DatabaseOperations()
        {
            db = new UserDBContext();     
        }
        [Benchmark(Baseline = true)]
        public async Task<List<Person>> GetPersons()
        {
            return await db.Persons.ToListAsync();
        }

        [Benchmark]
        public async Task<List<Person>> GetPersonsWithDapper()
        {
            string query = $"Select   [Name],[Email],[Gender],[DOB],[Category],[IsActive] from [dbo].[Persons]";
            SqlConnection connection = new SqlConnection(Infra.connectionString);
            var result = await connection.QueryAsync<Person>(query);
            return result.ToList();
        }
        [Benchmark]
        public async Task<List<Person>> GetPersonsWithRawSql()
        {
            FormattableString query = $"Select [Id], [Name],[Email],[Gender],[DOB],[JoinTime],[Category],[IsActive] from [dbo].[Persons]"; ;
            return await db.Database.SqlQuery<Person>(query).ToListAsync();
        }

    }
}
