using Microsoft.AspNetCore.Mvc;
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
    public class ImagesController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;

        public ImagesController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpGet("{filename}")]
        public IActionResult GetImage(string filename)
        {
            // Update the path to use WebRootPath for wwwroot folder
            var imagePath = Path.Combine(_environment.WebRootPath, "uploads", filename);

            if (!System.IO.File.Exists(imagePath))
            {
                return NotFound();
            }

            // Get the file extension and determine the content type
            string contentType;
            string extension = Path.GetExtension(filename).ToLowerInvariant();
            switch (extension)
            {
                case ".jpg":
                case ".jpeg":
                    contentType = "image/jpeg";
                    break;
                case ".png":
                    contentType = "image/png";
                    break;
                case ".gif":
                    contentType = "image/gif";
                    break;
                case ".webp":
                    contentType = "image/webp";
                    break;
                case ".bmp":
                    contentType = "image/bmp";
                    break;
                default:
                    return BadRequest("Unsupported image format");
            }

            // Get the image file's bytes and return it with the correct content type
            var imageBytes = System.IO.File.ReadAllBytes(imagePath);
            return File(imageBytes, contentType);
        }
    }
}