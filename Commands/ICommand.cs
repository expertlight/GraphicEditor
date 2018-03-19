﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicEditor.Commands
{
    public interface ICommand
    {
        bool Exec();
        void Undo();
    }
}
