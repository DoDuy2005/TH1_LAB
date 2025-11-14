using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models
{
    public class Student
    {
        public int Id { get; set; } // Mã sinh viên

        [Required(ErrorMessage = "Họ tên bắt buộc phải nhập")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Họ tên phải từ 4 đến 100 ký tự")]
        [Display(Name = "Họ tên")]
        public string? Name { get; set; } // Họ tên

        [Required(ErrorMessage = "Email bắt buộc phải nhập")]
        [RegularExpression(@"^[A-Za-z0-9._%+-]+@gmail\.com$",
        ErrorMessage = "Email phải có đuôi @gmail.com")]
        [Display(Name = "Địa chỉ email")]
        public string? Email { get; set; } // Email

        [Required(ErrorMessage = "Mật khẩu bắt buộc phải nhập")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Mật khẩu phải từ 8 ký tự trở lên")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$",
        ErrorMessage = "Mật khẩu phải gồm chữ thường, chữ hoa, chữ số và ký tự đặc biệt")]
        [Display(Name = "Mật khẩu")]
        public string? Password { get; set; } // Mật khẩu

        [Required(ErrorMessage = "Ngành học bắt buộc phải chọn")]
        [Display(Name = "Ngành học")]
        public Branch? Branch { get; set; } // Ngành học

        [Required(ErrorMessage = "Giới tính bắt buộc phải chọn")]
        [Display(Name = "Giới tính")]
        public Gender? Gender { get; set; } // Giới tính

        [Display(Name = "Hệ đào tạo (chính quy / phi chính quy)")]
        public bool IsRegular { get; set; } // Hệ: true–chính quy, false–phi chính quy

        [Required(ErrorMessage = "Địa chỉ bắt buộc phải nhập")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Địa chỉ")]
        public string? Address { get; set; } // Địa chỉ

        [Range(typeof(DateTime), "1963-01-01", "2005-12-31",
        ErrorMessage = "Ngày sinh phải trong khoảng 01/01/1963 đến 31/12/2005")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Ngày sinh bắt buộc phải nhập")]
        [Display(Name = "Ngày sinh")]
        public DateTime DateOfBorth { get; set; } // Ngày sinh

        [Required(ErrorMessage = "Điểm bắt buộc phải nhập")]
        [Range(0.0, 10.0, ErrorMessage = "Điểm phải từ 0.0 đến 10.0")]
        [Display(Name = "Điểm số")]
        public double? Score { get; set; }

        public string? ProfileImage { get; set; } = "default_avatar.png";

    }
}