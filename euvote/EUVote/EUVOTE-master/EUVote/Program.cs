using System;
using System.Collections.Generic;

/*To Do;
 * Clean Up
 * Comment
 * Make Github Uploads look good
 */

/* ENCAPSULATION: putting all funcionality and state information for a class together; keep state variables 
 *                or fields private; can only call public methods made avaliable
 *                
 * ABSTRACTION: hide implementation details of a class; use access modifiers to hide or not, and make necessary
 *              methods and fields public; functionality of a class is hidden from a user
 */

namespace euvote
{
    class Program
    {

         public static void Main(string[] args)
        {

            string user_input;
            VotingSystem voting = new VotingSystem();


            while (true)
            {
                Console.WriteLine("Are all countries participating? (y/n)");
                user_input = Console.ReadLine().ToLower().Trim();

                // Basic input verification, so we can have usable input
                if (user_input == "y" || user_input == "n")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Didn't recognise that!");
                }
            }

            // Deciding what to do, considering the user input
            switch (user_input)
            {
                case "y":
                    break;
                case "n":
                    voting.ModifyCountrySet();
                    break;
            }

            while (true)
            {
                Console.WriteLine("Choose the rule type you want to use with \"1\", \"2\" \"3\" or \"4\" \r\nfor Qualified Majority, Reinforced Qualified Majority, Simple Majority or Unamity.");
                user_input = Console.ReadLine().Trim();

                // Basic input verification
                if (user_input == "1" || user_input == "2" || user_input == "3" || user_input == "4")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Did not recognise that character!");
                }
            }

            // Deciding what to do, given the input
            switch (user_input)
            {
                case "1":
                    Console.WriteLine("Voting rule: Qualified Majority");
                    voting.CallTheJudge(0.55, 65);
                    break;
                case "2":
                    Console.WriteLine("Voting rule: Reinforced Qualified Majority");
                    voting.CallTheJudge(0.72, 65);
                    break;
                case "3":
                    Console.WriteLine("Voting rule: Simple Majority");
                    voting.CallTheJudge(50, 0);
                    break;
                case "4":
                    Console.WriteLine("Voting rule: Unamity");
                    voting.CallTheJudge(100, 0);
                    break;
            }
            
            Console.ReadLine();

        }
    }

    class VotingSystem
    {
        // Arrays are 1:1 match - so index 0 for countries matches its population
        private string[] _defaultCountries = new String[] { "Austria", "Belgium", "Bulgaria", "Croatia", "Cyprus", "Czech Republic", "Denmark", "Estonia", "Finland", "France", "Germany", "Greece", "Hungary", "Ireland", "Italy", "Latvia", "Lithuania", "Luxembourg", "Malta", "Netherlands", "Poland", "Portugal", "Romania", "Slovakia", "Slovenia", "Spain", "Sweden" };
        private double[] _defaultPopulation = new double[] { 1.98, 2.56, 1.56, 0.91, 0.2, 2.35, 1.3, 0.3, 1.23, 14.98, 18.54, 2.4, 2.18, 1.1, 13.65, 0.43, 0.62, 0.14, 0.11, 3.89, 8.49, 2.3, 4.34, 1.22, 0.44, 10.49, 2.29 };
        
        private string[] _modifiedCountries = new string[] { };
        private double[] _modifiedPopulation = new double[] { };

        private bool _countriesModified = false;    // Contains information about which is the proper country + population array to give when requested

        public string[] participatingCountries
        {
            get
            {
                if (_countriesModified == false)
                {
                    return _defaultCountries;
                }
                else
                {
                    return _modifiedCountries;
                }
            }
        }

        public double[] participatingPopulation
        {
            get
            {
                if(_countriesModified == false)
                {
                    return _defaultPopulation;
                }
                else
                {
                    return _modifiedPopulation;
                }
            }
        }

        public void ModifyCountrySet()
        {
            Console.WriteLine("*** Spelling and case sensitive!!");
            Console.WriteLine("How many countries are being removed?");
            int remove_counter = Int32.Parse(Console.ReadLine());
            List<string> removeCountries = new List<string>();      // A list to store which countries are being removed

            for (int i = 0; i != remove_counter; i++)
            {
                Console.Write($"Country {i+1}: ");
                removeCountries.Add(Console.ReadLine());
            }


            // Temporary lists to store the new countries which are able to vote + their population
            List<string> tempCountryList = new List<string>();
            List<double> tempPopulationList = new List<double>();

            // Looping through every country which is in the EU
            foreach(string country in _defaultCountries)
            {
                // Checking whether or not they have been removed
                if(removeCountries.Contains(country) == false)
                {
                    // If not removed, add them to this list
                    tempCountryList.Add(country);

                    // We want to keep the population in line with the countries, so we grab the index of where the country exists in the country array
                    int tempArrayIndex = Array.IndexOf(_defaultCountries, country);
                    // Then we index the population array, and add the value to another list
                    tempPopulationList.Add(_defaultPopulation[tempArrayIndex]);
                }
            }

            // Converting them back into arrays for the main program to use
            _modifiedCountries = tempCountryList.ToArray();
            _modifiedPopulation = tempPopulationList.ToArray();
            _countriesModified = true; // Making sure that the proper variable is returned by using this switch
        }

        public void CallTheJudge(double country_percent_required, double population_percent_required)
        {

            // Declaring variables used for vote & population tracking
            int array_counter = 0;
            double pop_vote_yes = 0, pop_vote_no = 0, pop_vote_abstain = 0, vote_yes = 0, vote_no = 0, vote_abstain = 0;
            string vote_input;


            Console.WriteLine("Answer with \"y\", \"n\" or \"a\" for yes, no and abstain respectively.");   // Telling the user how to answer
            // Looping through every country in the EU
            foreach (string country in participatingCountries)
            {
                while (true)
                {
                    Console.WriteLine($"Country: {country} ({participatingPopulation[array_counter]}% population)");

                    vote_input = Console.ReadLine().ToLower().Trim();

                    // Verification check to ensure that only characters which the program can understand are entered
                    if (vote_input == "y" || vote_input == "n" || vote_input == "a")
                    {
                        Console.Clear();
                        break;  // Breaks out of the while true loop
                    }
                    else
                    {
                        Console.WriteLine("Did not recognise that character!");
                    }
                }

                // Incrementing the correct counter judging by the answer given
                if (vote_input == "y")
                {
                    vote_yes++;
                    pop_vote_yes = pop_vote_yes + participatingPopulation[array_counter];
                }
                else if (vote_input == "n")
                {
                    vote_no++;
                    pop_vote_no = pop_vote_no + participatingPopulation[array_counter];
                }
                else if (vote_input == "a")
                {
                    vote_abstain++;
                    pop_vote_abstain = pop_vote_abstain + participatingPopulation[array_counter];
                }

                array_counter++;  // Incremting the array pointer to get the correct population
                double percent_vote_yes = (vote_yes / participatingCountries.Length) * 100;
                double percent_vote_no = (vote_no / participatingCountries.Length) * 100;
                double percent_vote_abstain = (vote_abstain / participatingCountries.Length) * 100;

                if (percent_vote_yes >= country_percent_required & pop_vote_yes >= population_percent_required)

                {
                    Console.WriteLine("Final result is approved.");
                }
                else
                {
                    Console.WriteLine("Final result is rejected.");
                }

                Console.WriteLine($"Member State Y/N/A: {vote_yes}, {vote_no}, {vote_abstain}");
                Console.WriteLine($"Population % Y/N/A: {pop_vote_yes}, {pop_vote_no}, {pop_vote_abstain}\r\n");
            }
        }
    }
}
