using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyWebApp.Models;

namespace MyWebApp.Controllers
{
    public class StudentController : Controller
    {
        private static List<Student> listStudents = new List<Student>(); // Thêm static

        public StudentController()
        {
            // Chỉ khởi tạo nếu danh sách rỗng (tránh mất data khi refresh)
            if (!listStudents.Any())
            {
                // Tạo danh sách sinh viên với 4 dữ liệu mẫu
                listStudents = new List<Student>()
                {
                    new Student() {
                        Id = 101, Name = "Hải Nam", Branch = Branch.IT,
                        Gender = Gender.Male, IsRegular = true,
                        Address = "A1-2018", Email = "nam@g.com",
                        DateOfBorth = new DateTime(2000, 1, 1),
                        ProfileImage = "default_avatar.png" // Thêm ảnh mặc định
                    },
                    new Student() {
                        Id = 102, Name = "Minh Tú", Branch = Branch.BE,
                        Gender = Gender.Female, IsRegular = true,
                        Address = "A1-2019", Email = "tu@g.com",
                        DateOfBorth = new DateTime(2000, 2, 1),
                        ProfileImage = "default_avatar.png"
                    },
                    new Student() {
                        Id = 103, Name = "Hoàng Phong", Branch = Branch.CE,
                        Gender = Gender.Male, IsRegular = false,
                        Address = "A1-2020", Email = "phong@g.com",
                        DateOfBorth = new DateTime(2000, 3, 1),
                        ProfileImage = "default_avatar.png"
                    },
                    new Student() {
                        Id = 104, Name = "Xuân Mai", Branch = Branch.EE,
                        Gender = Gender.Female, IsRegular = false,
                        Address = "A1-2021", Email = "mai@g.com",
                        DateOfBorth = new DateTime(2000, 4, 1),
                        ProfileImage = "default_avatar.png"
                    }
                };
            }
        }

        public IActionResult Index()
        {
            return View(listStudents);
        }

        [HttpGet]
        public IActionResult Create(Student s)
        {
            if(ModelState.IsValid)
            {
                s.Id = listStudents.Last().Id + 1;
                listStudents.Add(s);
                return View("Index", listStudents); // Sửa thành Redirect
            }
            ViewBag.AllGenders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();
            ViewBag.AllBranches = new List<SelectListItem>()
            {
                new SelectListItem { Text = "IT", Value = "IT" },
                new SelectListItem { Text = "Kinh tế", Value = "BE" },
                new SelectListItem { Text = "Công trình", Value = "CE" },
                new SelectListItem { Text = "Điện - Điện tử", Value = "EE" }
            };
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student s, IFormFile profileImage) // Thêm IFormFile
        {
            if (ModelState.IsValid)
            {
                // XỬ LÝ UPLOAD ẢNH
                if (profileImage != null && profileImage.Length > 0)
                {
                    // Tạo tên file duy nhất
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(profileImage.FileName);
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "profiles");

                    // Đảm bảo thư mục tồn tại
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var filePath = Path.Combine(uploadsFolder, fileName);

                    // Lưu file
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await profileImage.CopyToAsync(stream);
                    }

                    // Lưu thông tin ảnh vào student
                    s.ProfileImage = fileName;
                }
                else
                {
                    // Ảnh mặc định nếu không upload
                    s.ProfileImage = "default_avatar.png";
                }

                s.Id = listStudents.Last().Id + 1;
                listStudents.Add(s);
                return RedirectToAction("Index"); // Sửa thành Redirect
            }

            // Nếu có lỗi validation, load lại form với data
            ViewBag.AllGenders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();
            ViewBag.AllBranches = new List<SelectListItem>()
            {
                new SelectListItem { Text = "IT", Value = "IT" },
                new SelectListItem { Text = "Kinh tế", Value = "BE" },
                new SelectListItem { Text = "Công trình", Value = "CE" },
                new SelectListItem { Text = "Điện - Điện tử", Value = "EE" }
            };
            return View(s);
        }

        // ACTION ĐỂ HIỂN THỊ ẢNH
        public IActionResult GetImage(string fileName)
        {
            if (string.IsNullOrEmpty(fileName) || fileName == "default_avatar.png")
            {
                // Trả về ảnh mặc định từ wwwroot/images
                var defaultImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "default_avatar.png");
                if (System.IO.File.Exists(defaultImagePath))
                {
                    return PhysicalFile(defaultImagePath, "image/png");
                }
                return NotFound();
            }

            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "profiles", fileName);
            if (System.IO.File.Exists(imagePath))
            {
                var contentType = "image/jpeg";
                if (fileName.EndsWith(".png")) contentType = "image/png";
                else if (fileName.EndsWith(".gif")) contentType = "image/gif";

                return PhysicalFile(imagePath, contentType);
            }

            return NotFound();
        }
    }
}