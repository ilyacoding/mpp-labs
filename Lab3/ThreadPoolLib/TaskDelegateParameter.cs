﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadPoolLib
{
    public class TaskDelegateParameter
    {
        public TaskDelegate Task { get; set; }
        public object Parameters { get; set; }
    }
}
