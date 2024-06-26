﻿using Vibe.Domain.Employees;
using Vibe.Tools.Result;

namespace Vibe.EF.Interface
{
    public interface IEmployeeRepository
    {
        public Result SaveEmployee(EmployeeBlank blank);
        public Employee? GetEmployee(Guid id);
        public Employee? GetEmployee(String id);
        Employee[] List();
        Boolean CheckIsPasswordEquals(Guid employeeId, String password);
        Result RemoveEmployee(Guid employeeId);
    }
}
