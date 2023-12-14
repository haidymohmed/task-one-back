using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Data;
using task_one.Models;
namespace task_one.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        public NpgsqlConnection connection ;

        [HttpPost("AddPatient")]
        public GerneralResponse<Patient?> Add(Patient patient)
        {
            connection = new NpgsqlConnection("Server=localhost;Port=5432;Database=TaskOne;Username=postgres;Password=P@ssw0rd@Lotus;");
            connection.Open();
            string insertQuery = "INSERT INTO public.patients (name, age, gender, date) VALUES (@name, @age, @gender, @date)";
            NpgsqlCommand command = new NpgsqlCommand(insertQuery, connection);
            command.Parameters.AddWithValue("name", patient.Name);
            command.Parameters.AddWithValue("age", patient.Age);
            command.Parameters.AddWithValue("gender", patient.Gender);
            command.Parameters.AddWithValue("date", patient.BirthDate);

            try
            {
                NpgsqlDataReader reader = command.ExecuteReader();
                return new GerneralResponse<Patient?>
                {
                    Status = 1,
                    Message = "sucess",
                    Data = patient
                };
            }
            catch (Exception ex)
            {
                return new GerneralResponse<Patient?>
                {
                    Status = 2,
                    Message = patient.BirthDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),

                };
            }



        }

        [HttpDelete("delete")]
        public GerneralResponse<Patient?> Delete(int id)
        {
            connection = new NpgsqlConnection("Server=localhost;Port=5432;Database=TaskOne;Username=postgres;Password=P@ssw0rd@Lotus;");
            connection.Open();
            string deleteQuery = "DELETE FROM public.patients WHERE id = @id";
            NpgsqlCommand command = new NpgsqlCommand(deleteQuery, connection);
            command.Parameters.AddWithValue("id", id);

            try
            {
                NpgsqlDataReader reader = command.ExecuteReader();
                return new GerneralResponse<Patient?>
                {
                    Status = 1,
                    Message = "sucess",
                };
            }
            catch (Exception ex)
            {
                return new GerneralResponse<Patient?>
                {
                    Status = 2,
                    Message = ex.Message,
                };
            }
        }

        [HttpPut("EditPatient")]
        public GerneralResponse<Patient?> Edit(Patient patient)
        {
            connection = new NpgsqlConnection("Server=localhost;Port=5432;Database=TaskOne;Username=postgres;Password=P@ssw0rd@Lotus;");
            connection.Open(); string updateQuery = "UPDATE public.patients SET name = @name, age = @age, gender = @gender, date = @date WHERE id = @id";
            NpgsqlCommand command = new NpgsqlCommand(updateQuery, connection);
            command.Parameters.AddWithValue("id", patient.ID); 
            command.Parameters.AddWithValue("name", patient.Name);
            command.Parameters.AddWithValue("age", patient.Age);
            command.Parameters.AddWithValue("gender", patient.Gender);
            command.Parameters.AddWithValue("date", patient.BirthDate);

            try
            {
                NpgsqlDataReader reader = command.ExecuteReader();
                return new GerneralResponse<Patient?>
                {
                    Status = 1,
                    Message = "sucess",
                    Data = patient
                };
            }
            catch (Exception ex)
            {
                return new GerneralResponse<Patient?>
                {
                    Status = 2,
                    Message = ex.Message,

                };
            }
        }

        [HttpGet("GetPatients")]
        public IEnumerable<Patient> Get()
        {
            connection = new NpgsqlConnection("Server=localhost;Port=5432;Database=TaskOne;Username=postgres;Password=P@ssw0rd@Lotus;");
            connection.Open();
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM patients";
            NpgsqlDataReader reader = command.ExecuteReader();
            List<Patient> patients = new List<Patient>();
            while (reader.Read())
            {
                Patient patient = new Patient
                {
                    ID = reader.GetInt64("id"),
                    Name = reader.GetString("name"),
                    Age = reader.GetInt32("age"),
                    BirthDate = reader.GetDateTime("date"),
                    Gender = reader.GetInt32("gender")
                };
                patients.Add(patient);
            }
            return patients;
        }
        [HttpGet("GetPatientById")]
        public GerneralResponse<Patient?> GetById(int id)
        {
            connection = new NpgsqlConnection("Server=localhost;Port=5432;Database=TaskOne;Username=postgres;Password=P@ssw0rd@Lotus;");
            connection.Open();
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM public.patients WHERE id = " + id;
            NpgsqlDataReader reader = command.ExecuteReader();
            List<Patient> patients = new List<Patient>();
            if (reader.Read())
            {
                return new GerneralResponse<Patient?>
                {
                    Status = 1,
                    Message = "Success",
                    Data = new Patient
                    {
                        ID = reader.GetInt64("id"),
                        Name = reader.GetString("name"),
                        Age = reader.GetInt32("age"),
                        BirthDate = reader.GetDateTime("date"),
                        Gender = reader.GetInt32("gender")
                    }
                };
            }
            else{

                return new GerneralResponse<Patient?>
                {
                    Status = 2,
                    Message = "Not Found"
                };
            }
        }
    }
}
