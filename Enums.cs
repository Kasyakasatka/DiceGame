using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame
{
    public enum ZeroToFive
    {
        Zero = '0',
        One = '1',
        Two = '2',
        Three = '3',
        Four = '4',
        Five = '5'
    }
    public enum ZeroToOne
    {
        Zero = '0',
        One = '1'
    }
    public enum ExitHelp
    {
        Exit = 'X',
        Help = '?'
    }
    public enum  Player
    {
        Computer,
        User 
    }

    public enum SelectionType
    {
        ZeroOne,
        ZeroFive,
        DiceRange
    }
    public enum  YesNo
    {
        Yes = 'Y',
        No = 'N'
    }
}