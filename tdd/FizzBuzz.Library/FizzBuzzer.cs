namespace FizzBuzz.Library
{
    public class FizzBuzzer
    {
        public static string GetValue(int input)
        {
            string output = string.Empty;

            if (input % 3 == 0)
            {
                output += "Fizz";
            }

            if (input % 5 == 0)
            {
                output += "Buzz";
            }

            if (string.IsNullOrEmpty(output))
            {
                return input.ToString();
            }

            return output;
        }
    }
}
