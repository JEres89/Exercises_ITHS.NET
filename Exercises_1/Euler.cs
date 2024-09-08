using static CommonFunctionality.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Exercises_1
{
	internal class Euler : Exercises
	{
		private static Euler? euler;
		private Euler()
		{
			string name = "Euler";
			Dictionary<int, (Action, string)> exercises = new()
			{
				{ 8, (Euler8, "Largest product in a series") }
			};
			Init(name, exercises);

		}

		public static Euler GetEuler()
		{
			return (euler ??= new Euler());
		}

		public static void Euler8()
		{
			/*
			 * 
			 */
			double timerStart = System.DateTime.Now.Nanosecond;
			string fullSequence = "7316717653133062491922511967442657474235534919493496983520312774506326239578318016984801869478851843858615607891129494954595017379583319528532088055111254069874715852386305071569329096329522744304355766896648950445244523161731856403098711121722383113622298934233803081353362766142828064444866452387493035890729629049156044077239071381051585930796086670172427121883998797908792274921901699720888093776657273330010533678812202354218097512545405947522435258490771167055601360483958644670632441572215539753697817977846174064955149290862569321978468622482839722413756570560574902614079729686524145351004748216637048440319989000889524345065854122758866688116427171479924442928230863465674813919123162824586178664583591245665294765456828489128831426076900422421902267105562632111110937054421750694165896040807198403850962455444362981230987879927244284909188845801561660979191338754992005240636899125607176060588611646710940507754100225698315520005593572972571636269561882670428252483600823257530420752963450";
			string[] splitSequences = fullSequence.Split('0');

			long maxProduct = 0;
			int[] maxInts = new int[13];

			for (int i = 0; i < splitSequences.Length; i++)
			{
				if (splitSequences[i].Length >= 13)
				{
					int[] ints = new int[splitSequences[i].Length];
					long product = 1;
					int j = 0;

					while (j < ints.Length)
					{
						if (j == 0)
						{
							for (; j < 13; j++)
							{
								int num = (int)char.GetNumericValue(splitSequences[i][j]);
								ints[j] = num;
								product *= num;
							}
						}
						else
						{
							product /= ints[j - 13];
							int num = (int)char.GetNumericValue(splitSequences[i][j]);
							product *= num;
							ints[j] = num;
							j++;
						}

						if (product > maxProduct)
						{
							maxProduct = product;
							Array.Copy(ints, j - 13, maxInts, 0, 13);
						}
					}
				}
			}

			double timerStop = System.DateTime.Now.Nanosecond;

			double time = timerStop - timerStart;

			char[] maxChars = maxInts.Select(i => (char)('0' | i)).ToArray();
			Console.WriteLine($"{new string(maxChars)} = {maxProduct}\nTid: {time} ns");
			PromptContinue();

		}
	}
}
