using System.Data;
using DTO;
using Microsoft.Data.SqlClient;
using Repositories;

namespace Repositories;

public class RepositoryImages : AbstractRepository<Image>
{
    public RepositoryImages(IConfiguration configuration) : base(configuration)
    {
    }

    public override void Insert(Image entity)
    {
        try
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = conn;
                    command.CommandText = "InsertImages";

                    command.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar)).Value = entity.Name;
                    command.Parameters.Add(new SqlParameter("@Path", SqlDbType.VarChar)).Value = entity.Path;
                    command.Parameters.Add(new SqlParameter("@LastModified", SqlDbType.VarChar)).Value = entity.LastModified;
                    command.Parameters.Add(new SqlParameter("@Created", SqlDbType.DateTime)).Value = entity.Created;
                    command.Parameters.Add(new SqlParameter("@Tag", SqlDbType.VarChar)).Value = entity.Tag;
                    command.ExecuteNonQuery();
                }
            }

        }
        catch (Exception ex)
        {
            //TODO: Logs here
            Console.WriteLine(ex.Message);
        }

    }   
}