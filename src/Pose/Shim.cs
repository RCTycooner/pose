﻿using Pose.Helpers;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Pose
{
    public partial class Shim
    {
        private MethodBase _original;
        private Delegate _replacement;
        private Object _instance;
        private Type _type;
        private bool _setter;

        public MethodBase Original
        {
            get
            {
                return _original;
            }
        }

        public Delegate Replacement
        {
            get
            {
                return _replacement;
            }
        }

        public Object Instance
        {
            get
            {
                return _instance;
            }
        }

        public Type Type
        {
            get
            {
                return _type;
            }
        }

        private Shim(MethodBase original, object instanceOrType)
        {
            _original = original;
            if (instanceOrType is Type type)
                _type = type;
            else
                _instance = instanceOrType;
        }

        public static Shim Replace(Expression<Action> expression, bool setter = false)
            => ReplaceImpl(expression, setter);

        public static Shim Replace<T>(Expression<Func<T>> expression, bool setter = false)
            => ReplaceImpl(expression, setter);

        private static Shim ReplaceImpl<T>(Expression<T> expression, bool setter)
        {
            MethodBase methodBase = ShimHelper.GetMethodFromExpression(expression.Body, setter, out object instance);
            return new Shim(methodBase, instance) { _setter = setter };
        }

        private Shim WithImpl(Delegate replacement)
        {
            _replacement = replacement;
            ShimHelper.ValidateReplacementMethodSignature(this._original, this._replacement.Method, _instance?.GetType() ?? _type, _setter);
            return this;
        }
    }
}