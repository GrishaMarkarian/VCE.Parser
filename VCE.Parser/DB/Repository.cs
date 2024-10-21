
using Microsoft.Data.SqlClient;
using VCE.Parser.Models;

namespace VCE.Parser.DB;

public class Repository
{
    private readonly string connectionString = "Server=DESKTOP-A8MRQGF;Database=local_db;Trusted_Connection=True;TrustServerCertificate=True;";

    public async Task SavePartsAsync(List<Part> parts)
    {
        //string insertQuery = @"
        //    INSERT INTO local_db.dbo.Part (Title, Type, Name, Manufacturer, Price, AnaloguePartsString, CarsString) 
        //    VALUES (@Title, @Type, @Name, @Manufacturer, @Price, @AnaloguePartsString, @CarsString)";

        //try
        //{
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        await connection.OpenAsync();

        //        using (SqlTransaction transaction = connection.BeginTransaction())
        //        {
        //            using (SqlCommand command = new SqlCommand(insertQuery, connection, transaction))
        //            {
        //                command.Parameters.Add("@Title", System.Data.SqlDbType.NVarChar);
        //                command.Parameters.Add("@Type", System.Data.SqlDbType.NVarChar);
        //                command.Parameters.Add("@Name", System.Data.SqlDbType.NVarChar);
        //                command.Parameters.Add("@Manufacturer", System.Data.SqlDbType.NVarChar);
        //                command.Parameters.Add("@Price", System.Data.SqlDbType.NVarChar);
        //                command.Parameters.Add("@AnaloguePartsString", System.Data.SqlDbType.Text);
        //                command.Parameters.Add("@CarsString", System.Data.SqlDbType.Text);

        //                foreach (var part in parts)
        //                {
        //                    command.Parameters["@Title"].Value = part.Title;
        //                    command.Parameters["@Type"].Value = part.Type;
        //                    command.Parameters["@Name"].Value = part.Name;
        //                    command.Parameters["@Manufacturer"].Value = part.Manufacturer;
        //                    command.Parameters["@Price"].Value = part.Price;
        //                    command.Parameters["@AnaloguePartsString"].Value = part.AnaloguePartsString;
        //                    command.Parameters["@CarsString"].Value = part.CarsString;

        //                    await command.ExecuteNonQueryAsync();
        //                }

        //                await transaction.CommitAsync();
        //            }
        //        }

        //        Console.WriteLine($"Save DB");
        //        File.AppendAllText("C:\\Users\\Григорий\\Source\\Repos\\VCE.Parser\\VCE.Parser\\Data\\logs.txt", $"Save DB" + Environment.NewLine);
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine($"Error: {ex.Message}");
        //}
    }
}