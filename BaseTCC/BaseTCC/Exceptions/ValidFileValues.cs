using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseTCC.Exceptions
{
    public class ValidFileValues
    {
        private StringBuilder builder = null;

        public ValidFileValues(params string[] values) 
        {

            builder = new StringBuilder("Uma Excessão foi lançada, devido a um parametro vazio!");

            foreach (string val in values)
            {
                if (string.IsNullOrEmpty(val))
                {
                    throw new FormatException(builder.ToString());
                }
            }
        }

    }
}
