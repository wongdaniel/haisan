using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using haisan.util;
using haisan.domain;

namespace haisan.dao
{
    interface EmployeeDao
    {
        DataSet getAllEmployee(string queryStr);
        MessageLocal saveOrUpdateEmployee(Employee employee);
        Employee getEmployeeById(int id);
    }
}
