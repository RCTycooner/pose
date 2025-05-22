using System;
using System.Reflection;

namespace Pose.IL
{
    public class ExceptionHandler
    {
        public Type CatchType;

        public ExceptionHandlingClauseOptions Flags;

        public int TryStart;

        public int TryEnd;

        public int FilterStart;

        public int HandlerStart;

        public int HandlerEnd;
    }
}