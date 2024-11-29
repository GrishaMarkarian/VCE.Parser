
using Microsoft.Data.SqlClient;
using VCE.Parser.Models;

namespace VCE.Parser.DB;

public class Repository
{
    private readonly string connectionString = "Server=DESKTOP-A8MRQGF;Database=local_db;Trusted_Connection=True;TrustServerCertificate=True;";

    public async Task SavePartsAsync(List<Part> parts)
    {
        string insertQuery = @"
    INSERT INTO local_db.dbo.Parts_V2 (Title, Type, Name, Manufacturer, Price, AnaloguePartsString, CarsString) 
    VALUES (@Title, @Type, @Name, @Manufacturer, @Price, @AnaloguePartsString, @CarsString)";

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                foreach (var part in parts)
                {
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.Add("@Title", System.Data.SqlDbType.NVarChar).Value = part.Title;
                        command.Parameters.Add("@Type", System.Data.SqlDbType.NVarChar).Value = part.Type;
                        command.Parameters.Add("@Name", System.Data.SqlDbType.NVarChar).Value = part.Name;
                        command.Parameters.Add("@Manufacturer", System.Data.SqlDbType.NVarChar).Value = part.Manufacturer;
                        command.Parameters.Add("@Price", System.Data.SqlDbType.NVarChar).Value = part.Price;
                        command.Parameters.Add("@AnaloguePartsString", System.Data.SqlDbType.Text).Value = part.AnaloguePartsString;
                        command.Parameters.Add("@CarsString", System.Data.SqlDbType.Text).Value = part.CarsString;

                        try
                        {
                            await command.ExecuteNonQueryAsync();
                        }
                        catch (Exception insertEx)
                        {
                            Console.WriteLine($"Error inserting part {part.Name}: {insertEx.Message}");
                            File.AppendAllText("C:\\Users\\Григорий\\Source\\Repos\\VCE.Parser\\VCE.Parser\\Data\\logs.txt", $"Error inserting part {part.Name}: {insertEx.Message}" + Environment.NewLine);
                        }
                    }
                }

                Console.WriteLine("Save DB");
                File.AppendAllText("C:\\Users\\Григорий\\Source\\Repos\\VCE.Parser\\VCE.Parser\\Data\\logs.txt", "Save DB" + Environment.NewLine);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            File.AppendAllText("C:\\Users\\Григорий\\Source\\Repos\\VCE.Parser\\VCE.Parser\\Data\\logs.txt", $"Error: {ex.Message}" + Environment.NewLine);
        }
    }
}