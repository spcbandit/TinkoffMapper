using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TinkoffMapper
{
    public static class Versions
    {
        public static bool CheckDependency(out string error)
        {
            error = string.Empty;
            try
            {
                Google.Protobuf.JsonParser json = new Google.Protobuf.JsonParser(new Google.Protobuf.JsonParser.Settings(999));
                json.Parse("{}", null);
                return true;
            }
            catch (Exception e)
            {
                error = e.Message;
                return false;
            }
        }
    }
}
