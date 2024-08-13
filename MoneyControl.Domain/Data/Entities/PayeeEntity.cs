using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyControl.Domain.Data.Entities;
public class PayeeEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int DefaultCategoryId { get; set; }
    public string DisplayName { get; set; }
}
