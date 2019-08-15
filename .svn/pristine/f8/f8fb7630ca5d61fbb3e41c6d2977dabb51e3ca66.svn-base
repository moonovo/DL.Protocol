using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Friendcom.Protocol
{
    public class Result
    {
        public bool Successful
        {
            get;
            set;
        }

        public string Reason
        {
            get;
            set;
        }

        /// <summary>
        /// Result隐式转换为bool, 可写成
        ///   Result r = new Result{ Successful = true };
        ///   bool b = r;
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static implicit operator bool(Result result)
        {
            return result.Successful;
        }

        /// <summary>
        /// bool隐式转换为Result,可写成
        ///   bool b = true;
        ///   Result r = b;
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static implicit operator Result(bool b)
        {
            return new Result { Successful = b };
        }

        public static explicit operator string(Result result)
        {
            return result.Reason;
        }

        public static explicit operator Result(string str)
        {
            return new Result { Successful = false, Reason = str };
        }
    }
}
