
namespace BookStore.Application.Helpers
{
    public static class StringHelper
    {
        public static string GenerateRandomString(int size)
        {
            Random random = new Random();
            var alphabet = "abcdefghijklmnopqrstuvwyxzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var chars = new char[size];
            for (int i = 0; i < size; i++)
            {
                chars[i] = alphabet[random.Next(alphabet.Length)];
            }
            return new string(chars);
        }
    }
}