using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADPopulationApp
{
    class Program
    {
        static void Main(string[] args)
        {
           // SearchMethod();
            //CreateOU();
            CreateUser();
           //CreateSubOU();
            
            //DirectoryEntry user = new DirectoryEntry("LDAP://cn=userTest,dc=testes,dc=nddigital");
            //DirectoryEntry ou = user.Parent;
            //ou.Children.Remove(user);
            //ou.CommitChanges();
        }

        private static void CreateUser()
        {
            DirectoryEntry deBase = new DirectoryEntry("LDAP://Testes", "marina", "ndd@123");
            string name;
            for (int i = 0; i<3000; i++)
            {
                DirectoryEntry userEntry = deBase.Children.Find("OU=1", "organizationalUnit").Children.Find("ou=1.1","organizationalUnit")
                    .Children.Find("ou=1.1.1","organizationalUnit")/*.Children.Find("ou=2.3","organizationalUnit")*/
                    .Children.Add("cn=1.1.1 - user"+(i+1), "user");
                name = "Name " + (i + 1) + " - OU 1.1.1";
                userEntry.Properties["SamAccountName"].Add(name);
                userEntry.CommitChanges();
            }
        }

        private static void CreateSubOU()
        {
            DirectoryEntry deBase = new DirectoryEntry("LDAP://Testes", "marina", "testes@123");

            for (int m=0; m<10; m++)
            {
                for (int j = 0; j < 10; j++)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        for (int l=0; l<150; l++)
                        {
                            DirectoryEntry entry = deBase.Children.Find("OU=cursos", "organizationalUnit")
                                .Children.Find("OU=Tipo " + (j + 1), "organizationalUnit")
                                .Children.Find("OU=Curso " + (m + 1) + " Tipo " + (j + 1), "organizationalUnit")
                                .Children.Find("ou=Fase " + (i + 1) + " Curso " + (m + 1) + " Tipo " + (j + 1),
                                    "organizationalUnit")
                                .Children.Add("cn=User" + (l + 1) + " Curso " + (m + 1) + " Tipo " + (j + 1), "user");
                            entry.CommitChanges();
                        }
                    }
                }
            }


            //DirectoryEntry path = entry;
            //entry.CommitChanges();
            //path.Path = entry.Parent.Path;
            //path = path.Children.Add("SubTree2", "organizationalUnit");
            //path.CommitChanges();

            //DirectoryEntry path = entry;
            //for (int i = 0; i < 3; i++)
            //{
            //    string name = "SubTree" + (i + 1);
            //    string type = "organizationalUnit";
            //    path = path.Children.Add(name, type);
            //    path.CommitChanges();
            
            
            //}

             //entry = deBase.Children.Find("ou=Tree", "organizationalUnit")
             //           .Children.Find("ou=SubTree1"), "organizationalUnit")
             //               .Children.Add("cn=user" + (j + 1) + "-SubTree" + (i + 1), "user");
             //entry.CommitChanges();
            
            
            //for (int i=0; i<100; i++)
            //{
            //    DirectoryEntry entry = deBase.Children.Find("OU=Tree", "organizationalUnit")
            //        .Children.Add("ou=SubTree" + (i + 1), "organizationalUnit");
            //    entry.CommitChanges();
            //    for (int j = 0; j < 50; j++)
            //    {
            //        entry = deBase.Children.Find("ou=Tree", "organizationalUnit")
            //            .Children.Find("ou=SubTree" + (i + 1), "organizationalUnit")
            //                .Children.Add("cn=user"+ (j + 1)+"-SubTree"+(i+1), "user");
            //        entry.CommitChanges();
            //    }
            //}
        }
        private static void CreateOU()
        {
            DirectoryEntry deBase = new DirectoryEntry("LDAP://Testes", "marina", "ndd@123");
            DirectoryEntry entry = deBase.Children.Add("OU=1", "organizationalUnit");
            entry.CommitChanges();
        }

        private static void SearchMethod()
        {
            DirectoryEntry root = new DirectoryEntry("LDAP://Testes", "marina", "testes@123", AuthenticationTypes.Secure);
            DirectorySearcher ouSearch = new DirectorySearcher(root, "(objectClass=organizationalUnit)", null,
                SearchScope.Subtree);
            ouSearch.PropertiesToLoad.Add("cn");
            SearchResultCollection searchResultCollection = ouSearch.FindAll();
            List<string> ous = new List<string>();
            foreach (SearchResult result in searchResultCollection)
            {
                ous.Add(result.GetDirectoryEntry().Path);
                //Console.WriteLine(result.GetDirectoryEntry().Path);
            }
            string first = ous.First(x => x.ToLower().Contains("santini"));
            Console.ReadKey();
        }
    }
}
