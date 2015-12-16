﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharp.Ballistics.Calculator.ViewModels
{
    public class CalculatorViewModel : FunctionScreen
    {
        public override int Order
        {
            get
            {
                return 1;
            }
        }

        public override string IconFilename => "calc.png";
    }
}