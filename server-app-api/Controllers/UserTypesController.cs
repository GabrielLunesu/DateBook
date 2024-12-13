using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DatingApp.Data;
using DatingApp.Models;
using DatingApp.DTOs;

namespace dating_app_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTypesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/UserTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserTypeDTO>>> GetUserTypes()
        {
            return await _context.UserTypes
                .Select(ut => new UserTypeDTO
                {
                    TypeId = ut.TypeId,
                    Name = ut.Name
                })
                .ToListAsync();
        }

        // GET: api/UserTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserTypeDTO>> GetUserType(int id)
        {
            var userType = await _context.UserTypes.FindAsync(id);

            if (userType == null)
            {
                return NotFound();
            }

            return new UserTypeDTO
            {
                TypeId = userType.TypeId,
                Name = userType.Name
            };
        }
    }
}
