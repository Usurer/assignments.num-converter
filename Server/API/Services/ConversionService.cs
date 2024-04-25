using API.Utils;
using System.Text;

namespace API.Services
{
    public class ConversionService : IConversionService
    {
        public string Convert(decimal value)
        {
            // We can do additional validation here, since the code works only with
            // values in [0, 999_999_999.99] range. But I'll leave this out of scope.
            return ConvertToString(value);
        }

        internal (int Millions, int Thousands, int Ones, int Fractions) SplitToDigits(decimal value)
        {
            int flooredValue = (int)value;
            int fraction = (int)((value - flooredValue) * 100);

            int millions = flooredValue / 1000_000;
            int partBelowMillion = flooredValue - millions * 1000_000;

            int thousands = partBelowMillion / 1000;
            int partBelowThousand = partBelowMillion - thousands * 1000;

            int ones = partBelowThousand;

            return (millions, thousands, ones, fraction);
        }

        private string ConvertToString(decimal value)
        {
            var (millions, thousands, ones, fractions) = SplitToDigits(value);

            if (value == 0)
            {
                return $"{Labels.ONES[0]} {Labels.DOLLARS}";
            }

            var sb = new StringBuilder();

            GetMillions(millions, sb);

            GetThousands(thousands, sb);

            GetOnes(ones, sb);

            AddDollarsLabel(value, sb);

            GetFractions(fractions, sb);

            AddCentsLabel(fractions, sb);

            return sb.ToString();
        }

        private void GetMillions(int millions, StringBuilder sb)
        {
            if (millions > 0)
            {
                sb.Append($"{Stringify(millions)} {Labels.MILLION}");
            }
        }

        private void GetThousands(int thousands, StringBuilder sb)
        {
            if (thousands > 0)
            {
                if (sb.Length > 0)
                {
                    sb.Append(" ");
                }

                sb.Append($"{Stringify(thousands)} {Labels.THOUSAND}");
            }
        }

        private void GetOnes(int ones, StringBuilder sb)
        {
            if (ones > 0)
            {
                if (sb.Length > 0)
                {
                    sb.Append(" ");
                }

                sb.Append(Stringify(ones));
            }
        }

        private void GetFractions(int fractions, StringBuilder sb)
        {
            if (fractions > 0)
            {
                if (sb.Length > 0)
                {
                    sb.Append($" {Labels.AND} ");
                }

                sb.Append($"{Stringify(fractions)}");
            }
        }

        private void AddCentsLabel(int value, StringBuilder sb)
        {
            if (value == 1)
            {
                sb.Append($" {Labels.CENT}");
            }
            else if (value > 1)
            {
                sb.Append($" {Labels.CENTS}");
            }
        }

        private void AddDollarsLabel(decimal value, StringBuilder sb)
        {
            if (value >= 1 && value < 2)
            {
                sb.Append($" {Labels.DOLLAR}");
            }
            else if (value >= 2)
            {
                sb.Append($" {Labels.DOLLARS}");
            }
        }

        private string HundredsToString(int val)
        {
            if (val < 100)
            {
                return TensToString(val);
            }

            var hundreds = val / 100;
            var tens = val % 100;

            var tensStr = tens > 0 ? $" {TensToString(tens)}" : string.Empty;
            return $"{Labels.ONES[hundreds]} {Labels.HUNDRED}{tensStr}";
        }

        private string TensToString(int val)
        {
            if (val < 10)
            {
                return Labels.ONES[val];
            }

            if (val < 20)
            {
                return Labels.TEENS[val];
            }

            var ones = val % 10;
            var tens = val / 10;
            var onesString = ones > 0 ? $"-{Labels.ONES[ones]}" : string.Empty;

            return $"{Labels.TEES[tens * 10]}{onesString}";
        }

        private string Stringify(int value)
        {
            var val = HundredsToString(value);
            return val;
        }
    }
}