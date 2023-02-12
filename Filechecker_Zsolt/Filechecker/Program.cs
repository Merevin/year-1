using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Zsolt Bolla BOL19697492
//Filechecker
//Finished 17\03\2020
//Read files from user defined location
//Ask the user for file names and load them into arrays
//Compare the difference between the 2 files 
//Tell the user if its different or not
//

namespace Filechecker
{
    class Program
    {
        static void Main(string[] args)
        {
            //declare variables
            string user_input_path;
            string user_input_file_a;
            string user_input_file_b;

            // handles all the user inputs, first ask directory location then the 2 file names the user want to compare
            while (true)
            {
                Console.WriteLine(@"Please enter the file path of where all the files are located example C:\Users\Zsolt\Downloads");
                user_input_path = Console.ReadLine().ToLower().Trim();
                Console.WriteLine("Please enter the first file name you would like to compare for example a.txt");
                user_input_file_a = Console.ReadLine().ToLower().Trim();
                Console.WriteLine("Please enter the first file name you would like to compare for example b.txt");
                user_input_file_b = Console.ReadLine().ToLower().Trim();
                break;
            }
            // using try in case the there was an issue with the files it gives the user an answer in case that happens
            try
            {
                // counts how many lines are in each file
                var file_a_count = File.ReadLines(user_input_path +@"\"+ user_input_file_a).Count();
                var file_b_count = File.ReadLines(user_input_path + @"\"+ user_input_file_b).Count();

                
                //if there are differences in the amount of lines it skips the rest of the code since it meant that they are different
                if (file_a_count != file_b_count) 
                {
                    Console.WriteLine("They are different\r\n");                    
                }
                else
                {
                    //variable for a check on whether they are different or not
                    bool different = false;
                    bool file_end = false;

                    // create an array for each file
                    string[] file_a = new string[file_a_count];
                    string[] file_b = new string[file_b_count];

                    

                    //create a variable that has all the text from files inside
                    var str_a = File.ReadAllLines(user_input_path +@"\"+ user_input_file_a);
                    var str_b = File.ReadAllLines(user_input_path + @"\" + user_input_file_b);

                    // adds the text line by line to the variable
                    for (int i = 0; i < file_a_count; i++)
                        file_a[i] = str_a[i];
                        
                    for (int i = 0; i < file_b_count; i++)
                        file_b[i] = str_b[i];

                                        // runs the check process on whether the text are the same.
                    // for each line in a.txt the line checks if the length of sentence are the same and then it checks if the length of the words are the same.


                    while (different == false && file_end == false)
                    {
                        
                        for (int i = 0; i <file_a.Length; i++)
                        {

                            if (file_a[i].Length != file_b[i].Length)
                            {
                                different = true;
                            }
                            else
                            {

                            }

                               
                            
                            for (int c = 0; c < file_a[i].Length; c++)                            
                            {
                                    Console.WriteLine(file_a[i][c] + "vs" + file_b[i][c]);
                                    if (file_a[i][c] != file_b[i][c])
                                    {
                                        different = true;
                                    }
                                    else
                                    {

                                    }
                                    
                                    
                                
                            }
                            
                        }
                        //add a counter to avoid infinite loop
                        file_end = true;
                    }
                    
                    //gives the final asnwer after having the full check
                    if(different == true)
                    {
                        Console.WriteLine("They are different\r\n");
                    }
                    else
                    {
                        Console.WriteLine("They are same\r\n");
                    }

                    
                }
                

            }

            // handles all the error types that could come of by trying to read a file
            catch(FileNotFoundException e)
            {
                Console.WriteLine($"The file was not found: '{e}'\r\n");
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine($"The directory was not found: '{e}'\r\n");
            }
            catch (IOException e)
            {
                Console.WriteLine($"The file could not be opened: '{e}'\r\n");
            }
            
            //waits for user input to finish the program
            Console.WriteLine("Press a button to close the program\r\n");
            Console.ReadLine();

        }
    }
}
