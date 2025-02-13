using System.Text;

namespace ConsoleApp1
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine(VowelShift("This is a test!", 3));
		}
		public static string VowelShift(string text, int n)
		{
			if (text == null) { return text; }
			char[] vowels = ['a', 'e', 'i', 'o', 'u'];
			List<char> list = new List<char>();

			foreach (char c in text)
			{
				if (vowels.Contains(char.ToLower(c))) list.Add(c);
			}
			if (list.Count == 0) return text;
			n = n % list.Count;
			if (n < 0) n += list.Count;

			char[] shiftedVowels = new char[list.Count];

			for (int i = 0; i < list.Count; i++)
			{
				int newIndex = (i + n) % list.Count;
				shiftedVowels[newIndex] = list[i];
			}

			StringBuilder sb = new StringBuilder(text);
			int vowelIndex = 0;

			for (int i = 0; i < text.Length; i++)
			{
				if (vowels.Contains(char.ToLower(text[i])))
				{
					sb[i] = shiftedVowels[vowelIndex];
					vowelIndex++;
				}
			}

			return sb.ToString();
		}
	}
}