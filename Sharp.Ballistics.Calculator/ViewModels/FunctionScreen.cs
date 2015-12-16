﻿using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Sharp.Ballistics.Calculator.ViewModels
{
    public abstract class FunctionScreen : Screen
    {
        public abstract string IconFilename { get; }

        private ImageSource icon;
        public ImageSource Icon
        {
            get
            {
                if (icon == null)
                {
                    icon = new BitmapImage(new Uri($"pack://application:,,,/Sharp.Ballistics.Calculator;component/Images/{IconFilename}"));
                    icon.Freeze();
                }
                return icon;
            }
        }

        public abstract int Order { get; } 
    }
}