using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class IntParser
    {
        /// <summary>
        /// Returns an int string can be parsed, otherwise returns a 400 statuscode http exception
        /// </summary>
        /// <returns>Parsed int if successful, otherwiese a BadHttpRequestException</returns>
        public int parseInt(string parseable)
        {
            bool success = int.TryParse(parseable, out int value);

            if (!success)
            {
                throw new BadHttpRequestException($"'${parseable}' on collection can't be parsed to an int", 400);
            }

            return value;
        }
    }
}
