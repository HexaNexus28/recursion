
interface IAmTheTest{
List<string> GetEmailsInPageAndChildPages(IWebBrowser browser, string url, int maximumDepth);
}
// Provided interface. Just use it.
interface IWebBrowser{
// Returns null if the url could not be visited.
string GetHtml(string url);
}
Exemple avec