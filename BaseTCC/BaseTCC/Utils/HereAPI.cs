using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace BaseTCC.Utils
{
    public class HereAPI
    {
        public const string APP_ID = "Axu9Wi6ADYeSntr6rjGZ";
        public const string APP_CODE = "n_LD8zbO1ni-ec1wfmNRzg";
        public string RestHttpGet(string url)
        {
            string responseString = string.Empty;

            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                var response = (HttpWebResponse)request.GetResponse();

                using (var stream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(stream))
                    {
                        responseString = reader.ReadToEnd();
                    }
                }
            }
            catch { throw; }

            return responseString;
        }

    }
}
