using System;
using System.Collections.Generic;
using System.IO;

namespace DungeonsDatabase
{
    // **************************************************
    //
    // Title: Dungeons Database
    // Application Type: Console
    // Description: A Dungeons & Dragons character profile program
    // Author: Kavanagh, Miles
    // Dated Created: 11/20/2019
    // Last Modified: 12/01/2019
    //
    // ************************************************** 
    class Program
    {
        /// <summary>
        /// main method, program starts here
        /// </summary>
        static void Main(string[] args)
        {
            // read data from file into list
            List<Character> characters = ReadFromDataFile();

            // call methods
            DisplayWelcomeScreen();
            DisplayMenuScreen(characters);
            DisplayClosingScreen();
        }

        #region FILE IO

        /// <summary>
        /// writes data to data file
        /// </summary>
        static void WriteToDataFile(List<Character> characters)
        {            
            string dataPath = @"Data\Data.txt";
            string[] characterStrings = new string[characters.Count];

            // create the array of character strings
            for (int index = 0; index < characters.Count; index++)
            {

                // create character string
                string characterString =
                    characters[index].Name + "," +
                    characters[index].Level + "," +
                    characters[index].Exp + "," + 
                    characters[index].CharRace + "," +
                    characters[index].CharClass + "," +
                    characters[index].Hp + "," +
                    characters[index].CharStatus + "," +
                    characters[index].Strength + "," +
                    characters[index].Dexterity + "," +
                    characters[index].Constitution + "," +
                    characters[index].Intelligence + "," +
                    characters[index].Wisdom + "," + 
                    characters[index].Charisma + "," + 
                    characters[index].Equipment;

                // add string to array
                characterStrings[index] = characterString;
            }

            // write array to data file

            File.WriteAllLines(dataPath, characterStrings);
        }

        /// <summary>
        /// reads data from data file
        /// </summary>
        static List<Character> ReadFromDataFile()
        {
            List<Character> characters = new List<Character>();
            string dataPath = @"Data\Data.txt";
            // read from data file into an array

            if (!File.Exists(dataPath))
            {
                File.Create(dataPath);
            }
            
            string[] characterStrings = File.ReadAllLines(dataPath);

            foreach (string character in characterStrings)
            {

                // get character property values
                string[] characterProperties = character.Split(',');

                // create a new character with property values
                Character newChar = new Character();

                newChar.Name = characterProperties[0];

                int.TryParse(characterProperties[1], out int level);
                newChar.Level = level;

                int.TryParse(characterProperties[2], out int exp);
                newChar.Exp = exp;

                Enum.TryParse(characterProperties[3], out Character.Race charRace);
                newChar.CharRace = charRace;

                Enum.TryParse(characterProperties[4], out Character.Class charClass);
                newChar.CharClass = charClass;

                int.TryParse(characterProperties[5], out int hp);
                newChar.Hp = hp;

                Enum.TryParse(characterProperties[6], out Character.Status charStatus);
                newChar.CharStatus = charStatus;

                int.TryParse(characterProperties[7], out int strength);
                newChar.Strength = strength;

                int.TryParse(characterProperties[8], out int dexterity);
                newChar.Dexterity = dexterity;

                int.TryParse(characterProperties[9], out int constitution);
                newChar.Constitution = constitution;

                int.TryParse(characterProperties[10], out int intelligence);
                newChar.Intelligence = intelligence;

                int.TryParse(characterProperties[11], out int wisdom);
                newChar.Wisdom = wisdom;

                int.TryParse(characterProperties[12], out int charisma);
                newChar.Charisma = charisma;

                // todo - equipment save

                // add new character to list
                characters.Add(newChar);
            }

            return characters;
            
        }

        #endregion

        #region FILTERS

        /// <summary>
        /// SCREEN: filter character submenu
        /// </summary>
        static void DisplayFilterCharacter(List<Character> characters)
        {
            DisplayScreenHeader("Filter Character");
            Console.WriteLine("a) Filter by Status");
            Console.WriteLine("b) Filter by Race");
            Console.WriteLine("c) Filter by Class");
            string userResponse = Console.ReadLine();

            switch (userResponse)
            {
                case "a":
                    DisplayFilterByStatus(characters);
                    break;
                case "b":
                    DisplayFilterByRace(characters);
                    break;
                case "c":
                    DisplayFilterByClass(characters);
                    break;
                default:
                    Console.WriteLine("Please respond with a letter value.");
                    DisplayContinuePrompt();
                    break;
            }            
        }

        /// <summary>
        /// SCREEN: filter characters by class property
        /// </summary>
        static void DisplayFilterByClass(List<Character> characters)
        {
            List<Character> filteredCharacters = new List<Character>();

            Character.Class selectedClass;

            DisplayScreenHeader("Filter by Class");
            Console.WriteLine("Available classes are BARBARIAN, BARD, CLERIC, DRUID, FIGHTER, MONK, PALADIN, RANGER, ROGUE, SORCERER, WARLOCK, WIZARD");
            Console.WriteLine();
            Console.Write("Enter Class: ");
            Enum.TryParse(Console.ReadLine().ToUpper(), out selectedClass);

            // add monsters with the sleected attitude to a new list
            foreach (Character character in characters)
            {
                if (character.CharClass == selectedClass)
                {
                    filteredCharacters.Add(character);
                }
            }

            // display new list
            DisplayScreenHeader($"{selectedClass} Players");
            DisplayTableFormat();
            foreach (Character character in filteredCharacters)
            {
                CharInfo(character);
            }

            DisplayContinuePrompt();
        }

        /// <summary>
        /// SCREEN: filter characters by race property
        /// </summary>
        private static void DisplayFilterByRace(List<Character> characters)
        {
            List<Character> filteredCharacters = new List<Character>();

            Character.Race selectedRace;

            DisplayScreenHeader("Filter by Race");
            Console.WriteLine("Available races are DRAGONBORN, DWARF, ELF, GENASI, GNOME, FIRBOLG, ORC, HUMAN, TIEFLING");
            Console.WriteLine();
            Console.Write("Enter Race: ");
            Enum.TryParse(Console.ReadLine().ToUpper(), out selectedRace);

            // add characters with the sleected attitude to a new list
            foreach (Character character in characters)
            {
                if (character.CharRace == selectedRace)
                {
                    filteredCharacters.Add(character);
                }
            }

            // display new list
            DisplayScreenHeader($"{selectedRace} Players");
            DisplayTableFormat();
            foreach (Character character in filteredCharacters)
            {
                CharInfo(character);
            }

            DisplayContinuePrompt();
        }

        /// <summary>
        /// SCREEN: filter characters by status property
        /// </summary>
        private static void DisplayFilterByStatus(List<Character> characters)
        {
            List<Character> filteredCharacters = new List<Character>();

            Character.Status selectedStatus;

            DisplayScreenHeader("Filter by Status");
            Console.WriteLine("Available status effects are HEALTHY, POISONED, BLINDED, CHARMED, UNCONSCIOUS");
            Console.WriteLine();
            Console.Write("Enter Status: ");
            Enum.TryParse(Console.ReadLine().ToUpper(), out selectedStatus);

            // add characters with the selected attitude to a new list
            foreach (Character character in characters)
            {
                if (character.CharStatus == selectedStatus)
                {
                    filteredCharacters.Add(character);
                }
            }

            // display new list
            DisplayScreenHeader($"{selectedStatus} Players");
            DisplayTableFormat();
            foreach (Character character in filteredCharacters)
            {
                CharInfo(character);
            }

            DisplayContinuePrompt();
        }

        #endregion

        #region MENU SCREENS

        /// <summary>
        /// SCREEN: displays menu screen
        /// </summary>
        static void DisplayMenuScreen(List<Character> characters)
        {
            bool quitApplication = false;
            string menuChoice;

            do
            {
                DisplayScreenHeader("Main Menu");

                // get user menu choice
                Console.WriteLine("a) List All Characters");
                Console.WriteLine("b) View Character Details");
                Console.WriteLine("c) Add Character");
                Console.WriteLine("d) Delete Character");
                Console.WriteLine("e) Update Character");
                Console.WriteLine("f) Save");
                Console.WriteLine("g) Filter Characters");
                Console.WriteLine("q) Quit");
                Console.Write("Enter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                // process user menu choice
                switch (menuChoice)
                {
                    case "a":
                        DisplayAllCharacters(characters);
                        break;

                    case "b":
                        DisplayViewCharacterDetail(characters);
                        break;

                    case "c":
                        DisplayAddCharacter(characters);
                        break;

                    case "d":
                        DisplayDeleteCharacter(characters);
                        break;

                    case "e":
                        DisplayUpdateCharacter(characters);
                        break;

                    case "f":
                        DisplayWriteToFile(characters);
                        break;

                    case "g":
                        DisplayFilterCharacter(characters);
                        break;

                    case "q":
                        quitApplication = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("Please enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }


            } while (!quitApplication);
        }

        /// <summary>
        /// SCREEN: displays write to file/"save" 
        /// </summary>
        static void DisplayWriteToFile(List<Character> characters)
        {
            DisplayScreenHeader("Save File");

            Console.Write("Would you like to save your character additions? [y/n] ");
            string userResponse = Console.ReadLine();
            if (userResponse == "y")
            {

                // process the exceptions
                try
                {
                    WriteToDataFile(characters);
                }
                catch (IOException e)
                {
                    Console.WriteLine($"Unable to save data. {e}");
                    throw;
                }

                Console.WriteLine();
                Console.WriteLine("Data saved successfully.");

                DisplayContinuePrompt();
            }
            else if (userResponse == "n")
            {
                Console.WriteLine("Save cancelled");
                DisplayContinuePrompt();
            }


        }

        /// <summary>
        /// SCREEN: displays character update options
        /// </summary>
        static void DisplayUpdateCharacter(List<Character> characters)
        {
            bool validResponse = false;
            Character selectedChar = null;

            do
            {
                DisplayScreenHeader("Update Character");

                // display all character names
                Console.WriteLine("\tCharacter Names");
                Console.WriteLine("\t-------------");
                foreach (Character character in characters)
                {
                    Console.WriteLine("\t" + character.Name);
                }

                // get user character choice
                Console.WriteLine();
                Console.Write("\tEnter name:");
                string characterName = Console.ReadLine();

                // get character object
                foreach (Character character in characters)
                {
                    if (character.Name == characterName)
                    {
                        selectedChar = character;
                        validResponse = true;
                        break;
                    }
                }

                // feedback for wrong name choice
                if (!validResponse)
                {
                    Console.WriteLine("\tPlease select a correct name.");
                    DisplayContinuePrompt();
                }

            } while (!validResponse);

            // update character
            string userResponse;
            DisplayScreenHeader("Update");
            Console.WriteLine("\tReady to update. Press the enter key to keep the current info.");
            Console.WriteLine();
            Console.Write($"\tCurrent Name: {selectedChar.Name}".PadRight(25));
            Console.Write("\tNew Name: ");
            userResponse = Console.ReadLine();
            if (userResponse != "")
            {
                selectedChar.Name = userResponse;
            }

            Console.Write($"\tCurrent Level: {selectedChar.Level}".PadRight(25));
            Console.Write("\tNew Level: ");
            userResponse = Console.ReadLine();
            if (userResponse != "")
            {
                if (!int.TryParse(userResponse, out int level))
                {
                    Console.WriteLine("\tThis is not a valid level. Please enter a numerical value.");
                    Console.WriteLine();
                    Console.Write("\tNew Level: ");
                    int.TryParse(Console.ReadLine(), out level);
                    selectedChar.Level = level;
                }
                else
                {
                    selectedChar.Level = level;
                }
            }

            Console.Write($"\tCurrent EXP: {selectedChar.Exp}".PadRight(25));
            Console.Write("\tNew EXP: ");
            userResponse = Console.ReadLine();
            if (userResponse != "")
            {
                if (!int.TryParse(userResponse, out int exp))
                {
                    Console.WriteLine("\tThis is not a valid EXP value. Please enter a numerical value.");
                    Console.WriteLine();
                    Console.Write("\tNew EXP: ");
                    int.TryParse(Console.ReadLine(), out exp);
                    selectedChar.Exp = exp;
                }
                else
                {
                    selectedChar.Exp = exp;
                }
            }

            Console.Write($"\tCurrent Race: {selectedChar.CharRace}".PadRight(25));
            Console.Write("\tNew Race: ");
            userResponse = Console.ReadLine().ToUpper();
            if (userResponse != "")
            {
                if (!Enum.TryParse(userResponse, out Character.Race charRace))
                {
                    Console.WriteLine("\tThis is not a valid race. Please enter DRAGONBORN, DWARF, ELF, GENASI, GNOME, FIRBOLG, ORC, HUMAN, or TIEFLING");
                    Console.WriteLine();
                    Console.Write("\tNew Race: ");
                    Enum.TryParse(Console.ReadLine().ToUpper(), out charRace);
                    selectedChar.CharRace = charRace;
                }
                else
                {
                    selectedChar.CharRace = charRace;
                }
            }

            Console.Write($"\tCurrent Class: {selectedChar.CharClass}".PadRight(25));
            Console.Write("\tNew Class: ");
            userResponse = Console.ReadLine().ToUpper();
            if (userResponse != "")
            {
                if (!Enum.TryParse(userResponse, out Character.Class charClass))
                {
                    Console.WriteLine("\tThis is not a valid class. Please enter BARBARIAN ,BARD, CLERIC ,DRUID, FIGHTER, MONK, PALADIN, RANGER,");
                    Console.WriteLine("\tROGUE, SORCERER, WARLOCK, or WIZARD");
                    Console.WriteLine();
                    Console.Write("\tNew Class: ");
                    Enum.TryParse(Console.ReadLine().ToUpper(), out charClass);
                    selectedChar.CharClass = charClass;
                }
                else
                {
                    selectedChar.CharClass = charClass;
                }
            }

            Console.Write($"\tCurrent HP: {selectedChar.Hp}".PadRight(25));
            Console.Write("\tNew HP: ");
            userResponse = Console.ReadLine();
            if (userResponse != "")
            {
                if (!int.TryParse(userResponse, out int hp))
                {
                    Console.WriteLine("\tThis is not a valid HP value. Please enter a numerical value.");
                    Console.WriteLine();
                    Console.Write("\tNew HP: ");
                    int.TryParse(Console.ReadLine(), out hp);
                    selectedChar.Hp = hp;
                }
                else
                {
                    selectedChar.Hp = hp;
                }
            }

            Console.Write($"\tCurrent Status: {selectedChar.CharStatus}".PadRight(25));
            Console.Write("\tNew Status: ");
            userResponse = Console.ReadLine().ToUpper();
            if (userResponse != "")
            {
                if (!Enum.TryParse(userResponse, out Character.Status charStatus))
                {
                    Console.WriteLine("\tThis is not a valid Status. Please enter HEALTHY, POISONED ,BLINDED, CHARMED, or UNCONSCIOUS");
                    Console.WriteLine();
                    Console.Write("\tNew Status: ");
                    Enum.TryParse(Console.ReadLine().ToUpper(), out charStatus);
                    selectedChar.CharStatus = charStatus;
                }
                else
                {
                    selectedChar.CharStatus = charStatus;
                }
            }

            Console.Write($"\tCurrent Strength (STR): {selectedChar.Strength}".PadRight(25));
            Console.Write("\tNew Strength: ");
            userResponse = Console.ReadLine();
            if (userResponse != "")
            {
                if (!int.TryParse(userResponse, out int strength))
                {
                    Console.WriteLine("\tThis is not a valid Strength value. Please enter a numerical value.");
                    Console.WriteLine();
                    Console.Write("\tNew STR: ");
                    int.TryParse(Console.ReadLine(), out strength);
                    selectedChar.Strength = strength;
                }
                else
                {
                    selectedChar.Strength = strength;
                }
            }

            Console.Write($"\tCurrent Dexterity (DEX): {selectedChar.Dexterity}".PadRight(25));
            Console.Write("\tNew Dexterity: ");
            userResponse = Console.ReadLine();
            if (userResponse != "")
            {
                if (!int.TryParse(userResponse, out int dexterity))
                {
                    Console.WriteLine("\tThis is not a valid Dexterity value. Please enter a numerical value.");
                    Console.WriteLine();
                    Console.Write("\tNew DEX: ");
                    int.TryParse(Console.ReadLine(), out dexterity);
                    selectedChar.Dexterity = dexterity;
                }
                else
                {
                    selectedChar.Dexterity = dexterity;
                }
            }

            Console.Write($"\tCurrent Constitution (CON): {selectedChar.Constitution}".PadRight(25));
            Console.Write("\tNew Constitution: ");
            userResponse = Console.ReadLine();
            if (userResponse != "")
            {
                if (!int.TryParse(userResponse, out int constitution))
                {
                    Console.WriteLine("\tThis is not a valid Constitution value. Please enter a numerical value.");
                    Console.WriteLine();
                    Console.Write("\tNew CON: ");
                    int.TryParse(Console.ReadLine(), out constitution);
                    selectedChar.Constitution = constitution;
                }
                else
                {
                    selectedChar.Constitution = constitution;
                }
            }

            Console.Write($"\tCurrent Intelligence (INT): {selectedChar.Intelligence}".PadRight(25));
            Console.Write("\tNew Intelligence: ");
            userResponse = Console.ReadLine();
            if (userResponse != "")
            {
                if (!int.TryParse(userResponse, out int intelligence))
                {
                    Console.WriteLine("\tThis is not a valid Intelligence value. Please enter a numerical value.");
                    Console.WriteLine();
                    Console.Write("\tNew INT: ");
                    int.TryParse(Console.ReadLine(), out intelligence);
                    selectedChar.Intelligence = intelligence;
                }
                else
                {
                    selectedChar.Intelligence = intelligence;
                }
            }

            Console.Write($"\tCurrent Wisdom (WIS): {selectedChar.Wisdom}".PadRight(25));
            Console.Write("\tNew Wisdom: ");
            userResponse = Console.ReadLine();
            if (userResponse != "")
            {
                if (!int.TryParse(userResponse, out int wisdom))
                {
                    Console.WriteLine("\tThis is not a valid Wisdom value. Please enter a numerical value.");
                    Console.WriteLine();
                    Console.Write("\tNew WIS: ");
                    int.TryParse(Console.ReadLine(), out wisdom);
                    selectedChar.Wisdom = wisdom;
                }
                else
                {
                    selectedChar.Wisdom = wisdom;
                }
            }

            Console.Write($"\tCurrent Charisma (CHA): {selectedChar.Charisma}".PadRight(25));
            Console.Write("\tNew Charisma: ");
            userResponse = Console.ReadLine();
            if (userResponse != "")
            {
                if (!int.TryParse(userResponse, out int charisma))
                {
                    Console.WriteLine("\tThis is not a valid Charisma value. Please enter a numerical value.");
                    Console.WriteLine();
                    Console.Write("\tNew CHA: ");
                    int.TryParse(Console.ReadLine(), out charisma);
                    selectedChar.Charisma = charisma;
                }
                else
                {
                    selectedChar.Charisma = charisma;
                }
            }

            // todo - equipment update query

            DisplayContinuePrompt();
        }

        /// <summary>
        /// SCREEN: displays character deletion option
        /// </summary>
        static void DisplayDeleteCharacter(List<Character> characters)
        {
            DisplayScreenHeader("Delete Character");

            // display all names
            Console.WriteLine("\tCharacter Names");
            Console.WriteLine("\t-------------");
            foreach (Character character in characters)
            {
                Console.WriteLine("\t" + character.Name);
            }

            // get user name choice
            Console.WriteLine();
            Console.Write("\tEnter name:");
            string characterName = Console.ReadLine();

            // get character object
            Character selectedChar = null;
            foreach (Character character in characters)
            {
                if (character.Name == characterName)
                {
                    selectedChar = character;
                    break;
                }
            }

            // delete character
            if (selectedChar != null)
            {
                characters.Remove(selectedChar);
                Console.WriteLine();
                Console.WriteLine("\t-------------");
                Console.WriteLine($"\t{selectedChar.Name} deleted");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("\t-------------");
                Console.WriteLine($"\t{characterName} not found");
            }

            DisplayContinuePrompt();
        }

        /// <summary>
        /// SCREEN: displays details for user's chosen character
        /// </summary>
        static void DisplayViewCharacterDetail(List<Character> characters)
        {
            DisplayScreenHeader("Character Detail");

            // display all names
            Console.WriteLine("\tCharacter Names");
            Console.WriteLine("\t-------------");
            foreach (Character character in characters)
            {
                Console.WriteLine("\t" + character.Name);
            }

            // get user character choice
            Console.WriteLine();
            Console.Write("\tEnter name:");
            string characterName = Console.ReadLine();

            // get character object
            Character selectedChar = null;
            foreach (Character character in characters)
            {
                if (character.Name == characterName)
                {
                    selectedChar = character;
                    break;
                }
            }

            if (selectedChar != null)
            {
                // display detail
                DisplayScreenHeader($"{selectedChar.Name}");
                DisplayTableFormat();
                CharInfo(selectedChar);

                DisplayContinuePrompt();
            }

        }

        /// <summary>
        /// SCREEN: displays new character creator
        /// </summary>
        static void DisplayAddCharacter(List<Character> characters)
        {
            Character newChar = new Character();

            // instruction screen
            DisplayScreenHeader("Add Character");
            Console.WriteLine();
            Console.WriteLine("You will be filling in the values on the next screen.");
            Console.WriteLine();
            Console.WriteLine("Races Available: DRAGONBORN, ELF, GENASI, GNOME, FIRBOLG, ORC, HUMAN, TIEFLING");
            Console.WriteLine();
            Console.WriteLine("Classes Available: BARBARIAN, BARD, CLERIC, DRUID, FIGHTER, MONK, PALADIN, RANGER, ROGUE, SORCERER, WARLOCK, WIZARD");
            Console.WriteLine();
            Console.WriteLine("Statuses Available: HEALTHY, POISONED, BLINDED, CHARMED, UNCONSCIOUS");
            DisplayContinuePrompt();

            // add character object property values
            DisplayScreenHeader("Add Character");

            Console.Write("\tName: ");
            newChar.Name = Console.ReadLine();
            if (String.IsNullOrEmpty(newChar.Name))
            {
                Console.WriteLine("\tYou have not entered a valid name. Please enter a name of at least one letter.");
                Console.WriteLine();
                Console.Write("\tName: ");
                newChar.Name = Console.ReadLine();
            }

            Console.Write("\tLevel: ");
            if (!int.TryParse(Console.ReadLine(), out int level))
            {
                Console.WriteLine("\tThis is not a valid level. Please enter a numerical value.");
                Console.WriteLine();
                Console.Write("\tLevel: ");
                int.TryParse(Console.ReadLine(), out level);
                newChar.Level = level;
            }
            else
            {
                newChar.Level = level;
            }

            Console.Write("\tEXP: ");
            if (!int.TryParse(Console.ReadLine(), out int exp))
            {
                Console.WriteLine("\tThis is not a valid EXP value. Please enter a numerical value.");
                Console.WriteLine();
                Console.Write("\tEXP: ");
                int.TryParse(Console.ReadLine(), out exp);
                newChar.Exp = exp;
            }
            else
            {
                newChar.Exp = exp;
            }

            Console.Write("\tRace: ");
            if (!Enum.TryParse(Console.ReadLine().ToUpper(), out Character.Race charRace))
            {
                Console.WriteLine("\tThis is not a valid race. Please enter DRAGONBORN, DWARF, ELF, GENASI, GNOME, FIRBOLG, ORC, HUMAN, or TIEFLING");
                Console.WriteLine();
                Console.Write("\tRace: ");
                Enum.TryParse(Console.ReadLine().ToUpper(), out charRace);
                newChar.CharRace = charRace;
            }
            else
            {
                newChar.CharRace = charRace;
            }

            Console.Write("\tClass: ");

            if (!Enum.TryParse(Console.ReadLine().ToUpper(), out Character.Class charClass))
            {
                Console.WriteLine("\tThis is not a valid class. Please enter BARBARIAN ,BARD, CLERIC ,DRUID, FIGHTER, MONK, PALADIN, RANGER,");
                Console.WriteLine("\tROGUE, SORCERER, WARLOCK, or WIZARD");
                Console.WriteLine();
                Console.Write("\tClass: ");
                Enum.TryParse(Console.ReadLine().ToUpper(), out charClass);
                newChar.CharClass = charClass;
            }
            else
            {
                newChar.CharClass = charClass;
            }

            Console.Write("\tHP: ");
            if (!int.TryParse(Console.ReadLine(), out int hp))
            {
                Console.WriteLine("\tThis is not a valid HP value. Please enter a numerical value.");
                Console.WriteLine();
                Console.Write("\tHP: ");
                int.TryParse(Console.ReadLine(), out hp);
                newChar.Hp = hp;
            }
            else
            {
                newChar.Hp = hp;
            }

            Console.Write("\tStatus: ");
            if (!Enum.TryParse(Console.ReadLine().ToUpper(), out Character.Status charStatus))
            {
                Console.WriteLine("\tThis is not a valid Status. Please enter HEALTHY, POISONED ,BLINDED, CHARMED, or UNCONSCIOUS");
                Console.WriteLine();
                Console.Write("\tStatus: ");
                Enum.TryParse(Console.ReadLine().ToUpper(), out charStatus);
                newChar.CharStatus = charStatus;
            }
            else
            {
                newChar.CharStatus = charStatus;
            }

            Console.Write("\tStrength (STR): ");
            if(!int.TryParse(Console.ReadLine(), out int strength))
            {
                Console.WriteLine("This is not a valid Strength value. Please enter a numerical value.");
                Console.WriteLine();
                Console.Write("\tSTR: ");
                int.TryParse(Console.ReadLine(), out strength);
                newChar.Strength = strength;
            }
            else
            {
                newChar.Strength = strength;
            }

            Console.Write("\tDexterity (DEX): ");
            if(!int.TryParse(Console.ReadLine(), out int dexterity))
            {
                Console.WriteLine("This is not a valid Dexterity value. Please enter a numerical value.");
                Console.WriteLine();
                Console.Write("\tDEX: ");
                int.TryParse(Console.ReadLine(), out dexterity);
                newChar.Dexterity = dexterity;
            }
            else
            {
                newChar.Dexterity = dexterity;
            }

            Console.Write("\tConstitution (CON): ");
            if (!int.TryParse(Console.ReadLine(), out int constitution))
            {
                Console.WriteLine("This is not a valid Constitution value. Please enter a numerical value.");
                Console.WriteLine();
                Console.Write("\tCON: ");
                int.TryParse(Console.ReadLine(), out constitution);
                newChar.Constitution = constitution;
            }
            else
            {
                newChar.Constitution = constitution;
            }

            Console.Write("\tIntelligence (INT): ");
            if (!int.TryParse(Console.ReadLine(), out int intelligence))
            {
                Console.WriteLine("This is not a valid Intelligence value. Please enter a numerical value.");
                Console.WriteLine();
                Console.Write("\tINT: ");
                int.TryParse(Console.ReadLine(), out intelligence);
                newChar.Intelligence = intelligence;
            }
            else
            {
                newChar.Intelligence = intelligence;
            }

            Console.Write("\tWisdom (WIS): ");
            if (!int.TryParse(Console.ReadLine(), out int wisdom))
            {
                Console.WriteLine("This is not a valid Wisdom value. Please enter a numerical value.");
                Console.WriteLine();
                Console.Write("\tWIS: ");
                int.TryParse(Console.ReadLine(), out wisdom);
                newChar.Wisdom = wisdom;
            }
            else
            {
                newChar.Wisdom = wisdom;
            }

            Console.Write("\tCharisma (CHA): ");
            if (!int.TryParse(Console.ReadLine(), out int charisma))
            {
                Console.WriteLine("This is not a valid Charisma value. Please enter a numerical value.");
                Console.WriteLine();
                Console.Write("\tCHA: ");
                int.TryParse(Console.ReadLine(), out charisma);
                newChar.Charisma = charisma;
            }
            else
            {
                newChar.Charisma = charisma;
            }

            // todo - new equipment query

            // echo new character properties
            DisplayScreenHeader("New Character's Profile");
            DisplayTableFormat();
            CharInfo(newChar);
            DisplayContinuePrompt();

            characters.Add(newChar);
        }

        /// <summary>
        /// SCREEN: displays all characters
        /// </summary>
        static void DisplayAllCharacters(List<Character> characters)
        {
            DisplayScreenHeader("All characters");
            
            DisplayTableFormat();
            foreach (Character character in characters)
            {
                CharInfo(character);
            }

            DisplayContinuePrompt();
        }


        #endregion

        #region HELPER METHODS

        /// <summary>
        /// displays character information
        /// </summary>
        static void CharInfo(Character character)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(
                    character.Name.ToString().PadRight(20) +
                    character.Level.ToString().PadRight(10) +
                    character.Exp.ToString().PadRight(8) +
                    character.CharRace.ToString().PadRight(15) +
                    character.CharClass.ToString().PadRight(15) +
                    character.Hp.ToString().PadRight(8) +
                    character.CharStatus.ToString().PadRight(15) +
                    character.Strength.ToString().PadRight(5) +
                    character.Dexterity.ToString().PadRight(5) +
                    character.Constitution.ToString().PadRight(5) +
                    character.Intelligence.ToString().PadRight(5) +
                    character.Wisdom.ToString().PadRight(5) +
                    character.Charisma.ToString().PadRight(5)
                );

        }

        /// <summary>
        /// displays table format for character information
        /// </summary>
        static void DisplayTableFormat()
        {            
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(
                "Name".PadRight(20) +
                "Level".PadRight(10) +
                "EXP".PadRight(8) +
                "Race".PadRight(15) +
                "Class".PadRight(15) +
                "HP".PadRight(8) +
                "Status".PadRight(15) +             
                "STR".PadRight(5) +
                "DEX".PadRight(5) +
                "CON".PadRight(5) +
                "INT".PadRight(5) +
                "WIS".PadRight(5) +
                "CHA".PadRight(5)
                );  
        }

        /// <summary>
        /// display welcome screen
        /// </summary>
        static void DisplayWelcomeScreen()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Clear();
    
            Console.WriteLine();
            Console.WriteLine("\t\t\t\t\tDungeons Database");
            Console.WriteLine();
            Console.WriteLine("This application keeps track of any Dungeons & Dragons characters in your campaign's party.");
            DisplayContinuePrompt();
        }

        /// <summary>
        /// display closing screen
        /// </summary>
        static void DisplayClosingScreen()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t\tExiting Dungeons Database...");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display continue prompt
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        /// <summary>
        /// display screen header
        /// </summary>
        static void DisplayScreenHeader(string headerText)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine("\t\t\t\t\t" + headerText);
            Console.WriteLine();
        }

        #endregion

    }

}

