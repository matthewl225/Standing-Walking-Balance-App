using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WiiBalanceWalker
{
    public static class Globals
    {
        public static double COPx;
        public static double COPy;
        public static double COGx;
        public static double COGy;

        //keeps track of the last 30 seconds
        public static double[] COGxArray = new double[600];
        public static double[] COGyArray = new double[600];

        public static double[] TLArray = new double[600];
        public static double[] BLArray = new double[600];
        public static double[] TRArray = new double[600];
        public static double[] BRArray = new double[600];

        public static double[] TimeArray = new double[600];

        //20Hz, if 30 seconds, need 600
        public static int Time;
        public static int Period;

        public static bool TimerOn;
        public static bool TraceOn;

    
    }
}
