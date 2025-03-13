using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeiveIT.Models
{
    public record RawData(float PhiScale, float RolNum, float Weight = 0, float IndWeight = 0, float CummWeight = 0)
    {
        public static IEnumerable<RawData> GetRows()
        {
            var val = -2.00f;
            sbyte rowNumber = 0;
            var rows = new List<RawData>();
            while (val <= 5.00f)
            {
                rows.Add(new RawData(val, rowNumber));
                rowNumber++;
                val += 0.50f;
            }       
            return rows;
        }
    }

    
}
