using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Folder_Recon
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "©Akash Aman";

            
            // implemented this try-catch block in 2nd release to avoid the app from crashing incase the path is incorrect or non-existent.
            try
            {



                // Create two identical or different temporary folders
                // on a local drive and change these file paths.  

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\nEnter Folder1 name and path:");
            string pathA = Console.ReadLine();

            Console.WriteLine("\nEnter Folder2 name and path:");
            string pathB = Console.ReadLine();


            System.IO.DirectoryInfo dir1 = new System.IO.DirectoryInfo(pathA);
            System.IO.DirectoryInfo dir2 = new System.IO.DirectoryInfo(pathB);

            // Take a snapshot of the file system.  
            IEnumerable<System.IO.FileInfo> list1 = dir1.GetFiles("*.*", System.IO.SearchOption.AllDirectories);
            IEnumerable<System.IO.FileInfo> list2 = dir2.GetFiles("*.*", System.IO.SearchOption.AllDirectories);

            //A custom file comparer defined below  
            FileCompare myFileCompare = new FileCompare();

            // This query determines whether the two folders contain  
            // identical file lists, based on the custom file comparer  
            // that is defined in the FileCompare class.                       
            // The query executes immediately because it returns a bool.  
            bool areIdentical = list1.SequenceEqual(list2, myFileCompare);

            if (areIdentical == true)
            {
                Console.WriteLine("\nthe two folders are the same.\n");
            }
            else
            {
                Console.WriteLine("\nThe two folders are not the same.\n");
            }

            // Find the common files. It produces a sequence and doesn't
            // execute until the foreach statement.  
            var queryCommonFiles = list1.Intersect(list2, myFileCompare);

            if (queryCommonFiles.Any())
            {
                Console.WriteLine("\nThe following files are in both folders:");
                foreach (var v in queryCommonFiles)
                {
                    Console.WriteLine(v.FullName); //shows which items end up in result list  
                }
            }
            else
            {
                Console.WriteLine("There are no common files in the two folders.");
            }

            // Find the set difference between the two folders.  
            // For this example we only check one way.  
            var queryList1Only = (from file in list1
                                  select file).Except(list2, myFileCompare);

            Console.WriteLine("\nFollowing files in folder1 doesn't match with files in folder2:\n");
            foreach (var v in queryList1Only)
            {
                Console.WriteLine(v.FullName);
            }

                
                Console.WriteLine("\n***End of the Output***");

                // prompt for user input again withyout restarting the console

                Console.WriteLine("\nWant to try again ? Press 'y' and Enter\n");

                String SelectedKey = Console.ReadLine();
                if (SelectedKey == "y")
                {   

                    // clear the screen and reset the app by calling main method.

                    Console.Clear();
                    Main(args);

                }
                Console.WriteLine("\nOkay then, Press any key to exit....");

                
                
            }
            catch (DirectoryNotFoundException)
            {

                Console.WriteLine("\nHi DSD member, you have entered incorrect path or folder name.\n");
               

                // prompt for user input again withyout restarting the console

                Console.WriteLine("\nWant to try again ? Press 'y' and Enter\n");

               String SelectedKey = Console.ReadLine();
                if (SelectedKey == "y")
                {
                    // clear the screen and reset the app by calling main method.

                    Console.Clear();
                    Main(args);


                }
                Console.WriteLine("\nPress any key to exit....");
                Console.ReadKey();


            }
             }
    }



    // This implementation defines a very simple comparison  
    // between two FileInfo objects. It only compares the name  
    // of the files being compared and their length in bytes.  

      class FileCompare : System.Collections.Generic.IEqualityComparer<System.IO.FileInfo>
      {
          public FileCompare() { }

          public bool Equals(System.IO.FileInfo f1, System.IO.FileInfo f2)
          {
              return (f1.Name == f2.Name &&
                      f1.Length == f2.Length);
          } 

          // Return a hash that reflects the comparison criteria. According to the
          // rules for IEqualityComparer<T>, if Equals is true, then the hash codes must  
          // also be equal. Because equality as defined here is a simple value equality, not  
          // reference identity, it is possible that two or more objects will produce the same  
          // hash code.  
          public int GetHashCode(System.IO.FileInfo fi)
          {
              string s = $"{fi.Name}{fi.Length}";
              return s.GetHashCode();

          } 
      } 
}
