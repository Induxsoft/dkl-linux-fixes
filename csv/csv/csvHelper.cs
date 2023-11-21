using CsvHelper;
using System;
using System.Globalization;
using System.IO;
using System.Collections.Generic;

namespace devkron.serialize.csv
{
    public class csvHelper
    {
        public static void CsvWrite(string file, object data)
        {
            List<dynamic> l = new List<dynamic>();
            foreach(object d in (IList<object>)data)
            {
                IDictionary<string, object> dict = (IDictionary<string, object>)d;
                var eo = new System.Dynamic.ExpandoObject();
                
                foreach (var kvp in dict)
                    ((ICollection<KeyValuePair<string, object>>)eo).Add(kvp);

                l.Add((dynamic)eo);
            }

            using (var writer = new StreamWriter(file,false,System.Text.Encoding.UTF8))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(l);

                writer.Flush();
            }

        }
        public static object CsvRead(string file, string numcols)
        {
            List<object> l = new List<object>();
            var cols = numcols.Split(',');
            using (var reader = new StreamReader(file))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<dynamic>();

                foreach (var r in records)
                {
                    var dic = new Dictionary<string, object>((IDictionary<string, object>)r);
                    if (!string.IsNullOrWhiteSpace(numcols))
                    {
                        //Convertir campos numéricos conocidos
                        
                        foreach(string c in cols)
                        {
                            if (dic.ContainsKey(c.Trim()))
                            {
                                decimal v;
                                if (decimal.TryParse((dic[c.Trim()] ?? "0").ToString(),NumberStyles.Any, CultureInfo.InvariantCulture, out v))
                                    dic[c.Trim()] = v;
                            }
                        }
                    }
                    l.Add(dic);
                }
            }
            return l;
        }
    }
}
