using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;




namespace WebScraper
{

    public  class GetEmail : IWebBrowser
    {
        
      //ici, on génère un doc html avec url trouvé
      // si tant que le maximumdepth est superieur à 0,
      // si on trouve un url contenant du html dans les urls ,
      // on réappelle la fonction en elle même,
      // à chaque fois on ajoute l'email dans la liste des emails

        /* il faut  voir la recherche  comme un noeud qui s'étend vers les racines( les emails) ou d'autres noeud (site html) et ainsi de suite */
        public  List<string> GetEmailsInPageAndChildPages( string url, int maximumDepth)
        {
           
            var doc = new HtmlDocument();
            //on charge le document du contenu de url 

            doc.LoadHtml(url);
            var emails = new List<string>();

            //on recherche les emails, leur lien commence par mailto
            var emailsNode = doc.DocumentNode.SelectNodes("//a[starts-with(@href, 'mailto:')]");

            if (emailsNode != null )
            {
                foreach (var emailNode in emailsNode)
                {
                    string href = emailNode.GetAttributeValue("href", "");
                    if (href.StartsWith("mailto:"))
                    {
                        string email = href.Replace("mailto:", "").Trim();

                        if (!emails.Contains(email)) emails.Add(email);
                        
                        
                    }
                }

            }
            var siteNodes = doc.DocumentNode.SelectNodes("//a[@href]");
            maximumDepth = maximumDepth - 1;

            if (siteNodes != null)
            {
                foreach (var node in siteNodes)
                {
                    string href = node.GetAttributeValue("href", "").Trim();
                    if (href.EndsWith(".html", StringComparison.OrdinalIgnoreCase))
                    {
                        string Siteurl = href;
                        if(maximumDepth > 0)
                        {
                            var childEmails = GetEmailsInPageAndChildPages(Siteurl, maximumDepth);
                            foreach (var mail in childEmails)
                            {
                                if (!emails.Contains(mail)) emails.Add(mail);
                            }

                        }


                    }
                }
            }
            // Implementation will go here
            if (!(emails.Count > 0))
            {
                return new List<string>();
            }
            return emails;
        
        }
        public string GetHtml(string url)
        {
            try
            {
                if (url.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                {
                    using var client = new HttpClient();
                    var response = client.GetAsync(url).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return response.Content.ReadAsStringAsync().Result;
                    }
                }
                else if (File.Exists(url))
                {
                    return File.ReadAllText(url); 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }

            return null;
        }

    }

}
