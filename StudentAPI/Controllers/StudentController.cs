using StudentDBLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace StudentAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class StudentController : ApiController
    {
        private readonly IStudent student;
        public StudentController()
        {
            student = new Student();
        }
        
        [HttpGet]
        public HttpResponseMessage SaveStudent(string studentNumber, string firstName, string lastName, string collegeName)
        {
           int result = student.SaveStudent(studentNumber, firstName, lastName, collegeName);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        public HttpResponseMessage GetStudent(string studentNumber)
        {
            DataTable studentData = new DataTable();
            studentData = student.GetStudent(studentNumber);
            return Request.CreateResponse(HttpStatusCode.OK, studentData);
        }
    }
}