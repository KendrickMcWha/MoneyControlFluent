using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyControl.Domain.Extension;
public static class ExtensionMethods
{
    public static int DateToInt(this DateTime date)
    {
        return Convert.ToInt32(date.ToString("yyyyMMdd"));
    }
}
