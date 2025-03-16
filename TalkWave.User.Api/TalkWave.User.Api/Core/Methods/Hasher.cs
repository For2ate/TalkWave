using System.Security.Cryptography;
using System.Text;

namespace TalkWave.User.Api.Core.Methods {
    
    public class Hasher {

        public static string HashPassword(string password) {

            try {

                if (password == null) throw new ArgumentNullException("password is null");

                using (SHA256 sha256Hash = SHA256.Create()) {

                    // Преобразуем строку в массив байтов
                    byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                    // Создаем строку из массива байтов
                    StringBuilder builder = new StringBuilder();

                    foreach (byte b in bytes) {
                        builder.Append(b.ToString("x2"));
                    }

                    return builder.ToString();

                }

            } catch (Exception ex) {

                Console.WriteLine(ex.ToString());

                return "error";

            }

        }

    }

}
