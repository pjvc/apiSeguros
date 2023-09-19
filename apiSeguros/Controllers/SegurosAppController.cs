using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace apiSeguros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SegurosAppController : ControllerBase
    {
        private IConfiguration _configuration;

        public SegurosAppController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        [HttpGet]
        [Route("GetNotes")]
        
        public JsonResult GetNotes()
        {
            string query = "select * from usuarios";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("SegurosDbContext");
            SqlDataReader myReader;
            using (SqlConnection myCon=new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpPost]
        [Route("AddNotes")]
        public JsonResult AddNotes([FromForm] int newCedula, string newCliente, int newTelefono, int newEdad, string newApellido)
        {
            string query = "insert into usuarios values(@newCedula, @newCliente, @newTelefono, @newEdad)";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("SegurosDbContext");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@newCedula", newCedula);
                    myCommand.Parameters.AddWithValue("@newCliente", newCliente);
                    myCommand.Parameters.AddWithValue("@newTelefono", newTelefono);
                    myCommand.Parameters.AddWithValue("@newEdad", newEdad);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added Sucessfully");
        }
        [HttpDelete]
        [Route("DeleteNotes")]
        public JsonResult DeleteNotes(int id)
        {
            string query = "delete from usuarios where id=@id";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("SegurosDbContext");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Delete Sucessfully");
        }

    }
}
