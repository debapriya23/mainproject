using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KOODLE.Data.FileManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthRoleBased.Controllers
{
    public class EmpController : Controller
    {
        private IFileManager _fileManager;

        public EmpController(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        [Authorize(Policy ="writepolicy")]
        public IActionResult Post()
        {
            return View();
        }
        [Authorize(Roles ="Expert")]
        public IActionResult Certified()
        {
            return View();
        }

        [HttpGet("/Image/{image}")]
        public IActionResult Image(string image)
        {
            var mine = image.Substring(image.LastIndexOf('.') + 1);
            return new FileStreamResult(_fileManager.ImageStream(image), $"image/{mine}");
        }
    }
}