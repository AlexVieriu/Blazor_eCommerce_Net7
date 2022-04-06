using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace eShop.DataStore.SQL.Dapper.Helpers;
public class Sql : ISql
{
    private readonly IConfiguration _config;
    private const string ConnectionString = "eCommerce";

    public Sql(IConfiguration config)
    {
        _config = config;
    }

    // Single Stored Procedure
    public async Task<IEnumerable<T>> LoadData<T, U>(string procedure, U parameters)
    {
        try
        {
            var confConnection = _config.GetConnectionString(ConnectionString);

            using IDbConnection con = new SqlConnection(confConnection);
            var entities = await con.QueryAsync<T>(procedure, parameters, commandType: CommandType.StoredProcedure);

            return entities;
        }
        catch (Exception e)
        {
            throw;
        }
    }

    public async Task<int> SaveData<U>(string procedure, U parameters)
    {
        try
        {
            var confConnection = _config.GetConnectionString(ConnectionString);

            using IDbConnection con = new SqlConnection(confConnection);
            var statusQuery = await con.ExecuteAsync(procedure, parameters, commandType: CommandType.StoredProcedure);

            return statusQuery;
        }
        catch (Exception e)
        {
            throw;
        }
    }

    //// Mapping One To One
    //public async Task<T> LoadDataOneToOne<T, U>(string procedure, U parameters)
    //{
    //    try
    //    {
    //        var confConnection = _config.GetConnectionString(ConnectionString);

    //        using IDbConnection con = new SqlConnection(confConnection);

    //        //var entities = con.QueryAsync<T, U, T>(procedure,
    //        //    map:
    //        //    );

    //        reutrn 
    //    }
    //    catch (Exception)
    //    {

    //        throw;
    //    }
    //}


    // Multiple Stored Procedure

    private IDbConnection? _connection;
    private IDbTransaction? _transaction;
    private bool isClosed = false;

    public void StartTransaction()
    {
        string? connectionString = _config.GetConnectionString(ConnectionString);

        _connection = new SqlConnection(connectionString);
        _connection.Open();

        _transaction = _connection.BeginTransaction();

        isClosed = false;
    }

    public async Task<IEnumerable<T>> LoadDataTransation<T, U>(string? procedure, U parameters)
    {
        try
        {
            var entities = await _connection.QueryAsync<T>(procedure,
                                                           parameters,
                                                           commandType: CommandType.StoredProcedure,
                                                           transaction: _transaction);

            return entities;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task SaveDataTransaction<U>(string? procedure, U parameters)
    {
        try
        {
            var result = await _connection.ExecuteAsync(procedure,
                                                          parameters,
                                                          commandType: CommandType.StoredProcedure,
                                                          transaction: _transaction);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public void CommitTransaction()
    {
        _transaction?.Commit();
        _connection?.Close();

        isClosed = true;
    }

    public void RollBackTransaction()
    {
        _transaction?.Rollback();
        _connection?.Close();

        isClosed = true;
    }

    public void Dispose()
    {
        if (isClosed == false)
        {
            try
            {
                CommitTransaction();
            }
            catch (Exception)
            {
                throw;
            }
        }

        _transaction = null;
        _connection = null;
    }
}
