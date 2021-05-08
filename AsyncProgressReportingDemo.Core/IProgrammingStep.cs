using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncProgressReportingDemo.Core
{
    public interface IProgrammingStep
    {
        event ProgrammingProgressEventHandler ProgrammingProgressChanged;
    }
}
