using System;

namespace SubarrayMean
{

        private static void TakeInputAndProcessQueries()
        {
            int[] input = ReadIntArray();
            int numberOfElements = input[0];
            int numberOfQueries = input[1];

            long[] array = ReadInputs(numberOfElements);
            long[] prefixSum = EvaluatePrefixSum(array);

            for (int i = 0; i < numberOfQueries; i++)
            {
                int[] query = ReadQueries();
                int left = query[0];
                int right = query[1];

                long result = CalculateFloorMean(prefixSum, left, right);
                Console.WriteLine(result);
            }
        }


        private static long[] EvaluatePrefixSum(long[] array)
        {
            long[] prefixSum = new long[array.Length + 1];

            for (int i = 1; i <= array.Length; i++)
            {
                prefixSum[i] = prefixSum[i - 1] + array[i - 1];
            }

            return prefixSum;
        }

        private static long CalculateFloorMean(long[] prefixSum, int left, int right)
        {
            long subarraySum = prefixSum[right] - prefixSum[left - 1];
            int length = right - left + 1;

            return subarraySum / length;
        }


        private static int[] ReadQueries()
        {
            return Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
        }

        private static long[] ReadInputs(int count)
        {
            long[] values = Array.ConvertAll(Console.ReadLine().Split(' '), long.Parse);

            if (values.Length != count)
            {
                throw new ArgumentException("Array size does not match the given N value.");
            }

            return values;
        }
}
