using System.Data;
using DTO;
using Microsoft.Data.SqlClient;


namespace Repositories;
public class RepositoryImages<T> : IRepository<T> where T : Image, new()
{

    private readonly IConfiguration Configuration;
    private readonly string? ConnectionString;
    public RepositoryImages(IConfiguration configuration)
    {
        Configuration = configuration;
        ConnectionString = Configuration.GetConnectionString("dbTest2023");
        // $"{Configuration["ConnectionString"]}";
    }

    public void Delete(int id)
    {
        // using (var db = new SALContext())
        // {
        //     var entity = new T() { Id = id };
        //     db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
        //     db.SaveChanges();
        // }
    }

    public T GetById(int id)
    {
        // using (var db = new SALContext())
        // {
        //     return db.Set<T>().FirstOrDefault(x => x.Id == id);
        // }
        return null;
    }

    public IEnumerable<T> GetByIdPatient(int id)
    {
        // using (var db = new SALContext())
        // {
        //     var datesList = db.Set<T>().Where(x => x.IdPatient == id).ToList();
        //     return datesList;
        // }
        return null;
    }

    public IEnumerable<T> GetLists()
    {
        // using (var db = new SALContext())
        // {
        //     return db.Set<T>().ToList();
        // }static void Main(string[] args)
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                //TODO: Refactor connection & keys

                builder.DataSource = "dbTest2023.mssql.somee.com";
                builder.UserID = "xdavidgomez13_SQLLogin_1";
                builder.Password = $"{Configuration["Password"]}";
                builder.InitialCatalog = "dbTest2023";
                builder.TrustServerCertificate = true;

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    connection.Open();

                    String sql = "SELECT * FROM Images";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine("{0} {1}", reader.GetInt32(0), reader.GetString(1));
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            // Console.WriteLine("\nDone. Press enter.");
            // Console.ReadLine();
        }
        return null;
    }

    public void Insert(T entity)
    {

        using(var conn = new SqlConnection(ConnectionString)){
            conn.Open();
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.Connection = conn;
            command.CommandText = "InsertImages";

            command.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar)).Value = entity.Name;
            command.Parameters.Add(new SqlParameter("@Path", SqlDbType.VarChar)).Value = entity.Path;
            command.Parameters.Add(new SqlParameter("@LastModified", SqlDbType.VarChar)).Value = entity.LastModified;
            command.Parameters.Add(new SqlParameter("@Created", SqlDbType.DateTime)).Value = entity.Created;
            command.Parameters.Add(new SqlParameter("@Tag", SqlDbType.VarChar)).Value = entity.Tag;
            command.ExecuteNonQuery();
            conn.Close();
        }
    }

    public void Update(T entidad)
    {
        throw new NotImplementedException();
    }
}
