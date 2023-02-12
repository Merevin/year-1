using System;

/*To Do;
 * Add All Countries Participating Rule!!!
 * Clean Up / Improve efficiency
 * Comment
 * Make Github Uploads look good
 */
 /*
  * added the 4 country rules and selection for them
 */

namespace euvote
{
    class Program
    {
        static void Main(string[] args)
        {
            // Calling the "Qualified Majority" rule
            string rule_input;

            Console.WriteLine("Choose the rule type you want to use with \"1\", \"2\" \"3\" or \"4\" \r\nfor Qualified Majority, Reinforced Qualified Majority, Simple Majority or Unamity.");
            rule_input = Console.ReadLine().ToLower().Trim();
            while (true)
            {
                if (rule_input == "1" || rule_input == "2" || rule_input == "3" || rule_input == "4")
                {
                    
                    break;  // Breaks out of the while true loop
                }
                else
                {
                    Console.WriteLine("Did not recognise that character!");
                }

            }
            if (rule_input == "1")
            {
                Console.WriteLine("You have chosen Qualified Majority");
                Resultcalc(0.55, 65);
            }
            else if (rule_input == "2")
            {
                Console.WriteLine("You have chosen Reinforced Qualified Majority");
                Resultcalc(0.72, 65);
            }
            else if (rule_input == "3")
            {
                Console.WriteLine("You have chosen Simple Majority");
                Resultcalc(50, 0);
            }
            else
            {
                Console.WriteLine("You have chosen Unamity");
                Resultcalc(100, 0);
            }
            Console.ReadLine();
        }

        // Implementation of the Qualified Majority Rule
        static void Resultcalc(double country_percent, double population_percent) 
        {
            CountryStats EU_Countries = new CountryStats();

            // Declaring variables used for vote & population tracking
            int array_counter = 0;
            double pop_vote_yes = 0, pop_vote_no = 0, pop_vote_abstain = 0, vote_yes = 0, vote_no = 0, vote_abstain = 0;
            string vote_input;


            Console.WriteLine("Answer with \"y\", \"n\" or \"a\" for yes, no and abstain respectively.");   // Telling the user how to answer
            // Looping through every country in the EU
            foreach (string country in EU_Countries.countries)
            {
                
                
                while (true)
                {
                    
                    Console.WriteLine($"Country: {country} ({EU_Countries.population[array_counter]}% population)");
               
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
                    pop_vote_yes = pop_vote_yes + EU_Countries.population[array_counter];
                }
                else if (vote_input == "n")
                {
                    vote_no++;
                    pop_vote_no = pop_vote_no + EU_Countries.population[array_counter];
                }
                else if (vote_input == "a") 
                {
                    vote_abstain++;
                    pop_vote_abstain = pop_vote_abstain + EU_Countries.population[array_counter];
                }

                array_counter++;  // Incremting the array pointer to get the correct population
                double percent_vote_yes = (vote_yes / EU_Countries.countries.Length) * 100;
                double percent_vote_no = (vote_no / EU_Countries.countries.Length) * 100;
                double percent_vote_abstain = (vote_abstain / EU_Countries.countries.Length) * 100;
                
                if (percent_vote_yes >= country_percent & pop_vote_yes >= population_percent)

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


            // The final result is approved when 55% of the member states AND 65% of the population approve
            

        }
    }
    class CountryStats
    {
        // Arrays are 1:1 match - so index 0 for countries matches its population
        public string[] countries = new String[] { "Austria", "Belgium", "Bulgaria", "Croatia", "Cyprus", "Czech Republic", "Denmark", "Estonia", "Finland", "France", "Germany", "Greece", "Hungary", "Ireland", "Italy", "Latvia", "Lithuania", "Luxembourg", "Malta", "Netherlands", "Poland", "Portugal", "Romania", "Slovakia", "Slovenia", "Spain", "Sweden" };
        public double[] population = new double[] { 1.98, 2.56, 1.56, 0.91, 0.2, 2.35, 1.3, 0.3, 1.23, 14.98, 18.54, 2.4, 2.18, 1.1, 13.65, 0.43, 0.62, 0.14, 0.11, 3.89, 8.49, 2.3, 4.34, 1.22, 0.44, 10.49, 2.29 };
    }
}