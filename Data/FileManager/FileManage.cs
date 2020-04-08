﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KOODLE.Data.FileManager
{
    public class FileManage : IFileManager
    {
        private string _imagePath;

        public FileManage(IConfiguration config)
        {
            _imagePath = config["Path:images"];
        }

        public FileStream ImageStream(string image)
        {
            return new FileStream(Path.Combine(_imagePath, image), FileMode.Open, FileAccess.Read);
        }

        public async Task<string> SaveImage(IFormFile image)
        {
            try
            { 
                var save_path = Path.Combine(_imagePath);
                if (Directory.Exists(save_path)) ;
                {
                    Directory.CreateDirectory(save_path);
                }
                var mine = image.FileName.Substring(image.FileName.LastIndexOf('.'));
                var fileName = $"img{DateTime.Now.ToString("dd-mm-yyyy-hh-mm-ss")}(mine)";

                using (var fileStream = new FileStream(Path.Combine(save_path, fileName), FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }
                return fileName;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "Error";
            }
        }
    }
}

