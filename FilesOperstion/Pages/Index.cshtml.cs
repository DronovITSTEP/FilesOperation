using Microsoft.AspNetCore.Mvc.RazorPages;

using FilesOperation.Model;
using Microsoft.AspNetCore.Mvc;

namespace FilesOperation.Pages
{
    public class IndexModel : PageModel
    {
        private IWebHostEnvironment env;
        public Array userData = null;
        public char[] delimiterChar = { ',' };
        public IndexModel(IWebHostEnvironment env)
        {
            this.env = env;
        }

        public void OnGet()
        {
            var dataFile = env.WebRootPath + "/App_Data/data.txt";
            if (System.IO.File.Exists(dataFile))
                userData = System.IO.File.ReadAllLines(dataFile);
        }

        public IActionResult OnPostCreate(User user)
        {
            // строка для занесения в файл
            var result = user.FirstName + " ," + user.LastName + " ," + user.Email +
                Environment.NewLine;
            // путь к файлу
            var dataFile = env.WebRootPath + "/App_Data/data.txt";

            System.IO.File.WriteAllText(@dataFile, result);
            return RedirectToPage("");
        }

        public IActionResult OnPostAdd(User user)
        {
            // строка для занесения в файл
            var result = user.FirstName + " ," + user.LastName + " ," + user.Email +
                Environment.NewLine;
            // путь к файлу
            var dataFile = env.WebRootPath + "/App_Data/data.txt";

            System.IO.File.AppendAllText(@dataFile, result);
            return RedirectToPage("");
        }

        public void OnPostDelete(string fileName) 
        {
            var fullPath = env.WebRootPath + "/App_Data/"
                + fileName + ".txt";

            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
                TempData["Success"] = "Удаление прошло успешно";
            }
            
        }

        public void OnPostUpload(List<IFormFile> files)
        {
            foreach (var items in files)
            {
                var filePath = env.WebRootPath + "/App_Data/" + items.FileName;

                using (var stream = System.IO.File.Create(filePath))
                {
                    items.CopyTo(stream);
                }
            }
        }
    }
}