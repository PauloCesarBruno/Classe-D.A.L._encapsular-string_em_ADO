using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;

public class DAL
{
    private static SqlConnection Connection;

    public DAL()
    {
        var configuration = GetConfiguration();
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        Connection = new SqlConnection(connectionString);
        Connection.Open();
    }

    private IConfigurationRoot GetConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        return builder.Build();
    }

    public DataTable RetDatatable(string sql)
    {
        DataTable data = new DataTable();
        SqlCommand command = new SqlCommand(sql, Connection);
        SqlDataAdapter da = new SqlDataAdapter(command);
        da.Fill(data);
        return data;
    }

    public DataTable RetDatatable(SqlCommand command)
    {
        DataTable data = new DataTable();
        command.Connection = Connection;
        SqlDataAdapter da = new SqlDataAdapter(command);
        da.Fill(data);
        return data;
    }

    public void ExecutarComandoSql(string sql)
    {
        SqlCommand command = new SqlCommand(sql, Connection);
        command.ExecuteNonQuery();
    }
}