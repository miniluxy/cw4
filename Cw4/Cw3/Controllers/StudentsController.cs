using System;
using Microsoft.AspNetCore.Mvc;
using Cw4.Models;
using System.Data.SqlClient;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cw4.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        [HttpGet("sql/get")]
        public IActionResult GetStudents(string id)
        {

            using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s19263;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = $"select Student.FirstName, Student.LastName, Enrollment.Semester from Student inner join Enrollment on Student.IdEnrollment = Enrollment.IdEnrollment where Student.IndexNumber =@index";
                com.Parameters.AddWithValue("index", id);

                con.Open();
                var dr = com.ExecuteReader();

                while (dr.Read())
                {
                    var st = new Student { IndexNumber = id, FirstName = dr["FirstName"].ToString(), LastName = dr["LastName"].ToString() };
                    return Ok($"{dr.HasRows} {st.ToString()} {dr["Semester"].ToString()} ");
                }
                return NotFound();
            }

            //return Ok(_dbService.GetStudents());

        }
    
 /*       
         [HttpGet("sql")]
          public IActionResult GetStudents(string id)
          {
              using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s19263;Integrated Security=True"))
              using (var com = new SqlCommand())
              {
                 com.Connection = con;
                 //com.CommandText = $"select * from Author where name='{name}'";
                 com.CommandText = $"select Student.FirstName, Student.LastName, (Studies.Name)as Studies from Student " +
                     $"inner join Enrollment on Student.IdEnrollment = Enrollment.IdEnrollment " +
                     $"inner join Studies on Enrollment.IdEnrollment = Studies.IdStudy" +
                     $"where Student.IndexNumber=@index;";
                 com.Parameters.AddWithValue("index", id);
                 con.Open();
                 var dr = com.ExecuteReader();
                   while (dr.Read())
                   {
                       var st = new Student { IndexNumber = dr["IndexNumber"].ToString(), FirstName = dr["FirstName"].ToString(), LastName = dr["LastName"].ToString() };
                       return Ok($"{dr.HasRows} {st.ToString()}");
                   }
                   return Ok(dr.HasRows);
              }
              //return Ok(_dbService.GetStudents());
          }


        [HttpPost("sql/crt")]
        public IActionResult CreateStudent(Student student)
        {
            using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s19263;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "Insert into Student values (@index,@Fname,@Lname,@Date,@Eroll) ";
                com.Parameters.AddWithValue("index", student.IndexNumber);
                com.Parameters.AddWithValue("Fname", student.FirstName);
                com.Parameters.AddWithValue("Lname", student.LastName);
                com.Parameters.AddWithValue("Date", student.BirthDate);
                com.Parameters.AddWithValue("Eroll", student.IdEnrollment);

                con.Open();
                com.ExecuteNonQuery();
            }
            return StatusCode((int)System.Net.HttpStatusCode.Created);
        }
*/
        [HttpPut("{id}")]
        public IActionResult PutStudetn(int id)
        {

            return Ok("Aktualizacja dokonczona");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {

            return Ok("Usuwanie ukonczone");
        }

    }

}
