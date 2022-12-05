using Eventsourcing.EndUser.Commands.Interfaces;

namespace Eventsourcing.EndUser.Commands.Users;

public class TextUserNamesMapper : ITextUserNamesMapper
{
    public IEnumerable<UserInputModel> MapToInputModel(ITextUserNamesDataSource dataSource)
    {
        var menNames = new List<string>();
        var womenNames = new List<string>();
        var lastNames = new List<string>();
                    
        menNames.AddRange(GetLinesFromContent(dataSource.MenNames));
        womenNames.AddRange(GetLinesFromContent(dataSource.WomenNames));
        lastNames.AddRange(GetLinesFromContent(dataSource.LastNames));

        var usersByEmail = new Dictionary<string, UserInputModel>();

        CreateMenUsers(usersByEmail, menNames, lastNames);
        CreateWomenUsers(usersByEmail, womenNames, lastNames);

        var users = usersByEmail.Values.ToList();

        return users;
    }

    private IEnumerable<string> GetLinesFromContent(string content)
    {
        if (string.IsNullOrEmpty(content))
        {
            return Enumerable.Empty<string>();
        }

        var lines = new List<string>();

        using var reader = new StringReader(content);
        while (reader.Peek() != -1)
        {
            lines.Add(reader.ReadLine() ?? string.Empty);
        }

        return lines;
    }

    private void CreateMenUsers(IDictionary<string, UserInputModel> users, IList<string> menNames, IList<string> lastNames)
    {
        int numberOfMen = 0;
        while (numberOfMen < 500)
        {
            var indexGenerator = new Random(numberOfMen);
            var nameIndex = indexGenerator.Next(0, menNames.Count() - 1);
            var lastNameIndex = indexGenerator.Next(0, lastNames.Count() - 1);
            var lastName2Index = indexGenerator.Next(0, lastNames.Count() - 1);

            var ageGenerator = new Random(numberOfMen);
            var age = (short)ageGenerator.Next(20, 70);

            var newMan = new UserInputModel
            {
                UserId = Guid.NewGuid(),
                Name = menNames[nameIndex],
                LastName = $"{lastNames[lastNameIndex]} {lastNames[lastName2Index]}",
                Genre = "M",
                Age = age
            };

            newMan.Email = string.Format("{0}.{1}.{2}@fake-domain.com",
                newMan.Name.Trim().Replace(" ", "_").ToLower(),
                lastNames[lastNameIndex].Trim().Replace(" ", "_").ToLower(),
                (DateTime.Now.Year - age).ToString());

            if (!users.ContainsKey(newMan.Email))
            {
                users.Add(newMan.Email, newMan);
                numberOfMen++;
            }
        }
    }

    private void CreateWomenUsers(IDictionary<string, UserInputModel> users, IList<string> womenNames, IList<string> lastNames)
    {
        int numberOfWoman = 0;
        while (numberOfWoman < 500)
        {
            var indexGenerator = new Random(numberOfWoman);
            var nameIndex = indexGenerator.Next(0, womenNames.Count() - 1);
            var lastNameIndex = indexGenerator.Next(0, lastNames.Count() - 1);
            var lastName2Index = indexGenerator.Next(0, lastNames.Count() - 1);

            var ageGenerator = new Random(numberOfWoman);
            var age = (short)ageGenerator.Next(20, 70);

            var newWoman = new UserInputModel
            {
                UserId = Guid.NewGuid(),
                Name = womenNames[nameIndex],
                LastName = $"{lastNames[lastNameIndex]} {lastNames[lastName2Index]}",
                Genre = "F",
                Age = age
            };

            newWoman.Email = string.Format("{0}.{1}.{2}@fake-domain.com",
                newWoman.Name.Trim().Replace(" ", "_").ToLower(),
                lastNames[lastNameIndex].Trim().Replace(" ", "_").ToLower(),
                (DateTime.Now.Year - age).ToString());

            if (!users.ContainsKey(newWoman.Email))
            {
                users.Add(newWoman.Email, newWoman);
                numberOfWoman++;
            }
        }
    }
}
