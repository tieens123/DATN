using System.Text.RegularExpressions;

namespace K21CNT2_2110900086_DATN.Extension
{
    public static class Extension
    {
        public static string ToVnd(this double donGia)
        {
            return donGia.ToString("#,##0") + " đ";
        }

        public static string ToTitleCase(string str)
        {
            if (string.IsNullOrEmpty(str)) return str;

            var words = str.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                if (!string.IsNullOrEmpty(words[i]))
                {
                    words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1).ToLower();
                }
            }
            return string.Join(" ", words);
        }

        public static string ToUrlFriendly(this string url)
        {
            if (string.IsNullOrEmpty(url)) return string.Empty;

            url = url.ToLower().Trim();

            // Chuẩn hóa dấu tiếng Việt
            url = Regex.Replace(url, "[áàạảãâấầậẩẫăắằặẳẵ]", "a");
            url = Regex.Replace(url, "[éèẹẻẽêếềệểễ]", "e");
            url = Regex.Replace(url, "[óòọỏõôốồộổỗơớờợởỡ]", "o");
            url = Regex.Replace(url, "[úùụủũưứừựửữ]", "u");
            url = Regex.Replace(url, "[íìịỉĩ]", "i");
            url = Regex.Replace(url, "[ýỳỵỷỹ]", "y");
            url = Regex.Replace(url, "[đ]", "d");

            // Loại bỏ ký tự không hợp lệ
            url = Regex.Replace(url, "[^a-z0-9-]", "-");
            url = Regex.Replace(url, "-{2,}", "-"); // Xóa dấu '-' dư thừa liên tiếp

            return url.Trim('-'); // Xóa dấu '-' ở đầu và cuối chuỗi
        }
    }
}