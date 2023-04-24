using System.Collections.Generic;

namespace FootballKit1
{
    public class ACS
    {
        public List<string> listString { get; set; } = new List<string>();
        public string txtFile { get; set; } = string.Empty;
        public string title { get; set; } = string.Empty;

        public ACS(List<string> listStringParam, string txtFileParam, string titleParam)
        {
            listString = listStringParam;
            txtFile = txtFileParam;
            title = titleParam;
        }
    }
}
