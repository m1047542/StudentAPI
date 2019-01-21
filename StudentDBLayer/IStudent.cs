using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDBLayer
{
    public interface IStudent
    {
        int SaveStudent(string studentNumber, string firstName, string lastName, string collegeName);
        DataTable GetStudent(string StudentNumber);
    }
}
