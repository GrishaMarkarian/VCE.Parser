using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VCE.ParserCars.Models;

namespace VCE.ParserCars.Data
{
    public class SQLRepository
    {
        private readonly string connectionString = "Server=DESKTOP-A8MRQGF;Database=local_db;Trusted_Connection=True;TrustServerCertificate=True;";

        public async Task SaveCarsAsync(List<Cars> cars)
        {
            string insertQuery = @"
        INSERT INTO local_db.dbo.Cars (Brand, Model, BodyType, EngineCapacity, ModifyType, CarYear) 
        VALUES (@Brand, @Model, @BodyType, @EngineCapacity, @ModifyType, @CarYear)";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    foreach (var car in cars)
                    {
                        using (SqlCommand command = new SqlCommand(insertQuery, connection))
                        {
                            command.Parameters.Add("@Brand", System.Data.SqlDbType.NVarChar).Value = car.Brand;
                            command.Parameters.Add("@Model", System.Data.SqlDbType.NVarChar).Value = car.Model;
                            command.Parameters.Add("@BodyType", System.Data.SqlDbType.NVarChar).Value = car.BodyType;
                            command.Parameters.Add("@EngineCapacity", System.Data.SqlDbType.NVarChar).Value = car.EngineCapacity;
                            command.Parameters.Add("@ModifyType", System.Data.SqlDbType.NVarChar).Value = car.Modify;
                            command.Parameters.Add("@CarYear", System.Data.SqlDbType.Int).Value = car.Year;

                            try
                            {
                                await command.ExecuteNonQueryAsync();
                            }
                            catch (Exception insertEx)
                            {
                                Console.WriteLine($"Error inserting car {car.Model}: {insertEx.Message}");
                                File.AppendAllText("C:\\Users\\Григорий\\Source\\Repos\\VCE.Parser\\VCE.Parser\\Data\\logs.txt", $"Error inserting car {car.Model}: {insertEx.Message}" + Environment.NewLine);
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
}
