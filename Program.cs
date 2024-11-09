namespace ReunionRegistrationApp
{
    class Program
    {
        static void Main(string[] args)
        {
            RegistrationManager manager = new RegistrationManager();
            int choice;

            do
            {
                Console.WriteLine("\n--- Reunion Registration Application ---");
                Console.WriteLine("1. Create Registration");
                Console.WriteLine("2. Read All Registrations");
                Console.WriteLine("3. Update Registration");
                Console.WriteLine("4. Delete Registration");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice: ");

                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 5.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        manager.CreateRegistration();
                        break;
                    case 2:
                        manager.ReadAllRegistrations();
                        break;
                    case 3:
                        Console.Write("Enter Registration ID to update: ");
                        if (int.TryParse(Console.ReadLine(), out int updateId))
                        {
                            manager.UpdateRegistration(updateId);
                        }
                        else
                        {
                            Console.WriteLine("Invalid ID.");
                        }
                        break;
                    case 4:
                        Console.Write("Enter Registration ID to delete: ");
                        if (int.TryParse(Console.ReadLine(), out int deleteId))
                        {
                            manager.DeleteRegistration(deleteId);
                        }
                        else
                        {
                            Console.WriteLine("Invalid ID.");
                        }
                        break;
                    case 5:
                        Console.WriteLine("Exiting the application...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            } while (choice != 5);
        }
    }

    class Registration
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string Village { get; set; }
        public string PostOffice { get; set; }
        public string MobileNumber { get; set; }
        public int SSCYear { get; set; }
        public string CurrentPosition { get; set; }
        public string JobTitle { get; set; }
        public string SpouseName { get; set; }
        public string SpouseProfession { get; set; }
        public string SpouseWorkplace { get; set; }
        public string SpouseJobTitle { get; set; }
        public Dictionary<string, int> ChildrenInfo { get; set; }
        public string FeeProof { get; set; }
        public bool HasSpouse { get; set; }
        public bool HasChildren { get; set; }

        public Registration()
        {
            Name = string.Empty;
            FatherName = string.Empty;
            MotherName = string.Empty;
            Village = string.Empty;
            PostOffice = string.Empty;
            MobileNumber = string.Empty;
            CurrentPosition = string.Empty;
            JobTitle = string.Empty;
            SpouseName = string.Empty;
            SpouseProfession = string.Empty;
            SpouseWorkplace = string.Empty;
            SpouseJobTitle = string.Empty;
            ChildrenInfo = new Dictionary<string, int>();
            FeeProof = string.Empty;
        }

        public void Display()
        {
            Console.WriteLine($"ID: {Id}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Father's Name: {FatherName}");
            Console.WriteLine($"Mother's Name: {MotherName}");
            Console.WriteLine($"Village: {Village}");
            Console.WriteLine($"Post Office: {PostOffice}");
            Console.WriteLine($"Mobile Number: {MobileNumber}");
            Console.WriteLine($"SSC Year: {SSCYear}");
            Console.WriteLine($"Current Position: {CurrentPosition}");
            Console.WriteLine($"Job Title: {JobTitle}");

            if (HasSpouse)
            {
                Console.WriteLine($"Spouse Name: {SpouseName}");
                Console.WriteLine($"Spouse Profession: {SpouseProfession}");
                Console.WriteLine($"Spouse Workplace: {SpouseWorkplace}");
                Console.WriteLine($"Spouse Job Title: {SpouseJobTitle}");
            }

            if (HasChildren)
            {
                Console.WriteLine("Children Info:");
                foreach (var child in ChildrenInfo)
                {
                    Console.WriteLine($"   Name: {child.Key}, Age: {child.Value}");
                }
            }

            Console.WriteLine($"Fee Proof: {FeeProof}");
        }
    }

    class RegistrationManager
    {
        private List<Registration> registrations;
        private int nextId;

        public RegistrationManager()
        {
            registrations = new List<Registration>();
            nextId = 1;
        }

        public void CreateRegistration()
        {
            Registration reg = new Registration();
            reg.Id = nextId++;

            Console.Write("Enter Name: ");
            reg.Name = Console.ReadLine();
            Console.Write("Enter Father's Name: ");
            reg.FatherName = Console.ReadLine();
            Console.Write("Enter Mother's Name: ");
            reg.MotherName = Console.ReadLine();
            Console.Write("Enter Village: ");
            reg.Village = Console.ReadLine();
            Console.Write("Enter Post Office: ");
            reg.PostOffice = Console.ReadLine();
            Console.Write("Enter Mobile Number: ");
            reg.MobileNumber = Console.ReadLine();
            reg.SSCYear = GetValidatedInt("Enter SSC Year: ");
            Console.Write("Enter Current Position: ");
            reg.CurrentPosition = Console.ReadLine();
            Console.Write("Enter Job Title: ");
            reg.JobTitle = Console.ReadLine();

            reg.HasSpouse = GetYesOrNo("Is there a spouse? (0 = Yes, 1 = No): ");
            if (reg.HasSpouse)
            {
                Console.Write("Enter Spouse Name: ");
                reg.SpouseName = Console.ReadLine();
                Console.Write("Enter Spouse Profession: ");
                reg.SpouseProfession = Console.ReadLine();
                Console.Write("Enter Spouse Workplace: ");
                reg.SpouseWorkplace = Console.ReadLine();
                Console.Write("Enter Spouse Job Title: ");
                reg.SpouseJobTitle = Console.ReadLine();
            }

            reg.HasChildren = GetYesOrNo("Do you have children? (0 = Yes, 1 = No): ");
            if (reg.HasChildren)
            {
                Console.WriteLine("Enter Children Info (enter 'done' to finish):");
                while (true)
                {
                    Console.Write("Enter Child Name (or 'done' to stop): ");
                    string childName = Console.ReadLine();
                    if (childName.ToLower() == "done") break;

                    int childAge = GetValidatedInt("Enter Child Age: ");
                    reg.ChildrenInfo[childName] = childAge;
                }
            }

            Console.Write("Enter Fee Proof: ");
            reg.FeeProof = Console.ReadLine();

            registrations.Add(reg);
            Console.WriteLine("Registration created successfully!");
        }

        private int GetValidatedInt(string prompt)
        {
            int result;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (int.TryParse(input, out result))
                {
                    return result;
                }
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }

        private bool GetYesOrNo(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (input == "0") return true;
                if (input == "1") return false;
                Console.WriteLine("Invalid input. Please enter 0 for Yes or 1 for No.");
            }
        }

        public void ReadAllRegistrations()
        {
            if (registrations.Count == 0)
            {
                Console.WriteLine("No registrations found.");
                return;
            }

            foreach (var reg in registrations)
            {
                reg.Display();
                Console.WriteLine();
            }
        }

        public void UpdateRegistration(int id)
        {
            var reg = registrations.Find(r => r.Id == id);
            if (reg == null)
            {
                Console.WriteLine("Registration not found.");
                return;
            }

            Console.WriteLine("\nSelect the field to update:");
            Console.WriteLine("1. Name");
            Console.WriteLine("2. Father's Name");
            Console.WriteLine("3. Mother's Name");
            Console.WriteLine("4. Village");
            Console.WriteLine("5. Post Office");
            Console.WriteLine("6. Mobile Number");
            Console.WriteLine("7. SSC Year");
            Console.WriteLine("8. Current Position");
            Console.WriteLine("9. Job Title");
            Console.WriteLine("10. Spouse Name");
            Console.WriteLine("11. Spouse Profession");
            Console.WriteLine("12. Spouse Workplace");
            Console.WriteLine("13. Spouse Job Title");
            Console.WriteLine("14. Children Info");
            Console.WriteLine("15. Fee Proof");

            int choice = GetValidatedInt("Enter the number of the field you want to update: ");

            switch (choice)
            {
                case 1:
                    Console.Write("Enter new Name: ");
                    reg.Name = Console.ReadLine();
                    break;
                case 2:
                    Console.Write("Enter new Father's Name: ");
                    reg.FatherName = Console.ReadLine();
                    break;
                case 3:
                    Console.Write("Enter new Mother's Name: ");
                    reg.MotherName = Console.ReadLine();
                    break;
                case 4:
                    Console.Write("Enter new Village: ");
                    reg.Village = Console.ReadLine();
                    break;
                case 5:
                    Console.Write("Enter new Post Office: ");
                    reg.PostOffice = Console.ReadLine();
                    break;
                case 6:
                    Console.Write("Enter new Mobile Number: ");
                    reg.MobileNumber = Console.ReadLine();
                    break;
                case 7:
                    reg.SSCYear = GetValidatedInt("Enter new SSC Year: ");
                    break;
                case 8:
                    Console.Write("Enter new Current Position: ");
                    reg.CurrentPosition = Console.ReadLine();
                    break;
                case 9:
                    Console.Write("Enter new Job Title: ");
                    reg.JobTitle = Console.ReadLine();
                    break;
                case 10:
                    if (reg.HasSpouse)
                    {
                        Console.Write("Enter new Spouse Name: ");
                        reg.SpouseName = Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("No spouse information available.");
                    }
                    break;
                case 11:
                    if (reg.HasSpouse)
                    {
                        Console.Write("Enter new Spouse Profession: ");
                        reg.SpouseProfession = Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("No spouse information available.");
                    }
                    break;
                case 12:
                    if (reg.HasSpouse)
                    {
                        Console.Write("Enter new Spouse Workplace: ");
                        reg.SpouseWorkplace = Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("No spouse information available.");
                    }
                    break;
                case 13:
                    if (reg.HasSpouse)
                    {
                        Console.Write("Enter new Spouse Job Title: ");
                        reg.SpouseJobTitle = Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("No spouse information available.");
                    }
                    break;
                case 14:
                    if (reg.HasChildren)
                    {
                        Console.WriteLine("Enter Children Info (enter 'done' to finish):");
                        while (true)
                        {
                            Console.Write("Enter Child Name (or 'done' to stop): ");
                            string childName = Console.ReadLine();
                            if (childName.ToLower() == "done") break;

                            int childAge = GetValidatedInt("Enter Child Age: ");
                            reg.ChildrenInfo[childName] = childAge;
                        }
                    }
                    else
                    {
                        Console.WriteLine("No children information available.");
                    }
                    break;
                case 15:
                    Console.Write("Enter new Fee Proof: ");
                    reg.FeeProof = Console.ReadLine();
                    break;
                default:
                    Console.WriteLine("Invalid choice. No field updated.");
                    break;
            }

            Console.WriteLine("Registration updated successfully!");
        }

        public void DeleteRegistration(int id)
        {
            var reg = registrations.Find(r => r.Id == id);
            if (reg == null)
            {
                Console.WriteLine("Registration not found.");
                return;
            }

            registrations.Remove(reg);
            Console.WriteLine("Registration deleted successfully!");
        }
    }
}
