﻿using Microsoft.AspNetCore.Mvc;
using Vibe.Domain.Employees;
using Vibe.Domain.Infrastructure;
using Vibe.Services.Employees.Interface;
using Vibe.Services.Infrastructure.Interface;
using Vibe.Tools.Result;

namespace Vibe.BackOffice.Server.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IAuthService _authService;
        public EmployeeController(IEmployeeService employeeService, IAuthService authService) 
        {
            _employeeService = employeeService;
            _authService = authService;
        }

        [HttpPost("SaveEmployee")]
        public Result SaveEmployee([FromBody] EmployeeBlank blank)
        {
            return _employeeService.SaveEmployee(blank);
        }

        public record AuthData(String Login, String Password);
        [HttpPost("/Auth/LoginEmployee")]
        public Result<EmployeeLoginResultDTO> Login([FromBody] AuthData data)
        {
           return _employeeService.Login(data.Login, data.Password);
        }
    }
}
