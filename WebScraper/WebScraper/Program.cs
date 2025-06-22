// See https://aka.ms/new-console-template for more information
using WebScraper;

Console.WriteLine("Hello, World!");
string url = "http://tulipe.exp.esiea.fr:8080";
string url2 = @"C:/Users/peter/OneDrive - Groupe ESIEA/S3/Algo/crawler/index.html";
string url3 = "https://wordpress.mediadoma.com/fr/conceptions-de-boites-de-recherche-html-basees-sur-css-pour-ameliorer-la-recherche-de-votre-site/";
GetEmail getemails = new ();


int maximumDepth = 2;


Console.WriteLine( string.Join(" ",getemails.GetEmailsInPageAndChildPages(url3, maximumDepth)));
