using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace K21CNT2_2110900086_DATN.Helper
{
    public static class Utilities
    {
        public static bool IsValidEmail(string email)
        {
            if (email.Trim().EndsWith("."))
            {
                return false; // suggested by @TK-421
            }

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static string GetRandomKey(int length = 5)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static async Task<string> UploadFile(IFormFile file, string sDirectory, string newName)
        {
            try
            {
                if (file == null || file.Length == 0)
                    throw new ArgumentException("File không hợp lệ.");

                if (string.IsNullOrWhiteSpace(newName))
                    newName = Path.GetFileName(file.FileName);

                string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", sDirectory);
                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                string filePath = Path.Combine(uploadPath, newName);

                var supportedTypes = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                string fileExt = Path.GetExtension(file.FileName).ToLower();

                if (!supportedTypes.Contains(fileExt))
                    throw new InvalidOperationException("Định dạng file không được hỗ trợ.");

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return newName;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi tải file: {ex.Message}");
                return null;
            }
        }
    }
}
