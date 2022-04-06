namespace eShop.DataStore.SQL.Dapper.Helpers;
public interface ISql
{
    Task<IEnumerable<T>> LoadData<T, U>(string procedure, U parameters);
    Task<int> SaveData<U>(string procedure, U parameters);
    void StartTransaction();
    Task<IEnumerable<T>> LoadDataTransation<T, U>(string? procedure, U parameters);
    Task SaveDataTransaction<U>(string? procedure, U parameters);
    void CommitTransaction();
    void RollBackTransaction();
}
