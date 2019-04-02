using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp.AspNetCore.Services.DateTimeServices
{
  public class DateTimeService
  {
    public DateTime GetCurrent() => DateTime.UtcNow;
  }
}
